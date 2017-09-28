using System;

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
    }
}