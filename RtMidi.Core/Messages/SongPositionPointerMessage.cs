using RtMidi.Core.Devices;
using Serilog;

namespace RtMidi.Core.Messages
{
    public readonly struct SongPositionPointerMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SongPositionPointerMessage>();

        public SongPositionPointerMessage(int midiBeats)
        {
            StructHelper.IsWithin14BitRange(nameof(midiBeats), midiBeats);

            MidiBeats = midiBeats;
        }

        /// <summary>
        /// MIDI Beats (1 beat = six MIDI clocks) since the start of the song
        /// </summary>
        public int MidiBeats { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                Midi.Status.SongPositionPointer,
                StructHelper.DataByte(MidiBeats),
                StructHelper.DataByte(MidiBeats >> 7)
            };
        }

        internal static bool TryDecode(byte[] message, out SongPositionPointerMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Song Position Pointer message",
                    message.Length);
                msg = default;
                return false;
            }

            msg = new(
                // Data byte 1 = LSB
                (Midi.DataBitmask & message[1]) |
                // Data byte 2 = MSB
                ((Midi.DataBitmask & message[2]) << 7)
            );
            return true;
        }
    }
}