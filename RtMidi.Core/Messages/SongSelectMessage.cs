using RtMidi.Core.Devices;
using Serilog;

namespace RtMidi.Core.Messages
{
    public readonly struct SongSelectMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SongSelectMessage>();

        public SongSelectMessage(int song)
        {
            StructHelper.IsWithin7BitRange(nameof(song), song);

            Song = song;
        }
        
        /// <summary>
        /// Song og sequence to be played
        /// </summary>
        public int Song { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                Midi.Status.SongSelect,
                StructHelper.DataByte(Song)
            };
        }

        internal static bool TryDecode(byte[] message, out SongSelectMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Song Select message", message.Length);
                msg = default;
                return false;
            }

            msg = new(Midi.DataBitmask & message[1]);
            return true;
        }
    }
}