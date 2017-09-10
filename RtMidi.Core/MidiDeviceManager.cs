using System;
using System.Collections.Generic;

namespace RtMidi.Core
{
    public static class MidiDeviceManager
    {
        static readonly RtMidiOutputDevice manager_output = new RtMidiOutputDevice();
        static readonly RtMidiInputDevice manager_input = new RtMidiInputDevice();

        // OK, it is not really a device count. But RTMIDI is designed to have bad names enough
        // to enumerate APIs as DEVICEs.
        public static int DeviceCount
        {
            get { return manager_input.PortCount + manager_output.PortCount; }
        }

        public static int DefaultInputDeviceID
        {
            get { return 0; }
        }

        public static int DefaultOutputDeviceID
        {
            get { return manager_input.PortCount; }
        }

        public static IEnumerable<MidiDeviceInfo> AllDevices
        {
            get
            {
                for (int i = 0; i < DeviceCount; i++)
                    yield return GetDeviceInfo(i);
            }
        }

        public static MidiDeviceInfo GetDeviceInfo(int id)
        {
            return id < manager_input.PortCount ? new MidiDeviceInfo(manager_input, id, id, true) : new MidiDeviceInfo(manager_output, id, id - manager_input.PortCount, false);
        }

        public static RtMidiInputDevice OpenInput(int deviceID)
        {
            var dev = new RtMidiInputDevice();
            dev.OpenPort(deviceID, GetDeviceInfo(deviceID).Name);
            return dev;
        }

        public static RtMidiOutputDevice OpenOutput(int deviceID)
        {
            var dev = new RtMidiOutputDevice();
            dev.OpenPort(deviceID - manager_input.PortCount, GetDeviceInfo(deviceID).Name);
            return dev;
        }
    }
}
