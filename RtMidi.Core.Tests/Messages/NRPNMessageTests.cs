using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Xunit;
using Xunit.Abstractions;

namespace RtMidi.Core.Tests.Messages
{
    public class NRPNMessageTests : TestBase
    {
        public NRPNMessageTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Should_Recognize_NRPN_Message_Including_Partials()
        {
            Assert.True(NrpnMessage.IsExpectedControls(ControlChangeMessages(Channel.Channel1,
                ControlFunction.NonRegisteredParameterNumberMSB)));

            Assert.True(NrpnMessage.IsExpectedControls(ControlChangeMessages(Channel.Channel1,
                ControlFunction.NonRegisteredParameterNumberMSB,
                ControlFunction.NonRegisteredParameterNumberLSB)));

            Assert.True(NrpnMessage.IsExpectedControls(ControlChangeMessages(Channel.Channel1,
                ControlFunction.NonRegisteredParameterNumberMSB,
                ControlFunction.NonRegisteredParameterNumberLSB,
                ControlFunction.DataEntryMSB)));

            Assert.True(NrpnMessage.IsExpectedControls(ControlChangeMessages(Channel.Channel1,
                ControlFunction.NonRegisteredParameterNumberMSB,
                ControlFunction.NonRegisteredParameterNumberLSB,
                ControlFunction.DataEntryMSB,
                ControlFunction.LSBForControl6DataEntry)));
        }

        [Fact]
        public void Should_Not_Recognize_NRPN_Message_If_Different_Channels()
        {
            for (var i = 1; i < 4; i++)
            {
                // Setup
                var msgs = ControlChangeMessages(Channel.Channel1,
                    ControlFunction.NonRegisteredParameterNumberMSB,
                    ControlFunction.NonRegisteredParameterNumberLSB,
                    ControlFunction.DataEntryMSB,
                    ControlFunction.LSBForControl6DataEntry);
                msgs[i] = new ControlChangeMessage(Channel.Channel2, msgs[i].Control, msgs[i].Value);

                // Test
                Assert.False(NrpnMessage.IsExpectedControls(msgs));
            }
        }

        private static ControlChangeMessage[] ControlChangeMessages(Channel channel, params ControlFunction[] controlFunctions)
        {
            var msgs = new ControlChangeMessage[controlFunctions.Length];
            for (var i = 0; i < controlFunctions.Length; i++)
            {
                msgs[i] = new ControlChangeMessage(channel, (int)controlFunctions[i], 0);
            }
            return msgs;
        }
    }
}