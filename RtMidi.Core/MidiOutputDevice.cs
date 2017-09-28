using RtMidi.Core.Messages;
using RtMidi.Core.Unmanaged.Devices;
using Serilog;

namespace RtMidi.Core
{
    internal class MidiOutputDevice : MidiDevice, IMidiOutputDevice
    {
        private readonly IRtMidiOutputDevice _outputDevice;
        private static readonly ILogger Log = Serilog.Log.ForContext<MidiOutputDevice>();
        
        public MidiOutputDevice(IRtMidiOutputDevice outputDevice) : base(outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public bool Send(NoteOffMessage noteOffMessage)
            => _outputDevice.SendMessage(noteOffMessage.Encode());

        public bool Send(NoteOnMessage noteOnMessage)
            => _outputDevice.SendMessage(noteOnMessage.Encode());

        public bool Send(PolyphonicKeyPressureMessage polyphonicKeyPressureMessage)
            => _outputDevice.SendMessage(polyphonicKeyPressureMessage.Encode());

        public bool Send(ControlChangeMessage controlChangeMessage)
            => _outputDevice.SendMessage(controlChangeMessage.Encode());

        public bool Send(ProgramChangeMessage programChangeMessage)
            => _outputDevice.SendMessage(programChangeMessage.Encode());

        public bool Send(ChannelPressureMessage channelPressureMessage)
            => _outputDevice.SendMessage(channelPressureMessage.Encode());

        public bool Send(PitchBendMessage pitchBendMessage)
            => _outputDevice.SendMessage(pitchBendMessage.Encode());
    }
}
