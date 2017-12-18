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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void RtMidiCallback(double timestamp, IntPtr message, UIntPtr messageSize, IntPtr userData);

    internal static class RtMidiC
    {
        private static bool Is64Bit => IntPtr.Size == 8;
        
        /// <summary>
        /// Utility related API methods
        /// </summary>
        internal static class Utility
        {
            /// <summary>
            /// Returns the size (with sizeof) of a RtMidiApi instance.
            /// </summary>
            internal static int SizeofRtMidiApi() => Is64Bit
                ? RtMidiC64.Utility.SizeofRtMidiApi()
                : RtMidiC32.Utility.SizeofRtMidiApi();
        }

        /// <summary>
        /// Determine the available compiled MIDI APIs.
        /// If the given `apis` parameter is null, returns the number of available APIs.
        /// Otherwise, fill the given apis array with the RtMidi::Api values.
        /// </summary>
        /// <param name="apis">An array or a null value.</param>
        internal static int GetCompiledApi(IntPtr /* RtMidiApi * */ apis) => Is64Bit
            ? RtMidiC64.GetCompiledApi(apis)
            : RtMidiC32.GetCompiledApi(apis);

        /// <summary>
        /// Report an error.
        /// </summary>
        internal static void Error(RtMidiErrorType type, string errorString)
        {
            if (Is64Bit)
            {
                RtMidiC64.Error(type, errorString);
            }
            else
            {
                RtMidiC32.Error(type, errorString);
            }
        }

        /// <summary>
        /// Open a MIDI port.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Must be greater than 0</param>
        /// <param name="portName">Name for the application port.x</param>
        internal static void OpenPort(RtMidiPtr device, uint portNumber, string portName)
        {
            if (Is64Bit)
            {
                RtMidiC64.OpenPort(device, portNumber, portName);
            }
            else
            {
                RtMidiC32.OpenPort(device, portNumber, portName);
            }
        }

        /// <summary>
        /// Creates a virtual MIDI port to which other software applications can 
        /// connect.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portName"> Name for the application port.</param>
        internal static void OpenVirtualPort(RtMidiPtr device, string portName)
        {
            if (Is64Bit)
            {
                RtMidiC64.OpenVirtualPort(device, portName);
            }
            else
            {
                RtMidiC32.OpenVirtualPort(device, portName);
            }
        }

        /// <summary>
        /// Close a MIDI connection.
        /// </summary>
        /// <param name="device">Device to close</param>
        internal static void ClosePort(RtMidiPtr device)
        {
            if (Is64Bit)
            {
                RtMidiC64.ClosePort(device);
            }
            else
            {
                RtMidiC32.ClosePort(device);
            }
        }

        /// <summary>
        /// Return the number of available MIDI ports.
        /// </summary>
        /// <param name="device">Device</param>
        internal static uint GetPortCount(RtMidiPtr device) => Is64Bit
            ? RtMidiC64.GetPortCount(device)
            : RtMidiC32.GetPortCount(device);

        /// <summary>
        /// Return a string identifier for the specified MIDI input port number.
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="portNumber">Port number</param>
        internal static string GetPortName(RtMidiPtr device, uint portNumber) => Is64Bit
            ? RtMidiC64.GetPortName(device, portNumber)
            : RtMidiC32.GetPortName(device, portNumber);

        /// <summary>
        /// Input related API methods
        /// </summary>
        internal static class Input
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            internal static RtMidiInPtr CreateDefault() => Is64Bit
                ? RtMidiC64.Input.CreateDefault()
                : RtMidiC32.Input.CreateDefault();

            /// <summary>
            /// Create a  RtMidiInPtr value, with given api, clientName and queueSizeLimit.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">
            /// An optional client name can be specified. This will be used to group the ports that 
            /// are created by the application.
            /// </param>
            /// <param name="queueSizeLimit">An optional size of the MIDI input queue can be specified.</param>
            internal static RtMidiInPtr Create(RtMidiApi api, string clientName, uint queueSizeLimit) => Is64Bit
                ? RtMidiC64.Input.Create(api, clientName, queueSizeLimit)
                : RtMidiC32.Input.Create(api, clientName, queueSizeLimit);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            internal static void Free(RtMidiInPtr device)
            {
                if (Is64Bit)
                {
                    RtMidiC64.Input.Free(device);
                }
                else
                {
                    RtMidiC32.Input.Free(device);
                }
            }

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiIn.
            /// </summary>
            /// <param name="device">Device</param>
            internal static RtMidiApi GetCurrentApi(RtMidiPtr device) => Is64Bit
                ? RtMidiC64.Input.GetCurrentApi(device)
                : RtMidiC32.Input.GetCurrentApi(device);

            /// <summary>
            /// Set a callback function to be invoked for incoming MIDI messages.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="callback">Callback</param>
            /// <param name="userData">User data</param>
            internal static void SetCallback(RtMidiInPtr device, RtMidiCallback callback, IntPtr userData)
            {
                if (Is64Bit)
                {
                    RtMidiC64.Input.SetCallback(device, callback, userData);
                }
                else
                {
                    RtMidiC32.Input.SetCallback(device, callback, userData);
                }
            }

            /// <summary>
            /// Cancel use of the current callback function (if one exists).
            /// </summary>
            /// <param name="device">Device</param>
            internal static void CancelCallback(RtMidiInPtr device)
            {
                if (Is64Bit)
                {
                    RtMidiC64.Input.CancelCallback(device);
                }
                else
                {
                    RtMidiC32.Input.CancelCallback(device);
                }
            }

            /// <summary>
            /// Specify whether certain MIDI message types should be queued or ignored during input.
            /// </summary>
            /// <param name="device">Device.</param>
            /// <param name="midiSysex">Ignore sysex</param>
            /// <param name="midiTime">Ignore time</param>
            /// <param name="midiSense">Ignore sense</param>
            internal static void IgnoreTypes(RtMidiInPtr device, bool midiSysex, bool midiTime, bool midiSense)
            {
                if (Is64Bit)
                {
                    RtMidiC64.Input.IgnoreTypes(device, midiSysex, midiTime, midiSense);
                }
                else
                {
                    RtMidiC32.Input.IgnoreTypes(device, midiSysex, midiTime, midiSense);
                }
            }

            /// <summary>
            /// Fill the user-provided array with the data bytes for the next available
            /// MIDI message in the input queue and return the event delta-time in seconds.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Must point to a char* that is already allocated. SYSEX
            /// messages maximum size being 1024, a statically allocated array could be sufficient.</param>
            /// <param name="size">Is used to return the size of the message obtained. </param>
            internal static double GetMessage(RtMidiInPtr device, /* unsigned char ** */out IntPtr message, /* size_t * */ ref UIntPtr size) => Is64Bit
                ? RtMidiC64.Input.GetMessage(device, out message, ref size)
                : RtMidiC32.Input.GetMessage(device, out message, ref size);
        }

        /// <summary>
        /// Output related API methods
        /// </summary>
        internal static class Output
        {
            /// <summary>
            /// Create a default RtMidiInPtr value, with no initialization.
            /// </summary>
            internal static RtMidiOutPtr CreateDefault() => Is64Bit
                ? RtMidiC64.Output.CreateDefault()
                : RtMidiC32.Output.CreateDefault();

            /// <summary>
            /// Create a RtMidiOutPtr value, with given and clientName.
            /// </summary>
            /// <param name="api">An optional API id can be specified.</param>
            /// <param name="clientName">An optional client name can be specified. This
            /// will be used to group the ports that are created by the application.</param>
            internal static RtMidiOutPtr Create(RtMidiApi api, string clientName) => Is64Bit
                ? RtMidiC64.Output.Create(api, clientName)
                : RtMidiC32.Output.Create(api, clientName);

            /// <summary>
            /// Deallocate the given pointer.
            /// </summary>
            /// <param name="device">Device</param>
            internal static void Free(RtMidiOutPtr device)
            {
                if (Is64Bit)
                {
                    RtMidiC64.Output.Free(device);
                }
                else
                {
                    RtMidiC32.Output.Free(device);
                }
            }

            /// <summary>
            /// Returns the MIDI API specifier for the given instance of RtMidiOut.
            /// </summary>
            /// <param name="device">Device</param>
            internal static RtMidiApi GetCurrentApi(RtMidiPtr device) => Is64Bit
                ? RtMidiC64.Output.GetCurrentApi(device)
                : RtMidiC32.Output.GetCurrentApi(device);

            /// <summary>
            /// Immediately send a single message out an open MIDI output port.
            /// </summary>
            /// <param name="device">Device</param>
            /// <param name="message">Message</param>
            /// <param name="length">Length</param>
            internal static int SendMessage(RtMidiOutPtr device, byte[] message, int length) => Is64Bit
                ? RtMidiC64.Output.SendMessage(device, message, length)
                : RtMidiC32.Output.SendMessage(device, message, length);
        }
    }
}