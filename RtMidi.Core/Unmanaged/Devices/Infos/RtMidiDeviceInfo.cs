using System;
using System.Runtime.InteropServices;

namespace RtMidi.Core.Unmanaged.Devices.Infos
{
    internal class RtMidiDeviceInfo
    {
        internal RtMidiDeviceInfo(uint port, string name)
        {
            Port = port;

            // RtMidi may add port number to end of name on Windows to ensure uniqueness
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
               {
                Name = name.EndsWith(port.ToString())
                ? name.Substring(0, name.LastIndexOf(port.ToString(), StringComparison.Ordinal) - 1)
                : name;
            }
            else { Name = name; }
        }

        public uint Port { get; }
        public string Name { get; }
    }
}
