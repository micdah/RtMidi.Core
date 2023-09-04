using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message sent when the patch number changes.
    /// </summary>
    public readonly struct ProgramChangeMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ProgramChangeMessage>();

        // TODO Create detail enum of General MIDI instruments, see https://en.wikipedia.org/wiki/General_MIDI

        public ProgramChangeMessage(Channel channel, int program)
        {
            StructHelper.IsWithin7BitRange(nameof(program), program);

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

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.ProgramChangeBitmask, Channel),
                StructHelper.DataByte(Program)
            };
        }

        internal static bool TryDecode(byte[] message, out ProgramChangeMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Program Change message", message.Length);
                msg = default;
                return false;
            }

            msg = new(
                (Channel) (Midi.ChannelBitmask & message[0]),
                Midi.DataBitmask & message[1]
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Channel)}: {Channel}, {nameof(Program)}: {Program}";
        }
    }
}