using RtMidi.Core.Devices.Nrpn;
using RtMidi.Core.Messages;

namespace RtMidi.Core.Devices
{
    public interface IMidiInputDevice : IMidiDevice
    {
        /// <summary>
        /// Note Off event.
        /// </summary>
        event NoteOffMessageHandler NoteOff;

        /// <summary>
        /// Note On event.
        /// </summary>
        event NoteOnMessageHandler NoteOn;

        /// <summary>
        /// Polyphonic Key Pressure (Aftertouch).
        /// </summary>
        event PolyphonicKeyPressureMessageHandler PolyphonicKeyPressure;

        /// <summary>
        /// Control Change.
        /// </summary>
        event ControlChangeMessageHandler ControlChange;

        /// <summary>
        /// Program Change.
        /// </summary>
        event ProgramChangeMessageHandler ProgramChange;

        /// <summary>
        /// Channel Pressure (After-touch).
        /// </summary>
        event ChannelPressureMessageHandler ChannelPressure;

        /// <summary>
        /// Pitch Bend Change.
        /// </summary>
        event PitchBendMessageHandler PitchBend;

        /// <summary>
        /// Non-Registered Parameter Number (NRPN) event
        /// </summary>
        event NrpnMessageHandler Nrpn;

        /// <summary>
        /// Set NRPN interpretation mode
        /// </summary>
        /// <param name="mode">Mode</param>
        void SetNrpnMode(NrpnMode mode);

        /// <summary>
        /// SysEx data message event.
        /// </summary>
        event SysExMessageHandler SysEx;

        /// <summary>
        /// MIDI Time Code Quarter Frame event
        /// </summary>
        event MidiTimeCodeQuarterFrameHandler MidiTimeCodeQuarterFrame;

        /// <summary>
        /// Song Position Pointer event
        /// </summary>
        event SongPositionPointerHandler SongPositionPointer;

        /// <summary>
        /// Song Select event
        /// </summary>
        event SongSelectHandler SongSelect;

        /// <summary>
        /// Tune Request event
        /// </summary>
        event TuneRequestHandler TuneRequest;
    }

    public delegate void NoteOffMessageHandler(IMidiInputDevice sender, in NoteOffMessage msg);

    public delegate void NoteOnMessageHandler(IMidiInputDevice sender, in NoteOnMessage msg);
    
    public delegate void PolyphonicKeyPressureMessageHandler(IMidiInputDevice sender, in PolyphonicKeyPressureMessage msg);

    public delegate void ControlChangeMessageHandler(IMidiInputDevice sender, in ControlChangeMessage msg);

    public delegate void ProgramChangeMessageHandler(IMidiInputDevice sender, in ProgramChangeMessage msg);

    public delegate void ChannelPressureMessageHandler(IMidiInputDevice sender, in ChannelPressureMessage msg);

    public delegate void PitchBendMessageHandler(IMidiInputDevice sender, in PitchBendMessage msg);

    public delegate void NrpnMessageHandler(IMidiInputDevice sender, in NrpnMessage msg);

    public delegate void SysExMessageHandler(IMidiInputDevice sender, in SysExMessage msg);
    
    public delegate void MidiTimeCodeQuarterFrameHandler(IMidiInputDevice sender, in MidiTimeCodeQuarterFrameMessage msg);

    public delegate void SongPositionPointerHandler(IMidiInputDevice sender, in SongPositionPointerMessage msg);

    public delegate void SongSelectHandler(IMidiInputDevice sender, in SongSelectMessage msg);

    public delegate void TuneRequestHandler(IMidiInputDevice sender, in TuneRequestMessage msg);
}
