using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Xunit;
using Xunit.Abstractions;

namespace RtMidi.Core.Tests.Messages
{
    public class ControlChangeMessageTests : TestBase
    {
        public ControlChangeMessageTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Should_Return_ControlFunction_When_Control_Is_Defined()
        {
            // Setup
            var msg = new ControlChangeMessage(Channel.Channel1, (int) ControlFunction.NonRegisteredParameterNumberLSB, 0);

            // Test
            var controlFunction = msg.ControlFunction;

            // Verify
            Assert.Equal(ControlFunction.NonRegisteredParameterNumberLSB, controlFunction);
        }

        [Fact]
        public void Should_Return_Undefined_ControlFunction_When_Control_Is_Not_Defined()
        {
            // Setup
            var msg = new ControlChangeMessage(Channel.Channel1, 102, 0);

            // Test
            var controlFunction = msg.ControlFunction;

            // Verify
            Assert.Equal(ControlFunction.Undefined, controlFunction);
        }
    }
}
