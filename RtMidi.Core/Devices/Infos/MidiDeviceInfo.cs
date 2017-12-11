using System;
using RtMidi.Core.Unmanaged.Devices.Infos;

namespace RtMidi.Core.Devices.Infos
{
    internal class MidiDeviceInfo<TRtMidiDeviceInfo> : IMidiDeviceInfo
        where TRtMidiDeviceInfo : RtMidiDeviceInfo
    {
        protected readonly TRtMidiDeviceInfo RtMidiDeviceInfo;

        public MidiDeviceInfo(TRtMidiDeviceInfo rtMidiDeviceInfo)
        {
            RtMidiDeviceInfo = rtMidiDeviceInfo ?? throw new ArgumentNullException(nameof(rtMidiDeviceInfo));
        }

        public string Name => RtMidiDeviceInfo.Name;
    }
}
