using RtMidi.Core.Enums;
using Serilog;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message sent when the patch number changes.
    /// </summary>
    public struct ProgramChangeMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ProgramChangeMessage>();

        // TODO Create detail enum of General MIDI instruments, see https://en.wikipedia.org/wiki/General_MIDI

        public ProgramChangeMessage(Channel channel, int program)
        {
            StructHelper.IsWithin7BitRange(nameof(program), program);

            Channel = channel;
            Program = program;
        }

        public Channel Channel { get; private set; }
        public int Program { get; private set; }

        internal static bool TryDecode(byte[] message, out ProgramChangeMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Program Change message", message.Length);
                msg = default;
                return false;
            }

            msg = new ProgramChangeMessage
            {
                Channel = (Channel) (Midi.ChannelBitmask & message[0]),
                Program = Midi.DataBitmask & message[1]
            };
            return true;
        }
    }
}