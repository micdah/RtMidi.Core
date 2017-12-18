using System;
using System.Runtime.InteropServices;

namespace RtMidi.Core.Unmanaged.API
{
    internal static class RtMidiC32
    {
        private const string LibraryFile = "rtmidi32";

        /// <summary>
        /// Utility related API methods
        /// </summary>
        internal static class Utility
        {
            /// <summary>
            /// Returns the size (with sizeof) of a RtMidiApi instance.
            /// </summary>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_sizeof_rtmidi_api", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SizeofRtMidiApi();
        }

        /// <summary>
        /// Determine the available compiled MIDI APIs.
        /// If the given `apis` parameter is null, returns the number of available APIs.
        /// Otherwise, fill the given apis array with the RtMidi::Api values.
        /// </summary>
        /// <param name="apis">An array or a null value.</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_get_compiled_api", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetCompiledApi(IntPtr/* RtMidiApi * */ apis);

        /// <summary>
        /// Report an error.
        /// </summary>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_error", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Error(RtMidiErrorType type, string errorString);

        /// <summary>
        /// Open a MIDI port.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Must be greater than 0</param>
        /// <param name="portName">Name for the application port.x</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_open_port", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OpenPort(IntPtr device, uint portNumber, string portName);

        /// <summary>
        /// Creates a virtual MIDI port to which other software applications can 
        /// connect.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portName"> Name for the application port.</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_open_virtual_port", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OpenVirtualPort(IntPtr device, string portName);

        /// <summary>
        /// Close a MIDI connection.
        /// </summary>
        /// <param name="device">Device to close</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_close_port", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ClosePort(IntPtr device);

        /// <summary>
        /// Return the number of available MIDI ports.
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_get_port_count", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetPortCount(IntPtr device);

        /// <summary>
        /// Return a string identifier for the specified MIDI input port number.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Port number</param>
        [DllImport(LibraryFile, EntryPoint = "rtmidi_get_port_name", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string GetPortName(IntPtr device, uint portNumber);

        /// <summary>
        /// Input related API methods
        /// </summary>
        internal static class Input
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_create_default", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr CreateDefault();

            /// <summary>
            /// Create a  RtMidiInPtr value, with given api, clientName and queueSizeLimit.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">
            /// An optional client name can be specified. This will be used to group the ports that 
            /// are created by the application.
            /// </param>
            /// <param name="queueSizeLimit">An optional size of the MIDI input queue can be specified.</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_create", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr Create(RtMidiApi api, string clientName, uint queueSizeLimit);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_free", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Free(IntPtr device);

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiIn.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_get_current_api", CallingConvention = CallingConvention.Cdecl)]
            internal static extern RtMidiApi GetCurrentApi(IntPtr device);

            /// <summary>
            /// Set a callback function to be invoked for incoming MIDI messages.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="callback">Callback</param>
            /// <param name="userData">User data</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_set_callback", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetCallback(IntPtr device, RtMidiCallback callback, IntPtr userData);

            /// <summary>
            /// Cancel use of the current callback function (if one exists).
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_cancel_callback", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void CancelCallback(IntPtr device);

            /// <summary>
            /// Specify whether certain MIDI message types should be queued or ignored during input.
            /// </summary>
            /// <param name="device">Device.</param>
            /// <param name="midiSysex">Ignore sysex</param>
            /// <param name="midiTime">Ignore time</param>
            /// <param name="midiSense">Ignore sense</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_ignore_types", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void IgnoreTypes(IntPtr device, bool midiSysex, bool midiTime, bool midiSense);

            /// <summary>
            /// Fill the user-provided array with the data bytes for the next available
            /// MIDI message in the input queue and return the event delta-time in seconds.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Must point to a char* that is already allocated. SYSEX
            /// messages maximum size being 1024, a statically allocated array could be sufficient.</param>
            /// <param name="size">Is used to return the size of the message obtained. </param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_in_get_message", CallingConvention = CallingConvention.Cdecl)]
            internal static extern double GetMessage(IntPtr device, /* unsigned char ** */out IntPtr message, /* size_t * */ ref UIntPtr size);
        }

        /// <summary>
        /// Output related API methods
        /// </summary>
        internal static class Output
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_out_create_default", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr CreateDefault();

            /// <summary>
            /// Create a RtMidiOutPtr value, with given and clientName.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">An optional client name can be specified. This
            /// will be used to group the ports that are created by the application.</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_out_create", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr Create(RtMidiApi api, string clientName);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_out_free", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Free(IntPtr device);

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiOut.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_out_get_current_api", CallingConvention = CallingConvention.Cdecl)]
            internal static extern RtMidiApi GetCurrentApi(IntPtr device);

            /// <summary>
            /// Immediately send a single message out an open MIDI output port.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Message</param>
            /// <param name="length">Length</param>
            [DllImport(LibraryFile, EntryPoint = "rtmidi_out_send_message", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SendMessage(IntPtr device, byte[] message, int length);
        }
    }
}