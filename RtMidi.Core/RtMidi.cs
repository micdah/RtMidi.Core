using System;
using System.Runtime.InteropServices;
using RtMidiPtr = System.IntPtr;
using RtMidiInPtr = System.IntPtr;
using RtMidiOutPtr = System.IntPtr;

namespace RtMidi.Core
{
    public delegate void RtMidiCallback(double timestamp, string message, IntPtr userData);

    public static class RtMidi
    {
        public const string RtMidiLibrary = "rtmidi";

        /* Utility API */
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_sizeof_rtmidi_api();

        /* RtMidi API */
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_get_compiled_api(ref IntPtr/* RtMidiApi ** */ apis);
        // return length for NULL argument.
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_error(RtMidiErrorType type, string errorString);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_open_port(RtMidiPtr device, uint portNumber, string portName);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_open_virtual_port(RtMidiPtr device, string portName);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_close_port(RtMidiPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_get_port_count(RtMidiPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        static extern internal string rtmidi_get_port_name(RtMidiPtr device, uint portNumber);

        /* RtMidiIn API */
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiInPtr rtmidi_in_create_default();

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiInPtr rtmidi_in_create(RtMidiApi api, string clientName, uint queueSizeLimit);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_free(RtMidiInPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiApi rtmidi_in_get_current_api(RtMidiPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_set_callback(RtMidiInPtr device, RtMidiCallback callback, IntPtr userData);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_cancel_callback(RtMidiInPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_ignore_types(RtMidiInPtr device, bool midiSysex, bool midiTime, bool midiSense);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal double rtmidi_in_get_message(RtMidiInPtr device, /* unsigned char ** */out IntPtr message);

        /* RtMidiOut API */
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiOutPtr rtmidi_out_create_default();

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiOutPtr rtmidi_out_create(RtMidiApi api, string clientName);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_out_free(RtMidiOutPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiApi rtmidi_out_get_current_api(RtMidiPtr device);

        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_out_send_message(RtMidiOutPtr device, byte[] message, int length);
    }
}
