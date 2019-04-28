using RtMidi.Core.Devices;
using Serilog;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message contains System Exclusive raw data sent from the device to the host.
    /// </summary>
    public readonly struct SysExMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SysExMessage>();

        public SysExMessage(byte[] data)
        {
            Timestamp = 0;
            Data = StructHelper.IsValidSysEx(data);
        }
        
        public SysExMessage(double timestamp, byte[] data)
        {
            Timestamp = timestamp;
            Data = StructHelper.IsValidSysEx(data);
        }

        /// <summary>
        /// SysEx Data Array
        /// </summary>
        public byte[] Data { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        internal byte[] Encode()
        {
            return StructHelper.FormatSysEx(Data);
        }

        internal static bool TryDecode(double timestamp, byte[] message, out SysExMessage msg)
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
                timestamp,
                message
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Data)}: {string.Join(", ", Data)}";
        }
    }
}