﻿using RtMidi.Core.Enums;
using Serilog;
using RtMidi.Core.Devices;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a note is depressed (start). 
    /// </summary>
    public readonly struct NoteOnMessage: IMessage
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<NoteOnMessage>();

        public NoteOnMessage(Channel channel, Key key, int velocity)
        {
            StructHelper.IsWithin7BitRange(nameof(velocity), velocity);
            Timestamp = 0;
            Channel = channel;
            Key = key;
            Velocity = velocity;
        }
        
        public NoteOnMessage(double timestamp, Channel channel, Key key, int velocity)
        {
            StructHelper.IsWithin7BitRange(nameof(velocity), velocity);
            Timestamp = timestamp;
            Channel = channel;
            Key = key;
            Velocity = velocity;
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
        /// Velocity value (0-127)
        /// </summary>
        public int Velocity { get; }
        
        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        internal byte[] Encode()
        {
            return new[]
            {
                StructHelper.StatusByte(Midi.Status.NoteOnBitmask, Channel),
                StructHelper.DataByte(Key),
                StructHelper.DataByte(Velocity)
            };
        }

        internal static bool TryDecode(double timestamp, byte[] message, out NoteOnMessage msg)
        {
            if (message.Length != 3)
            {
                Log.Error("Incorrect number of bytes ({Length}) received for Note On message", message.Length);
                msg = default;
                return false;
            }

            msg = new NoteOnMessage
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
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Channel)}: {Channel}, {nameof(Key)}: {Key}, {nameof(Velocity)}: {Velocity}";
        }
    }
}