using RtMidi.Core.Unmanaged.Devices;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RtMidi.Core.Tests")]

namespace RtMidi.Core
{
    internal class MidiInputDevice : MidiDevice<IRtMidiInputDevice>
    {
        public MidiInputDevice(IRtMidiInputDevice rtMidiDevice) : base(rtMidiDevice)
        {
        }
    }
}
