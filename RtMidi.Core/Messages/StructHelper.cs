using System;
using RtMidi.Core.Enums;

namespace RtMidi.Core.Messages
{
    internal static class StructHelper
    {
        public static void IsWithin7BitRange(string parameter, int value)
        {
            if (value < 0 || value > 127)
                throw new ArgumentOutOfRangeException(parameter, "Must be within 0-127");
        }

        public static void IsWithin14BitRange(string parameter, int value)
        {
            if (value < 0 || value > 16383)
                throw new ArgumentOutOfRangeException(parameter, "Must be within 0-16383");
        }

        public static byte StatusByte(byte statusBitmask, Channel channel)
            => (byte)(statusBitmask | (Midi.ChannelBitmask & (int)channel));

        public static byte DataByte(int value)
            => (byte)(Midi.DataBitmask & value);

        public static byte DataByte(Key key)
            => (byte)(Midi.DataBitmask & (int)key);
    }
}