using RtMidi.Core.Unmanaged.Devices;
using RtMidi.Core.Messages;
using System;
using Serilog;
using RtMidi.Core.Enums;

namespace RtMidi.Core
{
    internal class MidiInputDevice : MidiDevice, IMidiInputDevice
    {
        internal const byte NoteOffBitmask = 0b1000_0000;
        internal const byte NoteOnBitmask = 0b1001_0000;

        private static readonly ILogger Log = Serilog.Log.ForContext<MidiInputDevice>();
        private readonly IRtMidiInputDevice _rtMidiInputDevice;

        public MidiInputDevice(IRtMidiInputDevice rtMidiInputDevice) : base(rtMidiInputDevice)
        {
            _rtMidiInputDevice = rtMidiInputDevice;
            _rtMidiInputDevice.Message += RtMidiInputDevice_Message;
        }

        public event EventHandler<NoteOffMessage> NoteOff;
        public event EventHandler<NoteOnMessage> NoteOn;

        private void RtMidiInputDevice_Message(object sender, byte[] message)
        {
            if (message == null)
            {
                Log.Error("Received null message from device");
                return;
            }

            if (message.Length == 0) 
            {
                Log.Error("Received empty message from device");
                return;
            }

            // TODO Decode and propagate midi events on separate thread as not to block receiving thread
            Decode(message);
        }

        private void Decode(byte[] message)
        {
            byte status = message[0];
            switch (status & 0b1111_0000)
            {
                case NoteOffBitmask:
                    DecodeNoteOffMessage(message);
                    break;
                default:
                    Log.Error("Unknown message type {Bitmask}", $"{status & 0b1111_0000:X2}");
                    break;
            }
        }

        private void DecodeNoteOffMessage(byte[] message)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Note Off Message", message.Length);
                return;
            }

            var noteOff = NoteOff;
            if (noteOff != null)
            {
                var channel = (Channel)(0b0000_1111 & message[0]);
                var key = (Key)(0b0111_1111 & message[1]);
                var velocity = 0b0111_1111 & message[2];

                noteOff.Invoke(this, new NoteOffMessage(channel, key, velocity));
            }
        }

        protected override void Disposing()
        {
            _rtMidiInputDevice.Message -= RtMidiInputDevice_Message;
        }
    }
}
