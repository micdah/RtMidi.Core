using System;
using System.Runtime.InteropServices;
using RtMidiPtr = System.IntPtr;
using RtMidiInPtr = System.IntPtr;
using RtMidiOutPtr = System.IntPtr;

namespace RtMidi.Core
{
    /// <summary>
    /// The type of a RtMidi callback function.
    /// </summary>
    public delegate void RtMidiCallback(double timestamp, IntPtr message, UIntPtr messageSize, IntPtr userData);

    public static class RtMidi
    {
        public const string RtMidiLibrary = "rtmidi";

        #region Utility API

        /// <summary>
        /// Returns the size (with sizeof) of a RtMidiApi instance.
        /// </summary>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_sizeof_rtmidi_api();

        #endregion

        #region RtMidi API

        /// <summary>
        /// Determine the available compiled MIDI APIs.
        /// If the given `apis` parameter is null, returns the number of available APIs.
        /// Otherwise, fill the given apis array with the RtMidi::Api values.
        /// </summary>
        /// <param name="apis">An array or a null value.</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal int rtmidi_get_compiled_api(ref IntPtr/* RtMidiApi ** */ apis);

        /// <summary>
        /// Report an error.
        /// </summary>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_error(RtMidiErrorType type, string errorString);

        /// <summary>
        /// Open a MIDI port.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Must be greater than 0</param>
        /// <param name="portName">Name for the application port.x</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_open_port(RtMidiPtr device, uint portNumber, string portName);

        /// <summary>
        /// Creates a virtual MIDI port to which other software applications can 
        /// connect.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portName"> Name for the application port.</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_open_virtual_port(RtMidiPtr device, string portName);

        /// <summary>
        /// Close a MIDI connection.
        /// </summary>
        /// <param name="device">Device to close</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_close_port(RtMidiPtr device);

        /// <summary>
        /// Return the number of available MIDI ports.
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal uint rtmidi_get_port_count(RtMidiPtr device);

        /// <summary>
        /// Return a string identifier for the specified MIDI input port number.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Port number</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        static extern internal string rtmidi_get_port_name(RtMidiPtr device, uint portNumber);

        #endregion

        #region RtMidiIn API

        /// <summary>
        /// Create a default RtMidiInPtr value, with no initialization.
        /// </summary>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiInPtr rtmidi_in_create_default();

        /// <summary>
        /// Create a  RtMidiInPtr value, with given api, clientName and queueSizeLimit.
        /// </summary>
        /// <param name="api">An optional API id can be specified.</param>
        /// <param name="clientName">
        /// An optional client name can be specified. This will be used to group the ports that 
        /// are created by the application.
        /// </param>
        /// <param name="queueSizeLimit">An optional size of the MIDI input queue can be specified.</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiInPtr rtmidi_in_create(RtMidiApi api, string clientName, uint queueSizeLimit);

        /// <summary>
        /// Deallocate the given pointer.
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_free(RtMidiInPtr device);

        /// <summary>
        /// Returns the MIDI API specifier for the given instance of RtMidiIn.
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal RtMidiApi rtmidi_in_get_current_api(RtMidiPtr device);

        /// <summary>
        /// Set a callback function to be invoked for incoming MIDI messages.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="callback">Callback</param>
        /// <param name="userData">User data</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_set_callback(RtMidiInPtr device, RtMidiCallback callback, IntPtr userData);

        /// <summary>
        /// Cancel use of the current callback function (if one exists).
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_cancel_callback(RtMidiInPtr device);

        /// <summary>
        /// Specify whether certain MIDI message types should be queued or ignored during input.
        /// </summary>
        /// <param name="device">Device.</param>
        /// <param name="midiSysex">Ignore sysex</param>
        /// <param name="midiTime">Ignore time</param>
        /// <param name="midiSense">Ignore sense</param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal void rtmidi_in_ignore_types(RtMidiInPtr device, bool midiSysex, bool midiTime, bool midiSense);

        /// <summary>
        /// Fill the user-provided array with the data bytes for the next available
        /// MIDI message in the input queue and return the event delta-time in seconds.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="message">Must point to a char* that is already allocated. SYSEX
        /// messages maximum size being 1024, a statically allocated array could be sufficient.</param>
        /// <param name="size">Is used to return the size of the message obtained. </param>
        [DllImport(RtMidiLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern internal double rtmidi_in_get_message(RtMidiInPtr device, /* unsigned char ** */out IntPtr message, /* size_t * */ ref UIntPtr size);

        #endregion

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
