using RtMidi.Core.Enums;
using Xunit;

namespace RtMidi.Core.Tests.Enums
{
    public class ControlFunctionTests
    {
        [Fact]
        public void Should_Return_Human_Readable_Display_Name()
        {
            Assert.Equal("Non-Registered Parameter Number LSB (NRPN)", ControlFunction.NonRegisteredParameterNumberLSB.DisplayName());
            Assert.Equal("Data Decrement", ControlFunction.DataDecrement.DisplayName());
        }

        [Fact]
        public void Should_Return_Empty_String_For_Undefined_Enum_Values()
        {
            Assert.Equal(string.Empty, ((ControlFunction)128).DisplayName());
        }
    }
}