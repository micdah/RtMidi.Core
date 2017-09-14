using System;
using System.Runtime.InteropServices;
using RtMidiPtr = System.IntPtr;
using RtMidiInPtr = System.IntPtr;
using RtMidiOutPtr = System.IntPtr;

namespace RtMidi.Core.Unmanaged.API
{
    /// <summary>
    /// The type of a RtMidi callback function.
    /// </summary>
    public delegate void RtMidiCallback(double timestamp, IntPtr message, UIntPtr messageSize, IntPtr userData);

    internal struct RtMidiWrapper 
    {
        IntPtr ptr;
        IntPtr data;
        bool ok;
        string msg;

        public bool Ok => ok;
        public string ErrorMessage => msg;
    }

    internal class RtMidiApiException : Exception
    {
        public RtMidiApiException(string message) : base(message)
        {
        }
    }

    internal static class RtMidiC
    {
        public const string RtMidiLibrary = "rtmidi";

        /// <summary>
        /// Utility related API methods
        /// </summary>
        internal static class Utility
        {
            /// <summary>
            /// Returns the size (with sizeof) of a RtMidiApi instance.
            /// </summary>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_sizeof_rtmidi_api", CallingConvention = CallingConvention.Cdecl)]
            static extern internal int SizeofRtMidiApi();
        }

        /// <summary>
        /// Determine the available compiled MIDI APIs.
        /// If the given `apis` parameter is null, returns the number of available APIs.
        /// Otherwise, fill the given apis array with the RtMidi::Api values.
        /// </summary>
        /// <param name="apis">An array or a null value.</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_get_compiled_api", CallingConvention = CallingConvention.Cdecl)]
        static extern internal int GetCompiledApi(ref IntPtr/* RtMidiApi ** */ apis);

        /// <summary>
        /// Report an error.
        /// </summary>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_error", CallingConvention = CallingConvention.Cdecl)]
        static extern internal void Error(RtMidiErrorType type, string errorString);

        /// <summary>
        /// Open a MIDI port.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Must be greater than 0</param>
        /// <param name="portName">Name for the application port.x</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_open_port", CallingConvention = CallingConvention.Cdecl)]
        static extern internal void OpenPort(RtMidiPtr device, uint portNumber, string portName);

        /// <summary>
        /// Creates a virtual MIDI port to which other software applications can 
        /// connect.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portName"> Name for the application port.</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_open_virtual_port", CallingConvention = CallingConvention.Cdecl)]
        static extern internal void OpenVirtualPort(RtMidiPtr device, string portName);

        /// <summary>
        /// Close a MIDI connection.
        /// </summary>
        /// <param name="device">Device to close</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_close_port", CallingConvention = CallingConvention.Cdecl)]
        static extern internal void ClosePort(RtMidiPtr device);

        /// <summary>
        /// Return the number of available MIDI ports.
        /// </summary>
        /// <param name="device">Device</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_get_port_count", CallingConvention = CallingConvention.Cdecl)]
        static extern internal uint GetPortCount(RtMidiPtr device);

        /// <summary>
        /// Return a string identifier for the specified MIDI input port number.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Port number</param>
        [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_get_port_name", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        static extern internal string GetPortName(RtMidiPtr device, uint portNumber);

        /// <summary>
        /// Input related API methods
        /// </summary>
        internal static class Input
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_create_default", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiInPtr CreateDefault();

            /// <summary>
            /// Create a  RtMidiInPtr value, with given api, clientName and queueSizeLimit.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">
            /// An optional client name can be specified. This will be used to group the ports that 
            /// are created by the application.
            /// </param>
            /// <param name="queueSizeLimit">An optional size of the MIDI input queue can be specified.</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_create", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiInPtr Create(RtMidiApi api, string clientName, uint queueSizeLimit);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_free", CallingConvention = CallingConvention.Cdecl)]
            static extern internal void Free(RtMidiInPtr device);

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiIn.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_get_current_api", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiApi GetCurrentApi(RtMidiPtr device);

            /// <summary>
            /// Set a callback function to be invoked for incoming MIDI messages.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="callback">Callback</param>
            /// <param name="userData">User data</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_set_callback", CallingConvention = CallingConvention.Cdecl)]
            static extern internal void SetCallback(RtMidiInPtr device, RtMidiCallback callback, IntPtr userData);

            /// <summary>
            /// Cancel use of the current callback function (if one exists).
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_cancel_callback", CallingConvention = CallingConvention.Cdecl)]
            static extern internal void CancelCallback(RtMidiInPtr device);

            /// <summary>
            /// Specify whether certain MIDI message types should be queued or ignored during input.
            /// </summary>
            /// <param name="device">Device.</param>
            /// <param name="midiSysex">Ignore sysex</param>
            /// <param name="midiTime">Ignore time</param>
            /// <param name="midiSense">Ignore sense</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_ignore_types", CallingConvention = CallingConvention.Cdecl)]
            static extern internal void IgnoreTypes(RtMidiInPtr device, bool midiSysex, bool midiTime, bool midiSense);

            /// <summary>
            /// Fill the user-provided array with the data bytes for the next available
            /// MIDI message in the input queue and return the event delta-time in seconds.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Must point to a char* that is already allocated. SYSEX
            /// messages maximum size being 1024, a statically allocated array could be sufficient.</param>
            /// <param name="size">Is used to return the size of the message obtained. </param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_in_get_message", CallingConvention = CallingConvention.Cdecl)]
            static extern internal double GetMessage(RtMidiInPtr device, /* unsigned char ** */out IntPtr message, /* size_t * */ ref UIntPtr size);
        }

        /// <summary>
        /// Output related API methods
        /// </summary>
        internal static class Output
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_out_create_default", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiOutPtr CreateDefault();

            /// <summary>
            /// Create a RtMidiOutPtr value, with given and clientName.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">An optional client name can be specified. This
            /// will be used to group the ports that are created by the application.</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_out_create", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiOutPtr Create(RtMidiApi api, string clientName);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_out_free", CallingConvention = CallingConvention.Cdecl)]
            static extern internal void Free(RtMidiOutPtr device);

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiOut.
            /// </summary>
            /// <param name="device">Device</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_out_get_current_api", CallingConvention = CallingConvention.Cdecl)]
            static extern internal RtMidiApi GetCurrentApi(RtMidiPtr device);

            /// <summary>
            /// Immediately send a single message out an open MIDI output port.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Message</param>
            /// <param name="length">Length</param>
            [DllImport(RtMidiLibrary, EntryPoint = "rtmidi_out_send_message", CallingConvention = CallingConvention.Cdecl)]
            static extern internal int SendMessage(RtMidiOutPtr device, byte[] message, int length);
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