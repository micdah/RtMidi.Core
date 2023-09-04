using RtMidi.Core.Devices;
using Serilog;

namespace RtMidi.Core.Messages
{
    public readonly struct TuneRequestMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<TuneRequestMessage>();

        internal byte[] Encode()
        {
            return new[]
            {
                Midi.Status.TuneRequest
            };
        }

        internal static bool TryDecode(byte[] message, out TuneRequestMessage msg)
        {
            if (message.Length != 1)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Tune Request message", message.Length);
                msg = default;
                return false;
            }

            msg = new();
            return true;
        }
    }
}