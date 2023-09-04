using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is most often sent by pressing down on the key after it "bottoms out". 
    /// This message is different from polyphonic after-touch. Use this message to send the 
    /// single greatest pressure value (of all the current depressed keys)
    /// </summary>
    public readonly struct ChannelPressureMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ChannelPressureMessage>();

        public ChannelPressureMessage(Channel channel, int pressure)
        {
            StructHelper.IsWithin7BitRange(nameof(pressure), pressure);

            Channel = channel;
            Pressure = pressure;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; }

        /// <summary>
        /// Pressure value (0-127)
        /// </summary>
        public int Pressure { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.ChannelPressureBitmask, Channel),
                StructHelper.DataByte(Pressure)
            };
        }

        internal static bool TryDecode(byte[] message, out ChannelPressureMessage msg)
        {
            if (message.Length != 2)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Channel Pressure message", message.Length);
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
            return $"{nameof(Channel)}: {Channel}, {nameof(Pressure)}: {Pressure}";
        }
    }
}