using RtMidi.Core.Devices;
using RtMidi.Core.Enums;
using RtMidi.Core.Enums.Core;
using Serilog;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a controller value changes. Controllers 
    /// include devices such as pedals and levers. 
    /// </summary>
    public readonly struct ControlChangeMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ControlChangeMessage>();

        public ControlChangeMessage(Channel channel, int control, int value)
        {
            StructHelper.IsWithin7BitRange(nameof(control), control);
            StructHelper.IsWithin7BitRange(nameof(value), value);
            
            Channel = channel;
            Control = control;
            Timestamp = 0;
            ControlFunction = ControlFunction.Undefined.OrValueIfDefined(control);
            Value = value;
        }
        
        public ControlChangeMessage(double timestamp, Channel channel, int control, int value)
        {
            StructHelper.IsWithin7BitRange(nameof(control), control);
            StructHelper.IsWithin7BitRange(nameof(value), value);
            
            Channel = channel;
            Control = control;
            Timestamp = timestamp;
            ControlFunction = ControlFunction.Undefined.OrValueIfDefined(control);
            Value = value;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; }

        /// <summary>
        /// Control number (0-127)
        /// </summary>
        public int Control { get; }

        /// <summary>
        /// Control value (0-127)
        /// </summary>
        public int Value { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        /// <summary>
        /// Control function (as defined by https://www.midi.org/specifications/item/table-3-control-change-messages-data-bytes-2)
        /// </summary>
        public ControlFunction ControlFunction { get; }        

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.ControlChangeBitmask, Channel),
                StructHelper.DataByte(Control),
                StructHelper.DataByte(Value)
            };
        }

        internal static bool TryDecode(double timestamp, byte[] message, out ControlChangeMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of btyes ({Length}) received for Control Change message", message.Length);
                msg = default;
                return false;
            }

            var control = Midi.DataBitmask & message[1];
            msg = new ControlChangeMessage
            (
                timestamp,
                (Channel) (Midi.ChannelBitmask & message[0]),
                control,
                Midi.DataBitmask & message[2]
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Channel)}: {Channel}, {nameof(Control)}: {Control}, {nameof(Value)}: {Value}, {nameof(ControlFunction)}: {ControlFunction}";
        }
    }
}