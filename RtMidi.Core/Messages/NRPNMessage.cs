using RtMidi.Core.Enums;
using Serilog;
using System;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Non-Registered Parameter Number message, sent using up to four Control
    /// Change messages and allows 14-bit values to be used, rather than the
    /// normal 7-bit values in regular midi messages.
    /// </summary>
    public struct NrpnMessage
    {
        public NrpnMessage(Channel channel, int parameter, int value)
        {
            StructHelper.IsWithin14BitRange(nameof(parameter), parameter);
            StructHelper.IsWithin14BitRange(nameof(value), value);

            Channel = channel;
            Parameter = parameter;
            Value = value;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; private set; }

        /// <summary>
        /// 14-bit Parameter number
        /// </summary>
        public int Parameter { get; private set; }

        /// <summary>
        /// 14-bit Parameter value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Determine if the array of Control Change messges represent a full or partial 
        /// NRPN message, meaning they are of the correct <see cref="ControlFunction"/>
        /// in the correct order, and all belong to the same channel
        /// </summary>
        /// <param name="messages">Control Change messages</param>
        /// <returns>True if full or partial NRPN message, false otherwise</returns>
        internal static bool IsExpectedControls(params ControlChangeMessage[] messages)
        {
            if (messages == null) return false;
            if (messages.Length == 0) return false;

            if (messages.Length > 0 && messages[0].ControlFunction != ControlFunction.NonRegisteredParameterNumberMSB)
                return false;

            var expectedChannel = messages[0].Channel;

            if (messages.Length > 1 && (
                    messages[1].ControlFunction != ControlFunction.NonRegisteredParameterNumberLSB ||
                    messages[1].Channel != expectedChannel))
                return false;

            if (messages.Length > 2 && (
                    messages[2].ControlFunction != ControlFunction.DataEntryMSB ||
                    messages[2].Channel != expectedChannel))
                return false;

            if (messages.Length > 3 && (
                    messages[3].ControlFunction != ControlFunction.LSBForControl6DataEntry ||
                    messages[3].Channel != expectedChannel))
                return false;

            return messages.Length <= 4;
        }

        internal ControlChangeMessage[] Encode()
        {
            return new[]
            {
                new ControlChangeMessage(Channel, (int)ControlFunction.NonRegisteredParameterNumberMSB, Parameter >> 7),
                new ControlChangeMessage(Channel, (int)ControlFunction.NonRegisteredParameterNumberLSB, Parameter & Midi.DataBitmask),  
                new ControlChangeMessage(Channel, (int)ControlFunction.DataEntryMSB, Value >> 7), 
                new ControlChangeMessage(Channel, (int)ControlFunction.LSBForControl6DataEntry, Value & Midi.DataBitmask)
            };
        }

        internal static bool TryDecode(ControlChangeMessage[] messages, out NrpnMessage msg)
        {
            if (messages.Length != 4)
            {
                Log.Error("Incorrect number of Control Change messages ({Length}) received for NRPN Message", messages.Length);
                msg = default;
                return false;
            }

            if (!IsExpectedControls(messages))
            {
                Log.Error("Incorrect sequence of Control Change messages received for NRPN Message");
                msg = default;
                return false;
            }

            msg = new NrpnMessage
            {
                Channel = messages[0].Channel,
                Parameter = (messages[0].Value & Midi.DataBitmask) << 7 | (messages[1].Value & Midi.DataBitmask),
                Value = (messages[2].Value & Midi.DataBitmask) << 7 | (messages[3].Value & Midi.DataBitmask)
            };
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Channel)}: {Channel}, {nameof(Parameter)}: {Parameter}, {nameof(Value)}: {Value}";
        }
    }
}
