namespace RtMidi.Core.Unmanaged.Devices
{
    /// <summary>
    /// RtMidi Output device
    /// </summary>
    public interface IRtMidiOutputDevice : IRtMidiDevice
    {
        /// <summary>
        /// Send MIDI message to output device
        /// </summary>
        /// <remarks>
        /// May only be called when the device is open (<see cref="IsOpen"/>)
        /// </remarks>
        /// <param name="message">Message bytes</param>
        /// <returns><c>true</c>, if message was sent, <c>false</c> otherwise.</returns>
        bool SendMessage(byte[] message);
    }
}
