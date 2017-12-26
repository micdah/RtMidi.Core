using RtMidi.Core.Messages;
using RtMidi.Core.Unmanaged.Devices;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using RtMidi.Core.Devices;
using RtMidi.Core.Enums;

namespace RtMidi.Core.Tests
{
    public class MidiOutputDeviceTest : MidiDeviceTestBase
    {
        private readonly RtMidiOutputDeviceMock _outputDeviceMock;
        private readonly IMidiOutputDevice _sut;

        public MidiOutputDeviceTest(ITestOutputHelper output) : base(output)
        {
            _outputDeviceMock = new RtMidiOutputDeviceMock();
            _sut = new MidiOutputDevice(_outputDeviceMock, string.Empty);
        }

        [Fact]
        public void Should_Send_NoteOffMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, velocity =>
            {
                _sut.Send(new NoteOffMessage(channel, key, velocity));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(NoteOffMessage(channel, key, velocity), msg);
            })));
        }

        [Fact]
        public void Should_Send_NoteOnMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, velocity =>
            {
                _sut.Send(new NoteOnMessage(channel, key, velocity));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(NoteOnMessage(channel, key, velocity), msg);
            })));
        }

        [Fact]
        public void Should_Send_PolyphonicKeyPressureMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, pressure =>
            {
                _sut.Send(new PolyphonicKeyPressureMessage(channel, key, pressure));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(PolyphonicKeyPressureMessage(channel, key, pressure), msg);
            })));
        }

        [Fact]
        public void Should_Send_ControlChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, control => AllInRange(0, 127, value =>
            {
                _sut.Send(new ControlChangeMessage(channel, control, value));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ControlChangeMessage(channel, control, value), msg);
            })));
        }

        [Fact]
        public void Should_Send_ProgramChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, program =>
            {
                _sut.Send(new ProgramChangeMessage(channel, program));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ProgramChangeMessage(channel, program), msg);
            }));
        }

        [Fact]
        public void Should_Send_ChannelPressureMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, pressure =>
            {
                _sut.Send(new ChannelPressureMessage(channel, pressure));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ChannelPressureMessage(channel, pressure), msg);
            }));
        }

        [Fact]
        public void Should_Send_PitchBendMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 16383, value =>
            {
                _sut.Send(new PitchBendMessage(channel, value));
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(PitchBendMessage(channel, value), msg);
            }));
        }

        [Fact]
        public void Should_Send_NrpnMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                _sut.Send(new NrpnMessage(channel, parameter, value));

                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var parameterMsb));
                Assert.Equal(ControlChangeMessage(channel, (int)ControlFunction.NonRegisteredParameterNumberMSB, parameter >> 7), parameterMsb);

                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var parameterLsb));
                Assert.Equal(ControlChangeMessage(channel, (int)ControlFunction.NonRegisteredParameterNumberLSB, parameter & Midi.DataBitmask), parameterLsb);

                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var valueMsb));
                Assert.Equal(ControlChangeMessage(channel, (int)ControlFunction.DataEntryMSB, value >> 7), valueMsb);

                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var valueLsb));
                Assert.Equal(ControlChangeMessage(channel, (int)ControlFunction.LSBForControl6DataEntry, value & Midi.DataBitmask), valueLsb);
            })));
        }

        private class RtMidiOutputDeviceMock : IRtMidiOutputDevice
        {
            public readonly Queue<byte[]> Messages = new Queue<byte[]>();

            public bool IsOpen => true;

            public bool Open() => true;

            public void Close()
            {
            }

            public bool SendMessage(byte[] message)
            {
                Messages.Enqueue(message);
                return true;
            }

            public void Dispose()
            {
            }
        }
    }
}
