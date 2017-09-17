using Xunit;
using RtMidi.Core.Enums;

namespace RtMidi.Core.Tests
{
    public class NoteTests
    {
        [Fact]
        public void Should_Return_Octave_0_For_Note_0_Through_11() => TestOctave(0, 11, 0);

        [Fact]
        public void Should_Return_Octave_1_For_Note_12_Through_23() => TestOctave(12, 23, 1);

        [Fact]
        public void Should_Return_Octave_2_For_Note_24_Through_35() => TestOctave(24, 35, 2);

        [Fact]
        public void Should_Return_Octave_3_For_Note_36_Through_47() => TestOctave(36, 47, 3);

        [Fact]
        public void Should_Return_Octave_4_For_Note_48_Through_59() => TestOctave(48, 59, 4);

        [Fact]
        public void Should_Return_Octave_5_For_Note_60_Through_71() => TestOctave(60, 71, 5);

        [Fact]
        public void Should_Return_Octave_6_For_Note_72_Through_83() => TestOctave(72, 83, 6);

        [Fact]
        public void Should_Return_Octave_7_For_Note_84_Through_95() => TestOctave(84, 95, 7);

        [Fact]
        public void Should_Return_Octave_8_For_Note_96_Through_107() => TestOctave(96, 107, 8);

        [Fact]
        public void Should_Return_Octave_9_For_Note_108_Through_119() => TestOctave(108, 119, 9);

        [Fact]
        public void Should_Return_Octave_10_For_Note_120_Through_127() => TestOctave(120, 127, 10);

        private static void TestOctave(int noteFrom, int noteTo, int expectedOctate)
        {
            for (var i = noteFrom; i <= noteTo; i++)
            {
                var note = (Note)i;
                Assert.Equal(expectedOctate, note.Octave());
            }
        }
    }
}