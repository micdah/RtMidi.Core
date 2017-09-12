using System.Collections.Generic;

namespace RtMidi.Core.Unmanaged.Devices
{
    public static class RtMidiDeviceManager
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

        public static IEnumerable<RtMidiDeviceInfo> AllDevices
        {
            get
            {
                for (int i = 0; i < DeviceCount; i++)
                    yield return GetDeviceInfo(i);
            }
        }

        public static RtMidiDeviceInfo GetDeviceInfo(int id)
        {
            return id < manager_input.PortCount ? new RtMidiDeviceInfo(manager_input, id, id, true) : new RtMidiDeviceInfo(manager_output, id, id - manager_input.PortCount, false);
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

/**
 * This is a derived work, based on https://github.com/atsushieno/managed-midi
 * 
 * Copyright (c) 2010 Atsushi Eno
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 **/