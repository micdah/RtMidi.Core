using System;
using System.Runtime.InteropServices;

namespace RtMidi.Core
{
    public class RtMidiInputDevice : RtMidiDevice
    {
        public RtMidiInputDevice()
            : base(RtMidi.rtmidi_in_create_default())
        {
        }

        public RtMidiInputDevice(RtMidiApi api, string clientName, int queueSizeLimit = 100)
            : base(RtMidi.rtmidi_in_create(api, clientName, (uint)queueSizeLimit))
        {
        }

        public override RtMidiApi CurrentApi
        {
            get { return RtMidi.rtmidi_in_get_current_api(Handle); }
        }

        protected override void ReleaseDevice()
        {
            RtMidi.rtmidi_in_free(Handle);
        }

        public void SetCallback(RtMidiCallback callback, IntPtr userData)
        {
            RtMidi.rtmidi_in_set_callback(Handle, callback, userData);
        }

        public void CancelCallback()
        {
            RtMidi.rtmidi_in_cancel_callback(Handle);
        }

        public void SetIgnoredTypes(bool midiSysex, bool midiTime, bool midiSense)
        {
            RtMidi.rtmidi_in_ignore_types(Handle, midiSysex, midiTime, midiSense);
        }

        public byte[] GetMessage()
        {
            UIntPtr length = UIntPtr.Zero;
            int size = (int)RtMidi.rtmidi_in_get_message(Handle, out IntPtr ptr, ref length);
            byte[] buf = new byte[size];
            Marshal.Copy(ptr, buf, 0, size);
            return buf;
        }
    }
}
