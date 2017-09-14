using System;
using RtMidi.Core.Unmanaged.API;
using Serilog;
using System.Runtime.InteropServices;
namespace RtMidi.Core.Unmanaged.Devices
{
    internal class RtMidiInputDevice : RtMidiDevice, IRtMidiInputDevice
    {
        internal RtMidiInputDevice(uint portNumber) : base(portNumber)
        {
        }

        public event EventHandler<byte[]> Message;

        protected override IntPtr CreateDevice()
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                Log.Debug("Creating default input device");
                handle = RtMidiC.Input.CreateDefault();
                CheckForError(handle);

                Log.Debug("Setting input callback");
                RtMidiC.Input.SetCallback(handle, HandleRtMidiCallback, IntPtr.Zero);
                CheckForError(handle);

                return handle;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to create default input device");

                if (handle != IntPtr.Zero)
                {
                    Log.Information("Freeing input device handle");
                    try
                    {
                        RtMidiC.Input.Free(handle);
                        CheckForError(handle);
                    }
                    catch (Exception e2)
                    {
                        Log.Error(e2, "Unable to free input device");
                    }
                }

                return IntPtr.Zero;
            }
        }

        private void HandleRtMidiCallback(double timestamp, IntPtr messagePtr, UIntPtr messageSize, IntPtr userData)
        {
            try
            {
                var messageHandlers = Message;
                if (messageHandlers != null)
                {
                    // Copy message to managed byte array
                    var size = (int)messageSize;
                    var message = new byte[size];
                    Marshal.Copy(messagePtr, message, 0, size);

                    // Invoke message handlers
                    messageHandlers.Invoke(this, message);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected exception occurred while receiving MIDI message");
                return;
            }


        }

        protected override void DestroyDevice()
        {
            try
            {
                Log.Debug("Cancelling input callback");
                RtMidiC.Input.CancelCallback(Handle);
                CheckForError();

                Log.Debug("Freeing input device handle");
                RtMidiC.Input.Free(Handle);
                CheckForError();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while freeing input device handle");
            }
        }
    }
}
