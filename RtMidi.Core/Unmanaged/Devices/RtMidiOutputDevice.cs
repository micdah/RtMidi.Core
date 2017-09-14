using System;
using RtMidi.Core.Unmanaged.API;
using Serilog;
namespace RtMidi.Core.Unmanaged.Devices
{
    internal class RtMidiOutputDevice : RtMidiDevice, IRtMidiOutputDevice
    {
        internal RtMidiOutputDevice(uint portNumber) : base(portNumber)
        {
        }

        public bool SendMessage(byte[] message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            // Cannot send, if device is not open
            if (!IsOpen) return false;

            try
            {
                return RtMidiC.Output.SendMessage(Handle, message, message.Length) == 0;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while sending message");
                return false;
            }
        }

        protected override IntPtr CreateDevice()
        {
            try
            {
                return RtMidiC.Output.CreateDefault();
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to create default output device");
                return IntPtr.Zero;
            }
        }

        protected override void DestroyDevice()
        {
            try
            {
                Log.Debug("Freeing output device handle");
                RtMidiC.Output.Free(Handle);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while freeing output device handle");
            }
        }
    }
}
