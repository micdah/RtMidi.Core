using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent to indicate a change in the pitch bender (wheel or lever, typically).
    /// The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H.
    /// Sensitivity is a function of the receiver, but may be set using RPN 0.
    /// </summary>
    public readonly struct PitchBendMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<PitchBendMessage>();

        public PitchBendMessage(Channel channel, int value)
        {
            StructHelper.IsWithin14BitRange(nameof(value), value);

            Channel = channel;
            Value = value;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; }

        /// <summary>
        /// Pitch value (0-16383)
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.PitchBendChange, Channel),
                StructHelper.DataByte(Value),
                StructHelper.DataByte(Value >> 7)
            };
        }

        internal static bool TryDecode(byte[] message, out PitchBendMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Pitch Bend message", message.Length);
                msg = default;
                return false;
            }

            msg = new(
                (Channel) (Midi.ChannelBitmask & message[0]),
                // Data byte 1 = LSB
                (Midi.DataBitmask & message[1]) |
                // Data byte 2 = MSB
                ((Midi.DataBitmask & message[2]) << 7)
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Channel)}: {Channel}, {nameof(Value)}: {Value}";
        }
    }
}