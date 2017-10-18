using RtMidi.Core.Enums;
using Xunit;

namespace RtMidi.Core.Tests.Enums
{
    public class ChannelTests
    {
        [Fact]
        public void Should_Return_Human_Readable_Display_Name()
        {
            Assert.Equal("Channel 1", Channel.Channel1.DisplayName());
            Assert.Equal("Channel 16", Channel.Channel16.DisplayName());
        }

        [Fact]
        public void Should_Return_Empty_String_For_Undefined_Enum_Values()
        {
            Assert.Equal(string.Empty, ((Channel) 17).DisplayName());
        }
    }
}
