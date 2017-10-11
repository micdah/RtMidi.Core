using RtMidi.Core.Unmanaged.Devices;
using System;
namespace RtMidi.Core.Devices.Infos
{
    internal class MidiDeviceInfo<TRtMidiDeviceInfo> : IMidiDeviceInfo
        where TRtMidiDeviceInfo : RtMidiDeviceInfo
    {
        protected readonly TRtMidiDeviceInfo RtMidiDeviceInfo;

        public MidiDeviceInfo(TRtMidiDeviceInfo rtMidiDeviceInfo)
        {
            if (rtMidiDeviceInfo == null) throw new ArgumentNullException(nameof(rtMidiDeviceInfo));

            RtMidiDeviceInfo = rtMidiDeviceInfo;
        }

        public string Name => RtMidiDeviceInfo.Name;
    }
}
