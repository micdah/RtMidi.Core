using RtMidi.Core.Unmanaged.Devices;
using System;
namespace RtMidi.Core
{
    public class MidiDeviceManager
    {
        public static readonly MidiDeviceManager Instance = new MidiDeviceManager();

        public MidiDeviceManager()
        {
        }
    }

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

    public interface IMidiInputDeviceInfo
    {
        IMidiInputDevice CreateDevice();
    }

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
