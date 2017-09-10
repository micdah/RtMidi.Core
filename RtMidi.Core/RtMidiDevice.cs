using System;
using System.Runtime.InteropServices;
using RtMidiPtr = System.IntPtr;

namespace RtMidi.Core
{
    public abstract class RtMidiDevice : IDisposable
    {
        // no idea when to use it...
        public static void Error(RtMidiErrorType errorType, string message)
        {
            RtMidi.rtmidi_error(errorType, message);
        }

        public static RtMidiApi[] GetAvailableApis()
        {
            int enumSize = RtMidi.rtmidi_sizeof_rtmidi_api();
            IntPtr ptr = IntPtr.Zero;
            int size = RtMidi.rtmidi_get_compiled_api(ref ptr);
            ptr = Marshal.AllocHGlobal(size * enumSize);
            RtMidi.rtmidi_get_compiled_api(ref ptr);
            RtMidiApi[] ret = new RtMidiApi[size];
            switch (enumSize)
            {
                case 1:
                    byte[] bytes = new byte[size];
                    Marshal.Copy(ptr, bytes, 0, bytes.Length);
                    for (int i = 0; i < bytes.Length; i++)
                        ret[i] = (RtMidiApi)bytes[i];
                    break;
                case 2:
                    short[] shorts = new short[size];
                    Marshal.Copy(ptr, shorts, 0, shorts.Length);
                    for (int i = 0; i < shorts.Length; i++)
                        ret[i] = (RtMidiApi)shorts[i];
                    break;
                case 4:
                    int[] ints = new int[size];
                    Marshal.Copy(ptr, ints, 0, ints.Length);
                    for (int i = 0; i < ints.Length; i++)
                        ret[i] = (RtMidiApi)ints[i];
                    break;
                case 8:
                    long[] longs = new long[size];
                    Marshal.Copy(ptr, longs, 0, longs.Length);
                    for (int i = 0; i < longs.Length; i++)
                        ret[i] = (RtMidiApi)longs[i];
                    break;
                default:
                    throw new NotSupportedException("sizeof RtMidiApi is unexpected: " + enumSize);
            }
            return ret;
        }

        RtMidiPtr handle;
        bool is_port_open;

        protected RtMidiDevice(RtMidiPtr handle)
        {
            this.handle = handle;
        }

        public RtMidiPtr Handle
        {
            get { return handle; }
        }

        public int PortCount
        {
            get { return (int)RtMidi.rtmidi_get_port_count(handle); }
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (is_port_open)
            {
                is_port_open = false;
                RtMidi.rtmidi_close_port(handle);
            }
            ReleaseDevice();
        }

        public string GetPortName(int portNumber)
        {
            return RtMidi.rtmidi_get_port_name(handle, (uint)portNumber);
        }

        public void OpenVirtualPort(string portName)
        {
            try
            {
                RtMidi.rtmidi_open_virtual_port(handle, portName);
            }
            finally
            {
                is_port_open = true;
            }
        }

        public void OpenPort(int portNumber, string portName)
        {
            try
            {
                RtMidi.rtmidi_open_port(handle, (uint)portNumber, portName);
            }
            finally
            {
                is_port_open = true;
            }
        }

        protected abstract void ReleaseDevice();

        public abstract RtMidiApi CurrentApi { get; }
    }
}
