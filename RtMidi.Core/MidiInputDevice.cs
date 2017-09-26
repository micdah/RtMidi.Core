using RtMidi.Core.Unmanaged.Devices;
using RtMidi.Core.Messages;
using System;
using Serilog;

namespace RtMidi.Core
{
    internal class MidiInputDevice : MidiDevice, IMidiInputDevice
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<MidiInputDevice>();
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

            try 
            {
                Decode(message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception occurred while decoding midi message");
            }
        }

        private void Decode(byte[] message)
        {
            byte status = message[0];
            switch (status & 0b1111_0000)
            {
                case Midi.NoteOffBitmask:
                    if (NoteOffMessage.TryDecode(message, out var noteOffMessage))
                        NoteOff?.Invoke(this, noteOffMessage);
                    break;
                case Midi.NoteOnBitmask:
                    if (NoteOnMessage.TryDecoce(message, out var noteOnMessage))
                        NoteOn?.Invoke(this, noteOnMessage);
                    break;
                case Midi.PolyphonicKeyPressureBitmask:
                    if (PolyphonicKeyPressureMessage.TryDecode(message, out var polyphonicKeyPressureMessage))
                        PolyphonicKeyPressure?.Invoke(this, polyphonicKeyPressureMessage);
                    break;
                case Midi.ControlChangeBitmask:
                    if (ControlChangeMessage.TryDecode(message, out var controlChangeMessage))
                        ControlChange?.Invoke(this, controlChangeMessage);
                    break;
                default:
                    Log.Error("Unknown message type {Bitmask}", $"{status & 0b1111_0000:X2}");
                    break;
            }
        }

        protected override void Disposing()
        {
            _rtMidiInputDevice.Message -= RtMidiInputDevice_Message;
        }
    }
}
