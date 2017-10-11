using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a note is released (ended). 
    /// </summary>
    public struct NoteOffMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<NoteOffMessage>();

        public NoteOffMessage(Channel channel, Key key, int velocity)
        {
            StructHelper.IsWithin7BitRange(nameof(velocity), velocity);

            Channel = channel;
            Key = key;
            Velocity = velocity;
        }

        public Channel Channel { get; private set; }
        public Key Key { get; private set; }
        public int Velocity { get; private set; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.NoteOffBitmask, Channel),
                StructHelper.DataByte(Key),
                StructHelper.DataByte(Velocity)
            };
        }

        internal static bool TryDecode(byte[] message, out NoteOffMessage msg) 
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Note Off message", message.Length);
                msg = default;
                return false;
            }

            msg = new NoteOffMessage
            {
                Channel = (Channel)(Midi.ChannelBitmask & message[0]),
                Key = (Key)(Midi.DataBitmask & message[1]),
                Velocity = Midi.DataBitmask & message[2]
            };
            return true;
        }
    }
}