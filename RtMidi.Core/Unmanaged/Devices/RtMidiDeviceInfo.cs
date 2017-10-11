namespace RtMidi.Core.Unmanaged.Devices
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
