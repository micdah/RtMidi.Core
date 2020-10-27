using RtMidi.Core.Devices;
using Serilog;

namespace RtMidi.Core.Messages
{
    public readonly struct MidiTimeCodeQuarterFrameMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<MidiTimeCodeQuarterFrameMessage>();

        public MidiTimeCodeQuarterFrameMessage(int messageType, int values)
        {
            StructHelper.IsWithin3BitRange(nameof(messageType), messageType);
            StructHelper.IsWithin3BitRange(nameof(values), values);

            MessageType = messageType;
            Values = values;
        }

        /// <summary>
        /// Message type
        /// </summary>
        public int MessageType { get; }

        /// <summary>
        /// Values
        /// </summary>
        public int Values { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                Midi.Status.MidiTimeCodeQuarterFrame,
                StructHelper.DataByte((MessageType << 3) | Values)
            };
        }

        internal static bool TryDecode(byte[] message, out MidiTimeCodeQuarterFrameMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for MIDI Time Code Quarter Frame message",
                    message.Length);
                msg = default;
                return false;
            }

            var data = Midi.DataBitmask & message[1];
            var messageType = data >> 3;
            var values = data & 0b0000_0111;
            msg = new MidiTimeCodeQuarterFrameMessage(messageType, values);
            return true;
        }
    }
}