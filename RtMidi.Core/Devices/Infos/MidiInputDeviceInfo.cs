using RtMidi.Core.Unmanaged.Devices.Infos;
namespace RtMidi.Core.Devices.Infos
{
    internal class MidiInputDeviceInfo : MidiDeviceInfo<RtMidiInputDeviceInfo>, IMidiInputDeviceInfo
    {
        public MidiInputDeviceInfo(RtMidiInputDeviceInfo rtMidiDeviceInfo) : base(rtMidiDeviceInfo)
        {
        }

        public IMidiInputDevice CreateDevice()
        {
            return new MidiInputDevice(RtMidiDeviceInfo.CreateDevice());
        }
    }
}
