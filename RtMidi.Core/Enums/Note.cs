using RtMidi.Core.Enums.Core;

namespace RtMidi.Core.Enums
{
    public enum Note
    {
        [EnumDisplayName("C")]  C = 0,
        [EnumDisplayName("C#")] CSharp = 1,
        [EnumDisplayName("D")]  D = 2,
        [EnumDisplayName("D#")] DSharp = 3,
        [EnumDisplayName("E")]  E = 4,
        [EnumDisplayName("F")]  F = 5,
        [EnumDisplayName("F#")] FSharp = 6,
        [EnumDisplayName("G")]  G = 7,
        [EnumDisplayName("G#")] GSharp = 8,
        [EnumDisplayName("A")]  A = 9,
        [EnumDisplayName("A#")] ASharp = 10,
        [EnumDisplayName("B")]  B = 11
    }

    public static class NoteExtensions
    {
        /// <summary>
        /// Get human readable display name of Key
        /// </summary>
        public static string DisplayName(this Note note) => EnumExtensions.GetDisplayNameAttribute(note)?.Name??string.Empty;
    }
}
