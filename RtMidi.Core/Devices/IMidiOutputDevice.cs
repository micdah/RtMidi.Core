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
        bool Send(NoteOffMessage noteOffMessage);

        /// <summary>
        /// Send Note On message
        /// </summary>
        /// <param name="noteOnMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(NoteOnMessage noteOnMessage);

        /// <summary>
        /// Send Polyphonic Key Pressure message
        /// </summary>
        /// <param name="polyphonicKeyPressureMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(PolyphonicKeyPressureMessage polyphonicKeyPressureMessage);

        /// <summary>
        /// Send Control Change message
        /// </summary>
        /// <param name="controlChangeMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(ControlChangeMessage controlChangeMessage);

        /// <summary>
        /// Send Program Change message
        /// </summary>
        /// <param name="programChangeMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(ProgramChangeMessage programChangeMessage);

        /// <summary>
        /// Send Channel Pressure message
        /// </summary>
        /// <param name="channelPressureMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(ChannelPressureMessage channelPressureMessage);

        /// <summary>
        /// Send Pitch Bend message
        /// </summary>
        /// <param name="pitchBendMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(PitchBendMessage pitchBendMessage);

        /// <summary>
        /// Send Non-Registered Parameter Number message
        /// </summary>
        /// <param name="nrpnMessage"></param>
        /// <returns>True if sent, false otherwise</returns>
        bool Send(NrpnMessage nrpnMessage);
    }
}