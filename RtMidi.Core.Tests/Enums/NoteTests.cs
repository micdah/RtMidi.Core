using RtMidi.Core.Enums;
using Xunit;

namespace RtMidi.Core.Tests.Enums
{
    public class NoteTests
    {
        [Fact]
        public void Should_Return_Human_Readable_Display_Name()
        {
            Assert.Equal("C#", Note.CSharp.DisplayName());
            Assert.Equal("B", Note.B.DisplayName());
        }

        [Fact]
        public void Should_Return_Empty_String_For_Undefined_Enum_Values()
        {
            Assert.Equal(string.Empty, ((Note)12).DisplayName());
        }
    }
}