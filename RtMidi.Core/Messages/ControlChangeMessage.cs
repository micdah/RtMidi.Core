using RtMidi.Core.Enums;
using Serilog;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a controller value changes. Controllers include devices such as pedals and levers. 
    /// </summary>
    public struct ControlChangeMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ControlChangeMessage>();

        public ControlChangeMessage(Channel channel, int control, int value)
        {
            StructHelper.IsWithin7BitRange(nameof(control), control);
            StructHelper.IsWithin7BitRange(nameof(value), value);
            
            Channel = channel;
            Control = control;
            Value = value;
        }

        // TODO Create detail enum of midi controls, see https://www.midi.org/specifications/item/table-3-control-change-messages-data-bytes-2

        public Channel Channel { get; private set; }
        public int Control { get; private set; }
        public int Value { get; private set; }

        internal static bool TryDecode(byte[] message, out ControlChangeMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of btyes ({Length}) received for Control Change message", message.Length);
                msg = default;
                return false;
            }

            msg = new ControlChangeMessage
            {
                Channel = (Channel)(Midi.ChannelBitmask & message[0]),
                Control = Midi.DataBitmask & message[1],
                Value = Midi.DataBitmask & message[2]
            };
            return true;
        }
    }
}