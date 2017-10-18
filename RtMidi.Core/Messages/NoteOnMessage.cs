using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a note is depressed (start). 
    /// </summary>
    public struct NoteOnMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<NoteOnMessage>();

        public NoteOnMessage(Channel channel, Key key, int velocity)
        {
            StructHelper.IsWithin7BitRange(nameof(velocity), velocity);

            Channel = channel;
            Key = key;
            Velocity = velocity;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; private set; }

        /// <summary>
        /// Key number (0-127)
        /// </summary>
        public Key Key { get; private set; }

        /// <summary>
        /// Velocity value (0-127)
        /// </summary>
        public int Velocity { get; private set; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.NoteOnBitmask, Channel),
                StructHelper.DataByte(Key),
                StructHelper.DataByte(Velocity)
            };
        }

        internal static bool TryDecoce(byte[] message, out NoteOnMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Note On message", message.Length);
                msg = default;
                return false;
            }

            msg = new NoteOnMessage
            {
                Channel = (Channel)(Midi.ChannelBitmask & message[0]),
                Key = (Key)(Midi.DataBitmask & message[1]),
                Velocity = Midi.DataBitmask & message[2]
            };
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Channel)}: {Channel}, {nameof(Key)}: {Key}, {nameof(Velocity)}: {Velocity}";
        }
    }
}