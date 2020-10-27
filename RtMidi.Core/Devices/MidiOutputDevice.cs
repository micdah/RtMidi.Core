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

        public bool Send(in NoteOffMessage noteOffMessage)
            => _outputDevice.SendMessage(noteOffMessage.Encode());

        public bool Send(in NoteOnMessage noteOnMessage)
            => _outputDevice.SendMessage(noteOnMessage.Encode());

        public bool Send(in PolyphonicKeyPressureMessage polyphonicKeyPressureMessage)
            => _outputDevice.SendMessage(polyphonicKeyPressureMessage.Encode());

        public bool Send(in ControlChangeMessage controlChangeMessage)
            => _outputDevice.SendMessage(controlChangeMessage.Encode());

        public bool Send(in ProgramChangeMessage programChangeMessage)
            => _outputDevice.SendMessage(programChangeMessage.Encode());

        public bool Send(in ChannelPressureMessage channelPressureMessage)
            => _outputDevice.SendMessage(channelPressureMessage.Encode());

        public bool Send(in PitchBendMessage pitchBendMessage)
            => _outputDevice.SendMessage(pitchBendMessage.Encode());

        public bool Send(in NrpnMessage nrpnMessage)
            => nrpnMessage.Encode().All(msg => Send(in msg));
        
        public bool Send(in SysExMessage sysExMessage)
            => _outputDevice.SendMessage(sysExMessage.Encode());

        public bool Send(in MidiTimeCodeQuarterFrameMessage midiTimeCodeQuarterFrameMessage)
            => _outputDevice.SendMessage(midiTimeCodeQuarterFrameMessage.Encode());

        public bool Send(in SongPositionPointerMessage songPositionPointerMessage)
            => _outputDevice.SendMessage(songPositionPointerMessage.Encode());

        public bool Send(in SongSelectMessage songSelectMessage)
            => _outputDevice.SendMessage(songSelectMessage.Encode());

        public bool Send(in TuneRequestMessage tuneRequestMessage)
            => _outputDevice.SendMessage(tuneRequestMessage.Encode());
    }
}
