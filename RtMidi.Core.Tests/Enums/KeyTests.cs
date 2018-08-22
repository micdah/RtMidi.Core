using RtMidi.Core.Enums;
using Xunit;

namespace RtMidi.Core.Tests.Enums
{
    public class KeyTests
    {
        [Fact]
        public void Should_Return_Human_Readable_Display_Name()
        {
            Assert.Equal("Key 0", Key.Key0.DisplayName());
            Assert.Equal("Key 127", Key.Key127.DisplayName());
        }

        [Fact]
        public void Should_Return_Empty_String_For_Undefined_Enum_Values()
        {
            Assert.Equal(string.Empty, ((Key) 128).DisplayName());
        }
    }
}