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
        private readonly Queue<PolyphonicKeyPressureMessage> _polyphonicKeyPressureMessages = new Queue<Messages.PolyphonicKeyPressureMessage>();
        private readonly Queue<ControlChangeMessage> _controlChangeMessages = new Queue<Messages.ControlChangeMessage>();

        public MidiInputDeviceTests(ITestOutputHelper output) : base(output)
        {
            _inputDeviceMock = new RtMidiInputDeviceMock();
            _sut = new MidiInputDevice(_inputDeviceMock);

            _sut.NoteOff += (sender, e) => _noteOffMessages.Enqueue(e);
            _sut.NoteOn += (sender, e) => _noteOnMessages.Enqueue(e);
            _sut.PolyphonicKeyPressure += (sender, e) => _polyphonicKeyPressureMessages.Enqueue(e);
            _sut.ControlChange += (sender, e) => _controlChangeMessages.Enqueue(e);
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

        private static void AllChannels(Action<Channel> func)
        {
            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>())
            {
                func(channel);
            }
        }

        private static void AllKeys(Action<Key> func)
        {
            foreach (var key in Enum.GetValues(typeof(Key)).Cast<Key>()) 
            {
                func(key);
            }
        }

        private static void AllInRange(int from, int to, Action<int> func)
        {
            for (var i = from; i <= to; i++) 
            {
                func(i);
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
                        Assert.True(_noteOffMessages.TryDequeue(out var msg));

                        Assert.Equal(channel, msg.Channel);
                        Assert.Equal(key, msg.Key);
                        Assert.Equal(velocity, msg.Velocity);
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
                        Assert.True(_noteOnMessages.TryDequeue(out var msg));

                        Assert.Equal(channel, msg.Channel);
                        Assert.Equal(key, msg.Key);
                        Assert.Equal(velocity, msg.Velocity);
                    }
                }
            }
        }

        [Fact]
        public void Should_Fire_PolyphonicKeyPressureMessages() 
        {
            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>()) 
            {
                foreach (var key in Enum.GetValues(typeof(Key)).Cast<Key>()) 
                {
                    for (var pressure = 0; pressure <= 127; pressure++)
                    {
                        _inputDeviceMock.OnMessage(PolyphonicKeyPressureMessage(channel, key, pressure));
                        Assert.True(_polyphonicKeyPressureMessages.TryDequeue(out var msg));

                        Assert.Equal(channel, msg.Channel);
                        Assert.Equal(key, msg.Key);
                        Assert.Equal(pressure, msg.Pressure);
                    }
                }
            }
        }

        [Fact]
        public void Should_Fire_ControlChangeMessages()
        {
            AllChannels(channel => AllInRange(0, 127, control => AllInRange(0, 127, value =>
            {
                _inputDeviceMock.OnMessage(ControlChangeMessage(channel, control, value));
                Assert.True(_controlChangeMessages.TryDequeue(out var msg));

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(control, msg.Control);
                Assert.Equal(value, msg.Value);
            })));
        }

        private static byte[] NoteOffMessage(Channel channel, Key key = Key.Key_0, int velocity = 0)
        {
            return new byte[]
            {
                StatusByte(MidiInputDevice.NoteOffBitmask, channel),
                DataByte(key),
                DataByte(velocity)
            };
        }

        private static byte[] NoteOnMessage(Channel channel, Key key = Key.Key_0, int velocity = 0)
        {
            return new byte[]
            {
                StatusByte(MidiInputDevice.NoteOnBitmask, channel),
                DataByte(key),
                DataByte(velocity)
            };
        }

        private static byte[] PolyphonicKeyPressureMessage(Channel channel, Key key = Key.Key_0, int pressure = 0)
        {
            return new byte[]
            {
                StatusByte(MidiInputDevice.PolyphonicKeyPressureBitmask, channel),
                DataByte(key),
                DataByte(pressure)
            };
        }

        private static byte[] ControlChangeMessage(Channel channel, int control, int value)
        {
            return new byte[]
            {
                StatusByte(MidiInputDevice.ControlChangeBitmask, channel),
                DataByte(control),
                DataByte(value)
            };
        }

        private static byte StatusByte(byte statusBitmask, Channel channel) 
        => (byte)(statusBitmask | (MidiInputDevice.ChannelBitmask & (int)channel));

        private static byte DataByte(int value)
        => (byte)(MidiInputDevice.DataBitmask & value);

        private static byte DataByte(Key key)
        => (byte)(MidiInputDevice.DataBitmask & (int)key);
            

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