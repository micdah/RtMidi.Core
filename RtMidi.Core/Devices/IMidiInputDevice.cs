using RtMidi.Core.Messages;
using System;
using RtMidi.Core.Devices.Nrpn;

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
        /// Clock event
        /// </summary>
        event ClockMessageHandler Clock;

        /// <summary>
        /// Start event
        /// </summary>
        event StartMessageHandler Start;

        /// <summary>
        /// Stop event
        /// </summary>
        event StopMessageHandler Stop;

        /// <summary>
        /// Continue event
        /// </summary>
        event ContinueMessageHandler Continue;

        /// <summary>
        /// Song Position Pointer event
        /// </summary>
        event SongPositionPointerMessageHandler SongPositionPointer;
        
        /// <summary>
        /// Set NRPN interpretation mode
        /// </summary>
        /// <param name="mode">Mode</param>
        void SetNrpnMode(NrpnMode mode);

        /// <summary>
        /// SysEx data message event.
        /// </summary>
        event SysExMessageHandler SysEx;
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
    
    public delegate void ClockMessageHandler(IMidiInputDevice sender, in ClockMessage msg);
    
    public delegate void StartMessageHandler(IMidiInputDevice sender, in StartMessage msg);
    
    public delegate void StopMessageHandler(IMidiInputDevice sender, in StopMessage msg);
    
    public delegate void ContinueMessageHandler(IMidiInputDevice sender, in ContinueMessage msg);
    
    public delegate void SongPositionPointerMessageHandler(IMidiInputDevice sender, in SongPositionPointerMessage msg);
}
