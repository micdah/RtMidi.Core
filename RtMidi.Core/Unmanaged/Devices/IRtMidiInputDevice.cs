using System;
namespace RtMidi.Core.Unmanaged.Devices
{
    /// <summary>
    /// RtMidi Input device
    /// </summary>
    internal interface IRtMidiInputDevice : IRtMidiDevice
    {
        /// <summary>
        /// MIDI message was received
        /// </summary>
        event EventHandler<byte[]> Message;
    }
}
