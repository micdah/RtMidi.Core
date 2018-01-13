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
        /// Set NRPN interpretation mode
        /// </summary>
        /// <param name="mode">Mode</param>
        void SetNrpnMode(NrpnMode mode);
    }

    public delegate void NoteOffMessageHandler(IMidiInputDevice sender, in NoteOffMessage msg);

    public delegate void NoteOnMessageHandler(IMidiInputDevice sender, in NoteOnMessage msg);
    
    public delegate void PolyphonicKeyPressureMessageHandler(IMidiInputDevice sender, in PolyphonicKeyPressureMessage msg);

    public delegate void ControlChangeMessageHandler(IMidiInputDevice sender, in ControlChangeMessage msg);

    public delegate void ProgramChangeMessageHandler(IMidiInputDevice sender, in ProgramChangeMessage msg);

    public delegate void ChannelPressureMessageHandler(IMidiInputDevice sender, in ChannelPressureMessage msg);

    public delegate void PitchBendMessageHandler(IMidiInputDevice sender, in PitchBendMessage msg);

    public delegate void NrpnMessageHandler(IMidiInputDevice sender, in NrpnMessage msg);
}
