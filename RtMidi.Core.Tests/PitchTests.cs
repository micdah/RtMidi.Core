using Xunit;
using RtMidi.Core.Enums;
using System;

namespace RtMidi.Core.Tests
{
    public class PitchTests
    {
        public class OctaveTests
        {
            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Pitch_Below_0()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var pitch = (Pitch)(-1);
                    pitch.Octave();
                });
            }

            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Pitch_Above_127()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var pitch = (Pitch)(128);
                    pitch.Octave();
                });
            }

            [Fact] public void Should_Return_0_For_Pitch_0_Through_11() => TestOctave(0, 11, 0);
            [Fact] public void Should_Return_1_For_Pitch_12_Through_23() => TestOctave(12, 23, 1);
            [Fact] public void Should_Return_2_For_Pitch_24_Through_35() => TestOctave(24, 35, 2);
            [Fact] public void Should_Return_3_For_Pitch_36_Through_47() => TestOctave(36, 47, 3);
            [Fact] public void Should_Return_4_For_Pitch_48_Through_59() => TestOctave(48, 59, 4);
            [Fact] public void Should_Return_5_For_Pitch_60_Through_71() => TestOctave(60, 71, 5);
            [Fact] public void Should_Return_6_For_Pitch_72_Through_83() => TestOctave(72, 83, 6);
            [Fact] public void Should_Return_7_For_Pitch_84_Through_95() => TestOctave(84, 95, 7);
            [Fact] public void Should_Return_8_For_Pitch_96_Through_107() => TestOctave(96, 107, 8);
            [Fact] public void Should_Return_9_For_Pitch_108_Through_119() => TestOctave(108, 119, 9);
            [Fact] public void Should_Return_10_For_Pitch_120_Through_127() => TestOctave(120, 127, 10);

            private static void TestOctave(int pitchFrom, int pitchTo, int expectedOctate)
            {
                for (var i = pitchFrom; i <= pitchTo; i++)
                {
                    var pitch = (Pitch)i;
                    Assert.Equal(expectedOctate, pitch.Octave());
                }
            }
        }

        public class NoteTests
        {
            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Pitch_Below_0() 
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var pitch = (Pitch)(-1);
                    pitch.Note();
                });
            }

            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Pitch_Above_127()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var pitch = (Pitch)(128);
                    pitch.Note();
                });
            }

            [Fact] public void Should_Return_Notes_In_Octave_0() => TestNotes(0);
            [Fact] public void Should_Return_Notes_In_Octave_1() => TestNotes(1);
            [Fact] public void Should_Return_Notes_In_Octave_2() => TestNotes(2);
            [Fact] public void Should_Return_Notes_In_Octave_3() => TestNotes(3);
            [Fact] public void Should_Return_Notes_In_Octave_4() => TestNotes(4);
            [Fact] public void Should_Return_Notes_In_Octave_5() => TestNotes(5);
            [Fact] public void Should_Return_Notes_In_Octave_6() => TestNotes(6);
            [Fact] public void Should_Return_Notes_In_Octave_7() => TestNotes(7);
            [Fact] public void Should_Return_Notes_In_Octave_8() => TestNotes(8);
            [Fact] public void Should_Return_Notes_In_Octave_9() => TestNotes(9);
            [Fact] public void Should_Return_Notes_In_Octave_10() => TestNotes(10);

            private static void TestNotes(int octave) 
            {
                var pitchFrom = octave * 12;

                for (var noteNum = 0; noteNum <= 11; noteNum++)
                {
                    var pitchNum = pitchFrom + noteNum;
                    if (pitchNum > 127) return;

                    var expectedNote = (Note)noteNum;
                    var pitch = (Pitch)(pitchNum);
                    Assert.Equal(expectedNote, pitch.Note());
                }
            }
        }
    }
}