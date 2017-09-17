using Xunit;
using RtMidi.Core.Enums;
using System;

namespace RtMidi.Core.Tests
{
    public class KeyTests
    {
        public class OctaveTests
        {
            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Key_Below_0()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var key = (Key)(-1);
                    key.Octave();
                });
            }

            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Key_Above_127()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var key = (Key)(128);
                    key.Octave();
                });
            }

            [Fact] public void Should_Return_0_For_Key_0_Through_11() => TestOctave(0, 11, 0);
            [Fact] public void Should_Return_1_For_Key_12_Through_23() => TestOctave(12, 23, 1);
            [Fact] public void Should_Return_2_For_Key_24_Through_35() => TestOctave(24, 35, 2);
            [Fact] public void Should_Return_3_For_Key_36_Through_47() => TestOctave(36, 47, 3);
            [Fact] public void Should_Return_4_For_Key_48_Through_59() => TestOctave(48, 59, 4);
            [Fact] public void Should_Return_5_For_Key_60_Through_71() => TestOctave(60, 71, 5);
            [Fact] public void Should_Return_6_For_Key_72_Through_83() => TestOctave(72, 83, 6);
            [Fact] public void Should_Return_7_For_Key_84_Through_95() => TestOctave(84, 95, 7);
            [Fact] public void Should_Return_8_For_Key_96_Through_107() => TestOctave(96, 107, 8);
            [Fact] public void Should_Return_9_For_Key_108_Through_119() => TestOctave(108, 119, 9);
            [Fact] public void Should_Return_10_For_Key_120_Through_127() => TestOctave(120, 127, 10);

            private static void TestOctave(int keyFrom, int keyTo, int expectedOctate)
            {
                for (var i = keyFrom; i <= keyTo; i++)
                {
                    var key = (Key)i;
                    Assert.Equal(expectedOctate, key.Octave());
                }
            }
        }

        public class NoteTests
        {
            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Key_Below_0() 
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var key = (Key)(-1);
                    key.Note();
                });
            }

            [Fact]
            public void Should_Throw_ArgumentOutOfRange_When_Key_Above_127()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var key = (Key)(128);
                    key.Note();
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
                var keyFrom = octave * 12;

                for (var noteNum = 0; noteNum <= 11; noteNum++)
                {
                    var keyNum = keyFrom + noteNum;
                    if (keyNum > 127) return;

                    var expectedNote = (Note)noteNum;
                    var key = (Key)(keyNum);
                    Assert.Equal(expectedNote, key.Note());
                }
            }
        }
    }
}