using System;
using System.Linq;
using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Xunit;
using Xunit.Abstractions;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Tests
{
    public class MidiDeviceTestBase : TestBase
    {
        public MidiDeviceTestBase(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Should_Separate_Lsb_And_Msb_For_Pitch_Bend()
        {
            var msg = PitchBendMessage(Channel.Channel1, 5482);

            Assert.Equal(3, msg.Length);
            var lsb = msg[1];
            var msb = msg[2];

            Assert.Equal(0b0110_1010, lsb);
            Assert.Equal(0b0010_1010, msb);
        }

        protected static void AllEnums<TEnum>(Action<TEnum> func)
            where TEnum : struct
        {
            foreach (var enumValue in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
            {
                func(enumValue);
            }
        }

        protected static void AllInRange(int from, int to, Action<int> func)
        {
            for (var i = from; i <= to; i++)
            {
                func(i);
            }
        }

        protected static void AllInRange(int from, int to, int increment, Action<int> func)
        {
            for (var i = from; i <= to; i += increment)
            {
                func(i);
            }
        }

        protected static byte[] NoteOffMessage(Channel channel, Key key = Key.Key0, int velocity = 0)
            => new[]
            {
            StructHelper.StatusByte(Midi.Status.NoteOffBitmask, channel),
                StructHelper.DataByte(key),
                StructHelper.DataByte(velocity)
            };

        protected static byte[] NoteOnMessage(Channel channel, Key key = Key.Key0, int velocity = 0)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.NoteOnBitmask, channel),
                StructHelper.DataByte(key),
                StructHelper.DataByte(velocity)
            };

        protected static byte[] PolyphonicKeyPressureMessage(Channel channel, Key key = Key.Key0, int pressure = 0)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.PolyphonicKeyPressureBitmask, channel),
                StructHelper.DataByte(key),
                StructHelper.DataByte(pressure)
            };

        protected static byte[] ControlChangeMessage(Channel channel, int control, int value)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.ControlChangeBitmask, channel),
                StructHelper.DataByte(control),
                StructHelper.DataByte(value)
            };

        protected static byte[] ProgramChangeMessage(Channel channel, int program)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.ProgramChangeBitmask, channel),
                StructHelper.DataByte(program)
            };

        protected static byte[] ChannelPressureMessage(Channel channel, int pressure)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.ChannelPressureBitmask, channel),
                StructHelper.DataByte(pressure)
            };

        protected static byte[] PitchBendMessage(Channel channel, int value)
            => new[]
            {
                StructHelper.StatusByte(Midi.Status.PitchBendChange, channel),
                StructHelper.DataByte(value & 0b0111_1111),
                StructHelper.DataByte(value >> 7)
            };
    }
}