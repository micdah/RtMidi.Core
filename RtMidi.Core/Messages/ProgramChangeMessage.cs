using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message sent when the patch number changes.
    /// </summary>
    public readonly struct ProgramChangeMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ProgramChangeMessage>();

        // TODO Create detail enum of General MIDI instruments, see https://en.wikipedia.org/wiki/General_MIDI

        public ProgramChangeMessage(Channel channel, int program)
        {
            StructHelper.IsWithin7BitRange(nameof(program), program);
            Timestamp = 0;
            Channel = channel;
            Program = program;
        }
        
        public ProgramChangeMessage(double timestamp, Channel channel, int program)
        {
            StructHelper.IsWithin7BitRange(nameof(program), program);
            Timestamp = timestamp;
            Channel = channel;
            Program = program;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; }

        /// <summary>
        /// Program number (0-127)
        /// </summary>
        public int Program { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.ProgramChangeBitmask, Channel),
                StructHelper.DataByte(Program)
            };
        }

        internal static bool TryDecode(double timestamp, byte[] message, out ProgramChangeMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Program Change message", message.Length);
                msg = default;
                return false;
            }

            msg = new ProgramChangeMessage
            (
                timestamp,
                (Channel) (Midi.ChannelBitmask & message[0]),
                Midi.DataBitmask & message[1]
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Channel)}: {Channel}, {nameof(Program)}: {Program}";
        }
    }
}