using System;
using RtMidi.Core.Enums;
using RtMidi.Core.Devices;

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

        public static void IsValidSysEx(byte[] data) {
            if (data[0] != Midi.Status.SysExStart)
                throw new ArgumentException($"SysEx message start byte must be {Midi.Status.SysExStart}");

            for (int i = 1; i < data.Length - 1; i++) {
                if (data[i] == Midi.Status.SysExStart || data[i] == Midi.Status.SysExEnd)
                    throw new ArgumentException($"SysEx message data byte must not be {Midi.Status.SysExStart} or {Midi.Status.SysExEnd}");
            }

            if (data[data.Length - 1] != Midi.Status.SysExEnd)
                throw new ArgumentException($"SysEx message end byte must be {Midi.Status.SysExEnd}");
        }

        public static byte StatusByte(byte statusBitmask, Channel channel)
            => (byte)(statusBitmask | (Midi.ChannelBitmask & (int)channel));

        public static byte DataByte(int value)
            => (byte)(Midi.DataBitmask & value);

        public static byte DataByte(Key key)
            => (byte)(Midi.DataBitmask & (int)key);
    }
}