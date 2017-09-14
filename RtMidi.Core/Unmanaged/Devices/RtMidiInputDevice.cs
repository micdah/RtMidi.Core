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
            try
            {
                Log.Debug("Creating default input device");
                var handle = RtMidiC.Input.CreateDefault();

                Log.Debug("Setting input callback");
                RtMidiC.Input.SetCallback(handle, HandleRtMidiCallback, IntPtr.Zero);

                return handle;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to create default input device");
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

                Log.Debug("Freeing input device handle");
                RtMidiC.Input.Free(Handle);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while freeing input device handle");
            }
        }
    }
}
