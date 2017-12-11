namespace RtMidi.Core.Unmanaged.Devices.Infos
{
    internal class RtMidiInputDeviceInfo : RtMidiDeviceInfo
    {
        internal RtMidiInputDeviceInfo(uint port, string name) : base(port, name)
        {
        }

        public IRtMidiInputDevice CreateDevice()
        {
            return new RtMidiInputDevice(Port);
        }
    }
}
