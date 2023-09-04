using System.Runtime.CompilerServices;
using System.Collections.Generic;
using RtMidi.Core.Unmanaged;
using RtMidi.Core.Devices.Infos;
using RtMidi.Core.Unmanaged.API;
using System;

[assembly: InternalsVisibleTo("RtMidi.Core.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace RtMidi.Core
{
    /// <summary>
    /// This is the MIDI Device Manager, which you shall use to obtain information
    /// about all available input and output devices, information is always up-to-date
    /// so each time you enumerate input or output devices, it will reflect the 
    /// currently available devices.
    /// </summary>
    public sealed class MidiDeviceManager : IDisposable
    {
        /// <summary>
        /// Manager singleton instance to use
        /// </summary>
        public static MidiDeviceManager Default => DefaultHolder.Value;

        private static readonly Lazy<MidiDeviceManager> DefaultHolder = new(() => new());

        private readonly RtMidiDeviceManager _rtDeviceManager;
        private bool _disposed;

        private MidiDeviceManager()
        {
            _rtDeviceManager = RtMidiDeviceManager.Default;
        }

        ~MidiDeviceManager() 
        {
            Dispose();
        }

        /// <summary>
        /// Enumerate all currently available input devices
        /// </summary>
        public IEnumerable<IMidiInputDeviceInfo> InputDevices 
        {
            get
            {
                foreach (var rtInputDeviceInfo in _rtDeviceManager.InputDevices) 
                {
                    yield return new MidiInputDeviceInfo(rtInputDeviceInfo);
                }
            }
        }

        /// <summary>
        /// Enumerate all currently available output devices
        /// </summary>
        public IEnumerable<IMidiOutputDeviceInfo> OutputDevices
        {
            get 
            {
                foreach (var rtOutputDeviceInfo in _rtDeviceManager.OutputDevices) 
                {
                    yield return new MidiOutputDeviceInfo(rtOutputDeviceInfo);
                }
            }
        }

        /// <summary>
        /// Get the available MIDI API's used by RtMidi (if any)
        /// </summary>
        /// <returns>The available midi apis.</returns>
        public IEnumerable<RtMidiApi> GetAvailableMidiApis()
        {
            return RtMidiApiManager.GetAvailableApis();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _rtDeviceManager.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
