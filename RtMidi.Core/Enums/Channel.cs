using System;
using RtMidi.Core.Enums.Core;

namespace RtMidi.Core.Enums
{
    /// <summary>
    /// MIDI Channel
    /// </summary>
    public enum Channel
    {
        [EnumDisplayName("Channel 1")]  Channel1 = 0,
        [EnumDisplayName("Channel 2")]  Channel2 = 1,
        [EnumDisplayName("Channel 3")]  Channel3 = 2,
        [EnumDisplayName("Channel 4")]  Channel4 = 3,
        [EnumDisplayName("Channel 5")]  Channel5 = 4,
        [EnumDisplayName("Channel 6")]  Channel6 = 5,
        [EnumDisplayName("Channel 7")]  Channel7 = 6,
        [EnumDisplayName("Channel 8")]  Channel8 = 7,
        [EnumDisplayName("Channel 9")]  Channel9 = 8,
        [EnumDisplayName("Channel 10")] Channel10 = 9,
        [EnumDisplayName("Channel 11")] Channel11 = 10,
        [EnumDisplayName("Channel 12")] Channel12 = 11,
        [EnumDisplayName("Channel 13")] Channel13 = 12,
        [EnumDisplayName("Channel 14")] Channel14 = 13,
        [EnumDisplayName("Channel 15")] Channel15 = 14,
        [EnumDisplayName("Channel 16")] Channel16 = 15,
    }

    public static class ChannelExtensions
    {
        /// <summary>
        /// Get human readable display name of Channel
        /// </summary>
        public static string DisplayName(this Channel channel) => EnumExtensions.GetDisplayNameAttribute(channel)?.Name??string.Empty;
    }
}
