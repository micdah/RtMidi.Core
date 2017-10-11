using System;
using System.Collections.Generic;
using RtMidi.Core.Unmanaged.Devices;

namespace RtMidi.Core.Unmanaged
{
    // TODO Change visibility to internal
    public class RtMidiDeviceManager : IDisposable
    {
        public static readonly RtMidiDeviceManager Instance = new RtMidiDeviceManager();

        private readonly RtMidiInputDevice _defaultInputDevice;
        private readonly RtMidiOutputDevice _defaultOutputDevice;
        private bool _disposed;

        private RtMidiDeviceManager()
        {
            /*
             * These are used exlusively to get number of available ports for the given
             * type of device (input/output) as well as to provide port names
             */
            _defaultInputDevice = new RtMidiInputDevice(0);
            _defaultOutputDevice = new RtMidiOutputDevice(0);
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
                    yield return new RtMidiInputDeviceInfo(port, _defaultInputDevice.GetPortName(port));
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
                    yield return new RtMidiOutputDeviceInfo(port, _defaultOutputDevice.GetPortName(port));
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
