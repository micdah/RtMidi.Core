using System;
using System.Collections.Generic;
using RtMidi.Core.Unmanaged.Devices;
using RtMidi.Core.Unmanaged.Devices.Infos;

namespace RtMidi.Core.Unmanaged
{
    internal class RtMidiDeviceManager : IDisposable
    {
        public static RtMidiDeviceManager Default => DefaultHolder.Value;

        public static readonly Lazy<RtMidiDeviceManager> DefaultHolder = new(() => new());

        private readonly RtMidiInputDevice _defaultInputDevice;
        private readonly RtMidiOutputDevice _defaultOutputDevice;
        private bool _disposed;

        private RtMidiDeviceManager()
        {
            /*
             * These are used exlusively to get number of available ports for the given
             * type of device (input/output) as well as to provide port names
             */
            _defaultInputDevice = new(0);
            _defaultOutputDevice = new(0);
        }

        ~RtMidiDeviceManager() 
        {
            Dispose();
        }

        /// <summary>
        /// Enumerate all currently available input devices
        /// </summary>
        public IEnumerable<RtMidiInputDeviceInfo> InputDevices
        {
            get
            {
                for (uint port = 0; port < _defaultInputDevice.GetPortCount(); port++)
                {
                    yield return new(port, _defaultInputDevice.GetPortName(port));
                }
            }
        }

        /// <summary>
        /// Enumerate all currently available output devices
        /// </summary>
        public IEnumerable<RtMidiOutputDeviceInfo> OutputDevices
        {
            get 
            {
                for (uint port = 0; port < _defaultOutputDevice.GetPortCount(); port++) 
                {
                    yield return new(port, _defaultOutputDevice.GetPortName(port));
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _defaultInputDevice.Dispose();
            _defaultOutputDevice.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
