using System;
namespace RtMidi.Core.Unmanaged.Devices
{
    public interface IRtMidiDevice : IDisposable
    {
        /// <summary>
        /// Whether or not the device is open
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Open device port
        /// </summary>
        /// <returns>True if port was opened, false otherwise</returns>
        bool Open();

        /// <summary>
        /// Close device port (if already open, <see cref="IsOpen"/>)
        /// </summary>
        void Close();
    }
}
