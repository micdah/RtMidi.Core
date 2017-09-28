namespace RtMidi.Core
{
    internal static class Midi
    {
        /// <summary>
        /// Bitmask to isolate channel part of status byte
        /// </summary>
        internal const byte ChannelBitmask = 0b0000_1111;

        /// <summary>
        /// Bitmask to isolate data part of data byte
        /// </summary>
        internal const byte DataBitmask = 0b0111_1111;

        /// <summary>
        /// Status masks
        /// </summary>
        internal static class Status
        {
            internal const byte NoteOffBitmask = 0b1000_0000;
            internal const byte NoteOnBitmask = 0b1001_0000;
            internal const byte PolyphonicKeyPressureBitmask = 0b1010_0000;
            internal const byte ControlChangeBitmask = 0b1011_0000;
            internal const byte ProgramChangeBitmask = 0b1100_0000;
            internal const byte ChannelPressureBitmask = 0b1101_0000;
            internal const byte PitchBendChange = 0b1110_0000;
        }
    }
}
