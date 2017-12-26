using System.Linq;
using RtMidi.Core.Messages;
using RtMidi.Core.Unmanaged.Devices;

namespace RtMidi.Core.Devices
{
    internal class MidiOutputDevice : MidiDevice, IMidiOutputDevice
    {
        private readonly IRtMidiOutputDevice _outputDevice;
        
        public MidiOutputDevice(IRtMidiOutputDevice outputDevice, string name) : base(outputDevice, name)
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

        public bool Send(NrpnMessage nrpnMessage)
            => nrpnMessage.Encode().All(Send);
    }
}
