namespace RtMidi.Core.Unmanaged.Devices
{
    public class RtMidiDeviceInfo
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
