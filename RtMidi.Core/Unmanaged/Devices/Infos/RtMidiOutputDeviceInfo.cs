namespace RtMidi.Core.Unmanaged.Devices.Infos
{
    internal class RtMidiOutputDeviceInfo : RtMidiDeviceInfo
    {
        internal RtMidiOutputDeviceInfo(uint port, string name) : base(port, name)
        {
        }

        public IRtMidiOutputDevice CreateDevice()
        {
            return new RtMidiOutputDevice(Port);
        }
    }
}
