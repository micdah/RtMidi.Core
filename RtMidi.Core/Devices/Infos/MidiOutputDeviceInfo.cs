using RtMidi.Core.Unmanaged.Devices;
namespace RtMidi.Core.Devices.Infos
{
    internal class MidiOutputDeviceInfo : MidiDeviceInfo<RtMidiOutputDeviceInfo>, IMidiOutputDeviceInfo
    {
        public MidiOutputDeviceInfo(RtMidiOutputDeviceInfo rtMidiDeviceInfo) : base(rtMidiDeviceInfo)
        {
        }

        public IMidiOutputDevice CreateDevice()
        {
            return new MidiOutputDevice(RtMidiDeviceInfo.CreateDevice());
        }
    }
}
