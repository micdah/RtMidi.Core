using System;
using RtMidi.Core.Unmanaged.API;
using Serilog;
namespace RtMidi.Core.Unmanaged.Devices
{

    /// <summary>
    /// Abstract RtMidi device base class
    /// </summary>
    internal abstract class RtMidiDevice : IRtMidiDevice
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<RtMidiDevice>();

        private IntPtr _handle;
        private readonly uint _portNumber;
        private bool _disposed;
        private bool _isOpen;

        protected RtMidiDevice(uint portNumber) 
        {
            _handle = IntPtr.Zero;
            _portNumber = portNumber;
        }

        ~RtMidiDevice() 
        {
            // Ensure unmanaged handles are freed
            Dispose();
        }

        public bool IsOpen => _isOpen;

        /// <summary>
        /// Read-only access to the unmanaged device handle
        /// </summary>
        protected IntPtr Handle => _handle;

        public bool Open() 
        {
            if (_isOpen) return false;

            if (!EnsureDevice())
            {
                Log.Debug("Could not create device handle, cannot open port {PortNumber}", _portNumber);
                return false;
            }

            try
            {
                Log.Debug("Feching port name, for port {PortNumber}", _portNumber);
                var portName = RtMidiC.GetPortName(_handle, _portNumber);

                Log.Debug("Opening port {PortNumber} using name {PortName}", _portNumber, portName);
                RtMidiC.OpenPort(_handle, _portNumber, portName);

                _isOpen = true;

                return true;
            }
            catch (Exception e) 
            {
                Log.Error(e, "Unable to open port number {PortNumber}", _portNumber);
                return false;
            }
        }

        public void Close()
        {
            if (!_isOpen) return;

            try
            {
                Log.Debug("Closing port number {PortNumber}", _portNumber);
                RtMidiC.ClosePort(_handle);
                _isOpen = false;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to close port number {PortNumber}", _portNumber);
            }
        }

        /// <summary>
        /// Get number of available ports for this device type
        /// </summary>
        /// <returns>Number of ports</returns>
        internal uint GetPortCount()
        {
            if (!EnsureDevice()) return 0;

            try
            {
                return RtMidiC.GetPortCount(_handle);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while getting number of ports");
                return 0;
            }
        }

        /// <summary>
        /// Get name of port, for this device type
        /// </summary>
        /// <returns>The port name.</returns>
        /// <param name="portNumber">Port number.</param>
        internal string GetPortName(uint portNumber) 
        {
            if (!EnsureDevice()) return null;

            try 
            {
                return RtMidiC.GetPortName(_handle, portNumber);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while getting port {PortNumber} name", portNumber);
                return null;
            }
        }

        private bool EnsureDevice()
        {
            if (_handle != IntPtr.Zero) return true;

            _handle = CreateDevice();

            return _handle != IntPtr.Zero;
        }

        public void Dispose() 
        {
            if (_disposed) return;

            // Ensure device is closed
            if (_isOpen) 
            {
                Close();
            }

            // Ensure device is destroyed
            if (_handle != IntPtr.Zero) 
            {
                DestroyDevice();
            }

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        protected abstract IntPtr CreateDevice();
        protected abstract void DestroyDevice();
    }
}
