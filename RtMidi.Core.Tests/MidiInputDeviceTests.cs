using System;
using RtMidi.Core.Unmanaged.Devices;
using Xunit;
using RtMidi.Core.Messages;
using System.Collections.Generic;
using RtMidi.Core.Enums;

namespace RtMidi.Core.Tests
{
    public class MidiInputDeviceTests
    {
        private readonly RtMidiInputDeviceMock _inputDevice;
        private readonly IMidiInputDevice _sut;
        private readonly Queue<NoteOffMessage> _noteOffMessages = new Queue<NoteOffMessage>();

        public MidiInputDeviceTests()
        {
            _inputDevice = new RtMidiInputDeviceMock();
            _sut = new MidiInputDevice(_inputDevice);

            _sut.NoteOff += (sender, e) => _noteOffMessages.Enqueue(e);
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
                _inputDevice.OnMessage(NoteOffMessage(channel));
                Assert.True(_noteOffMessages.TryDequeue(out var noteOffMessage));
                Assert.Equal(channel, noteOffMessage.Channel);
            }
        }

        private byte[] NoteOffMessage(Channel channel, Key key = Key.Key_0, int velocity = 0)
        {
            return new byte[]
            {
                (byte)(0b1000_0000 | (0b0000_1111 & (int)channel)),
                (byte)(0b0111_1111 & (int)key),
                (byte)(0b0111_1111 & velocity)
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