using System;
using RtMidi.Core.Unmanaged.Devices;
using Xunit;
using RtMidi.Core.Messages;
using System.Collections.Generic;
using RtMidi.Core.Enums;
using Xunit.Abstractions;
using System.Linq;

namespace RtMidi.Core.Tests
{
    public class MidiInputDeviceTests : TestBase
    {
        private readonly RtMidiInputDeviceMock _inputDeviceMock;
        private readonly IMidiInputDevice _sut;
        private readonly Queue<NoteOffMessage> _noteOffMessages = new Queue<NoteOffMessage>();
        private readonly Queue<NoteOnMessage> _noteOnMessages = new Queue<NoteOnMessage>();

        public MidiInputDeviceTests(ITestOutputHelper output) : base(output)
        {
            _inputDeviceMock = new RtMidiInputDeviceMock();
            _sut = new MidiInputDevice(_inputDeviceMock);

            _sut.NoteOff += (sender, e) => _noteOffMessages.Enqueue(e);
            _sut.NoteOn += (sender, e) => _noteOnMessages.Enqueue(e);
        }

        [Fact]
        public void Test_NoteOffMessage()
        {
            var noteOffMsg = NoteOffMessage(Channel.Channel_4, Key.Key_127, 11);
            Assert.NotNull(noteOffMsg);
            Assert.Equal(3, noteOffMsg.Length);
            Assert.Equal(0b1000_0011, noteOffMsg[0]);   // Channel
            Assert.Equal(0b0111_1111, noteOffMsg[1]);   // Key
            Assert.Equal(0b0000_1011, noteOffMsg[2]);   // Velocity
        }

        [Fact]
        public void Should_Fire_Events_On_Channel_1_Through_16()
        {
            for (var i = 0; i < 16; i++)
            {
                var channel = (Channel)i;
                _inputDeviceMock.OnMessage(NoteOffMessage(channel));
                Assert.True(_noteOffMessages.TryDequeue(out var noteOffMessage));
                Assert.Equal(channel, noteOffMessage.Channel);
            }
        }

        [Fact]
        public void Should_Fire_NoteOffMessage() 
        {
            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>()) 
            {
                foreach (var key in Enum.GetValues(typeof(Key)).Cast<Key>())
                {
                    for (var velocity = 0; velocity <= 127; velocity ++) 
                    {
                        _inputDeviceMock.OnMessage(NoteOffMessage(channel, key, velocity));
                        Assert.True(_noteOffMessages.TryDequeue(out var noteOffMessage));

                        Assert.Equal(channel, noteOffMessage.Channel);
                        Assert.Equal(key, noteOffMessage.Key);
                        Assert.Equal(velocity, noteOffMessage.Velocity);
                    }
                }
            }
        }

        [Fact]
        public void Should_Fire_NoteOnMessages()
        {
            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>())
            {
                foreach (var key in Enum.GetValues(typeof(Key)).Cast<Key>())
                {
                    for (var velocity = 0; velocity <= 127; velocity++)
                    {
                        _inputDeviceMock.OnMessage(NoteOnMessage(channel, key, velocity));
                        Assert.True(_noteOnMessages.TryDequeue(out var noteOffMessage));

                        Assert.Equal(channel, noteOffMessage.Channel);
                        Assert.Equal(key, noteOffMessage.Key);
                        Assert.Equal(velocity, noteOffMessage.Velocity);
                    }
                }
            }
        }

        private byte[] NoteOffMessage(Channel channel, Key key = Key.Key_0, int velocity = 0)
        {
            return new byte[]
            {
                (byte)(MidiInputDevice.NoteOffBitmask | (MidiInputDevice.ChannelBitmask & (int)channel)),
                (byte)(MidiInputDevice.DataBitmask & (int)key),
                (byte)(MidiInputDevice.DataBitmask & velocity)
            };
        }

        private byte[] NoteOnMessage(Channel channel, Key key = Key.Key_0, int velocity = 0)
        {
            return new byte[]
            {
                (byte)(MidiInputDevice.NoteOnBitmask | (MidiInputDevice.ChannelBitmask & (int)channel)),
                (byte)(MidiInputDevice.DataBitmask & (int)key),
                (byte)(MidiInputDevice.DataBitmask & velocity)
            };
        }

        private class RtMidiInputDeviceMock : IRtMidiInputDevice
        {
            public bool IsOpen => true;

            public event EventHandler<byte[]> Message;

            public void OnMessage(byte[] message) => Message?.Invoke(this, message);

            public void Close()
            {
            }

            public void Dispose()
            {
            }

            public bool Open() => true;
        }
    }
}