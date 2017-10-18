using RtMidi.Core.Messages;
using System;

namespace RtMidi.Core.Devices
{
    public interface IMidiInputDevice : IMidiDevice
    {
        /// <summary>
        /// Note Off event.
        /// </summary>
        event EventHandler<NoteOffMessage> NoteOff;

        /// <summary>
        /// Note On event.
        /// </summary>
        event EventHandler<NoteOnMessage> NoteOn;

        /// <summary>
        /// Polyphonic Key Pressure (Aftertouch).
        /// </summary>
        event EventHandler<PolyphonicKeyPressureMessage> PolyphonicKeyPressure;

        /// <summary>
        /// Control Change.
        /// </summary>
        event EventHandler<ControlChangeMessage> ControlChange;

        /// <summary>
        /// Program Change.
        /// </summary>
        event EventHandler<ProgramChangeMessage> ProgramChange;

        /// <summary>
        /// Channel Pressure (After-touch).
        /// </summary>
        event EventHandler<ChannelPressureMessage> ChannelPressure;

        /// <summary>
        /// Pitch Bend Change.
        /// </summary>
        event EventHandler<PitchBendMessage> PitchBend;

        /// <summary>
        /// Non-Registered Parameter Number (NRPN) event
        /// </summary>
        event EventHandler<NRPNMessage> NRPN;
    }
}
