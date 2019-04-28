﻿using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is most often sent by pressing down on the key after it "bottoms out".
    /// </summary>
    public readonly struct PolyphonicKeyPressureMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<PolyphonicKeyPressureMessage>();

        public PolyphonicKeyPressureMessage(Channel channel, Key key, int pressure)
        {
            StructHelper.IsWithin7BitRange(nameof(pressure), pressure);
            Timestamp = 0;
            Channel = channel;
            Key = key;
            Pressure = pressure;
        }
        
        public PolyphonicKeyPressureMessage(double timestamp, Channel channel, Key key, int pressure)
        {
            StructHelper.IsWithin7BitRange(nameof(pressure), pressure);
            Timestamp = timestamp;
            Channel = channel;
            Key = key;
            Pressure = pressure;
        }

        /// <summary>
        /// MIDI Channel
        /// </summary>
        public Channel Channel { get; }

        /// <summary>
        /// Key number (0-127)
        /// </summary>
        public Key Key { get; }

        /// <summary>
        /// Pressure value (0-127)
        /// </summary>
        public int Pressure { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.PolyphonicKeyPressureBitmask, Channel),
                StructHelper.DataByte(Key),
                StructHelper.DataByte(Pressure)
            };
        }

        internal static bool TryDecode(double timestamp, byte[] message, out PolyphonicKeyPressureMessage msg) 
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect nuber of bytes ({Length}) received for Polyphonic Key Pressure message", message.Length);
                msg = default;
                return false;
            }

            msg = new PolyphonicKeyPressureMessage
            (
                timestamp,
                (Channel) (Midi.ChannelBitmask & message[0]),
                (Key) (Midi.DataBitmask & message[1]),
                Midi.DataBitmask & message[2]
            );
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Channel)}: {Channel}, {nameof(Key)}: {Key}, {nameof(Pressure)}: {Pressure}";
        }
    }
}