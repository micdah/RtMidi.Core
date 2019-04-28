using Serilog;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Some master device that controls sequence playback sends this message to force a slave device to cue the
    /// playback to a certain point in the song/sequence. In other words, this message sets the device's
    /// "Song Position". This message doesn't actually start the playback. It just sets up the device to be
    /// "ready to play" at a particular point in the song.
    /// </summary>
    public readonly struct SongPositionPointerMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SongPositionPointerMessage>();

        public SongPositionPointerMessage(int value)
        {
            StructHelper.IsWithin14BitRange(nameof(value), value);
            Timestamp = 0;
            Value = value;
        }
        
        public SongPositionPointerMessage(double timestamp, int value)
        {
            StructHelper.IsWithin14BitRange(nameof(value), value);
            Timestamp = timestamp;
            Value = value;
        }

        /// <summary>
        /// MIDI Beat value (0-16383)
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                Midi.Status.SongPositionPointer,
                StructHelper.DataByte(Value & 0b0111_1111),
                StructHelper.DataByte(Value >> 7)
            };
        }

        internal static bool TryDecode(double timestamp, byte[] message, out SongPositionPointerMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Song Position Pointer message", message.Length);
                msg = default;
                return false;
            }

            msg = new SongPositionPointerMessage
            (
                timestamp,
                // Data byte 1 = LSB
                (Midi.DataBitmask & message[1]) |
                // Data byte 2 = MSB
                ((Midi.DataBitmask & message[2]) << 7)
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Value)}: {Value}";
        }
    }
}