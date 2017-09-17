using RtMidi.Core.Unmanaged.Devices;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RtMidi.Core.Tests")]

namespace RtMidi.Core
{

    internal abstract class MidiDevice<TRtMidiDevice> : IMidiDevice 
        where TRtMidiDevice : class, IRtMidiDevice
    {
        protected readonly TRtMidiDevice RtMidiDevice;

        protected MidiDevice(TRtMidiDevice rtMidiDevice)
        {
            RtMidiDevice = rtMidiDevice ?? throw new ArgumentNullException(nameof(rtMidiDevice));
        }

        public bool IsOpen => RtMidiDevice.IsOpen;
        public bool Open() => RtMidiDevice.Open();
        public void Close() => RtMidiDevice.Close();
    }
}
