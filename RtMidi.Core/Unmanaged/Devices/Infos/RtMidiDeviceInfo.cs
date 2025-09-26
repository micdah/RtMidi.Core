using System;

namespace RtMidi.Core.Unmanaged.Devices.Infos
{
    internal class RtMidiDeviceInfo
    {
        internal RtMidiDeviceInfo(uint port, string name)
        {
            Port = port;
            Name = name;
        }

        public uint Port { get; }
        public string Name { get; }
    }
}
