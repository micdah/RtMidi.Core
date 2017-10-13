using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using System;
using Serilog;
namespace RtMidi.Core
{
    /// <summary>
    /// Non-Registered Parameter Number message, sent using up to four Control
    /// Change messages and allows 14-bit values to be used, rather than the
    /// normal 7-bit values in regular midi messages.
    /// </summary>
    public class NRPNMessage
    {
        public NRPNMessage(Channel channel, int parameter, int value)
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

        internal byte[][] Encode()
        {
            throw new NotImplementedException();
        }

        internal static bool TryDecode(byte[][] messages, out NRPNMessage msg)
        {
            throw new NotImplementedException();

            if (messages.Length < 3 || messages.Length > 4)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for NRPN Message", messages.Length);
                msg = default;
                return false;
            }

            
        }
    }
}
