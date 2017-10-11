using RtMidi.Core.Unmanaged.Devices;
using System;

namespace RtMidi.Core.Devices
{

    internal abstract class MidiDevice : IMidiDevice 
    {
        private readonly IRtMidiDevice _rtMidiDevice;
        private bool _disposed;

        protected MidiDevice(IRtMidiDevice rtMidiDevice)
        {
            _rtMidiDevice = rtMidiDevice ?? throw new ArgumentNullException(nameof(rtMidiDevice));
        }

        public bool IsOpen => _rtMidiDevice.IsOpen;
        public bool Open() => _rtMidiDevice.Open();
        public void Close() => _rtMidiDevice.Close();

        public void Dispose()
        {
            if (_disposed) return;

            try {
                Disposing();
                _rtMidiDevice.Dispose();
            }
            finally
            {
                _disposed = true;
            }
        }

        protected virtual void Disposing()
        {
        }
    }
}
