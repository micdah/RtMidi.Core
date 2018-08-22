using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message contains System Exclusive raw data sent from the device to the host.
    /// </summary>
    public readonly struct SysExMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SysExMessage>();

        public SysExMessage(byte[] data)
        {
            Data = StructHelper.IsValidSysEx(data);
        }

        /// <summary>
        /// SysEx Data Array
        /// </summary>
        public byte[] Data { get; }

        internal byte[] Encode()
        {
            return StructHelper.FormatSysEx(Data);
        }

        internal static bool TryDecode(byte[] message, out SysExMessage msg)
        {
            if (message.Length <= 1)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for SysEx message", message.Length);
                msg = default;
                return false;
            }

            if (message[0] != Midi.Status.SysExStart || message[message.Length - 1] != Midi.Status.SysExEnd)
            {
                Log.Error("Not a valid SysEx message received for SysEx message", message.Length);
                msg = default;
                return false;
            }

            msg = new SysExMessage
            (
                StructHelper.StripSysEx(message)
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Data)}: {string.Join(", ", Data)}";
        }
    }
}