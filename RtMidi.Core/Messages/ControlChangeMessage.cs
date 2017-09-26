using RtMidi.Core.Enums;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a controller value changes. Controllers include devices such as pedals and levers. 
    /// </summary>
    public struct ControlChangeMessage
    {
        public ControlChangeMessage(Channel channel, int control, int value)
        {
            Channel = channel;
            Control = control;
            Value = value;
        }

        // TODO Create detail enum of midi controls, see https://www.midi.org/specifications/item/table-3-control-change-messages-data-bytes-2

        public Channel Channel { get; }
        public int Control { get; }
        public int Value { get; }
    }
}