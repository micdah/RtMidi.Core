using RtMidi.Core.Enums;
using Serilog;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is most often sent by pressing down on the key after it "bottoms out".
    /// </summary>
    public struct PolyphonicKeyPressureMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<PolyphonicKeyPressureMessage>();

        public PolyphonicKeyPressureMessage(Channel channel, Key key, int pressure)
        {
            StructHelper.IsWithin7BitRange(nameof(pressure), pressure);

            Channel = channel;
            Key = key;
            Pressure = pressure;
        }

        public Channel Channel { get; private set; }
        public Key Key { get; private set; }
        public int Pressure { get; private set; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.PolyphonicKeyPressureBitmask, Channel),
                StructHelper.DataByte(Key),
                StructHelper.DataByte(Pressure)
            };
        }

        internal static bool TryDecode(byte[] message, out PolyphonicKeyPressureMessage msg) 
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect nuber of bytes ({Length}) received for Polyphonic Key Pressure message", message.Length);
                msg = default;
                return false;
            }

            msg = new PolyphonicKeyPressureMessage
            {
                Channel = (Channel)(Midi.ChannelBitmask & message[0]),
                Key = (Key)(Midi.DataBitmask & message[1]),
                Pressure = Midi.DataBitmask & message[2]
            };
            return true;
        }
    }
}