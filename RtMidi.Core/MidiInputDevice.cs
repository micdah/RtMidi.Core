using RtMidi.Core.Unmanaged.Devices;
using RtMidi.Core.Messages;
using System;
using Serilog;
using RtMidi.Core.Enums;

namespace RtMidi.Core
{
    internal class MidiInputDevice : MidiDevice, IMidiInputDevice
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<MidiInputDevice>();

        /// <summary>
        /// Bitmask to isolate channel part of status byte
        /// </summary>
        internal const byte ChannelBitmask = 0b0000_1111;

        /// <summary>
        /// Bitmask to isolate data part of data byte
        /// </summary>
        internal const byte DataBitmask = 0b0111_1111;

        /// <summary>
        /// Status masks
        /// </summary>
        internal const byte NoteOffBitmask = 0b1000_0000;
        internal const byte NoteOnBitmask = 0b1001_0000;
        internal const byte PolyphonicKeyPressureBitmask = 0b1010_0000;
        internal const byte ControlChangeBitmask = 0b1011_0000;

        private readonly IRtMidiInputDevice _rtMidiInputDevice;

        public MidiInputDevice(IRtMidiInputDevice rtMidiInputDevice) : base(rtMidiInputDevice)
        {
            _rtMidiInputDevice = rtMidiInputDevice;
            _rtMidiInputDevice.Message += RtMidiInputDevice_Message;
        }

        public event EventHandler<NoteOffMessage> NoteOff;
        public event EventHandler<NoteOnMessage> NoteOn;
        public event EventHandler<PolyphonicKeyPressureMessage> PolyphonicKeyPressure;
        public event EventHandler<ControlChangeMessage> ControlChange;

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
                case NoteOnBitmask:
                    DecodeNoteOnMessage(message);
                    break;
                case PolyphonicKeyPressureBitmask:
                    DecodePolyphonicKeyPressureMessage(message);
                    break;
                case ControlChangeBitmask:
                    DecodeControlChange(message);
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
                Log.Error("Incorrect number of bytes ({Length}) received for Note Off message", message.Length);
                return;
            }

            var @event = NoteOff;
            if (@event != null)
            {
                var channel = (Channel)(ChannelBitmask & message[0]);
                var key = (Key)(DataBitmask & message[1]);
                var velocity = DataBitmask & message[2];

                @event.Invoke(this, new NoteOffMessage(channel, key, velocity));
            }
        }

        private void DecodeNoteOnMessage(byte[] message) 
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Note On message", message.Length);
                return;
            }

            var @event = NoteOn;
            if (@event != null) 
            {
                var channel = (Channel)(ChannelBitmask & message[0]);
                var key = (Key)(DataBitmask & message[1]);
                var velocity = DataBitmask & message[2];

                @event.Invoke(this, new NoteOnMessage(channel, key, velocity));
            }
        }

        private void DecodePolyphonicKeyPressureMessage(byte[] message)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect nuber of bytes ({Length}) received for Polyphonic Key Pressure message", message.Length);
                return;
            }

            var @event = PolyphonicKeyPressure;
            if (@event != null) 
            {
                var channel = (Channel)(ChannelBitmask & message[0]);
                var key = (Key)(DataBitmask & message[1]);
                var pressure = DataBitmask & message[2];

                @event.Invoke(this, new PolyphonicKeyPressureMessage(channel, key, pressure));
            }
        }

        private void DecodeControlChange(byte[] message) 
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of btyes ({Length}) received for Control Change message");
                return;
            }

            var @event = ControlChange;
            if (@event != null)
            {
                var channel = (Channel)(ChannelBitmask & message[0]);
                var control = DataBitmask & message[1];
                var value = DataBitmask & message[2];

                @event.Invoke(this, new ControlChangeMessage(channel, control, value));
            }
        }

        protected override void Disposing()
        {
            _rtMidiInputDevice.Message -= RtMidiInputDevice_Message;
        }
    }
}
