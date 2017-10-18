using System;
using RtMidi.Core.Unmanaged.Devices;

namespace RtMidi.Core.Tests
{
    internal class RtMidiInputDeviceMock : IRtMidiInputDevice
    {
        public bool IsOpen => true;

        public event EventHandler<byte[]> Message;

        public void OnMessage(byte[] message) => Message?.Invoke(this, message);

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public bool Open() => true;
    }
}