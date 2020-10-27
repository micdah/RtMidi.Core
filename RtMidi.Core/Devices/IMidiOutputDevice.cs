using RtMidi.Core.Messages;

namespace RtMidi.Core.Devices
{
    public interface IMidiOutputDevice : IMidiDevice
    {
        /// <summary>
        /// Send Note Off message
        /// </summary>
        /// <param name="noteOffMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in NoteOffMessage noteOffMessage);

        /// <summary>
        /// Send Note On message
        /// </summary>
        /// <param name="noteOnMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in NoteOnMessage noteOnMessage);

        /// <summary>
        /// Send Polyphonic Key Pressure message
        /// </summary>
        /// <param name="polyphonicKeyPressureMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in PolyphonicKeyPressureMessage polyphonicKeyPressureMessage);

        /// <summary>
        /// Send Control Change message
        /// </summary>
        /// <param name="controlChangeMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in ControlChangeMessage controlChangeMessage);

        /// <summary>
        /// Send Program Change message
        /// </summary>
        /// <param name="programChangeMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in ProgramChangeMessage programChangeMessage);

        /// <summary>
        /// Send Channel Pressure message
        /// </summary>
        /// <param name="channelPressureMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in ChannelPressureMessage channelPressureMessage);

        /// <summary>
        /// Send Pitch Bend message
        /// </summary>
        /// <param name="pitchBendMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in PitchBendMessage pitchBendMessage);

        /// <summary>
        /// Send Non-Registered Parameter Number message
        /// </summary>
        /// <param name="nrpnMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in NrpnMessage nrpnMessage);

        /// <summary>
        /// Send SysEx message
        /// </summary>
        /// <param name="sysExMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in SysExMessage sysExMessage);

        /// <summary>
        /// Send MIDI Time Code Quarter Frame message
        /// </summary>
        /// <param name="midiTimeCodeQuarterFrameMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in MidiTimeCodeQuarterFrameMessage midiTimeCodeQuarterFrameMessage);

        /// <summary>
        /// Send Song Position Pointer message
        /// </summary>
        /// <param name="songPositionPointerMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in SongPositionPointerMessage songPositionPointerMessage);

        /// <summary>
        /// Send Song Select message
        /// </summary>
        /// <param name="songSelectMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in SongSelectMessage songSelectMessage);

        /// <summary>
        /// Send Tune Request message
        /// </summary>
        /// <param name="tuneRequestMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(in TuneRequestMessage tuneRequestMessage);
    }
}