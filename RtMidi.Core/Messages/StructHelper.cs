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

        public static byte[] IsValidSysEx(byte[] data) {
            if (data[0] == Midi.Status.SysExStart && data[data.Length - 1] == Midi.Status.SysExEnd)
                data = StripSysEx(data);
  
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Midi.Status.SysExStart || data[i] == Midi.Status.SysExEnd)
                    throw new ArgumentException(
                        $"SysEx message data byte must not be {Midi.Status.SysExStart} or {Midi.Status.SysExEnd}");
            }

            return data;
        }

        public static byte StatusByte(byte statusBitmask, Channel channel)
            => (byte) (statusBitmask | (Midi.ChannelBitmask & (int) channel));

        public static byte DataByte(int value)
            => (byte) (Midi.DataBitmask & value);

        public static byte DataByte(Key key)
            => (byte) (Midi.DataBitmask & (int) key);

        public static byte[] StripSysEx(byte[] data)
        {
            var stripped = new byte[data.Length - 2];
            Array.Copy(data, 1, stripped, 0, data.Length - 2);
            return stripped;
        }

        public static byte[] FormatSysEx(byte[] data)
        {
            var appended = new byte[data.Length + 2];
            
            appended[0] = Midi.Status.SysExStart;
            Array.Copy(data, 0, appended, 1, data.Length);
            appended[appended.Length - 1] = Midi.Status.SysExEnd;

            return appended;
        }
    }
}