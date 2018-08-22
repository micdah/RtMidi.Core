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
                var noteOffMessage = new NoteOffMessage(channel, key, velocity);
                _sut.Send(in noteOffMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(NoteOffMessage(channel, key, velocity), msg);
            })));
        }

        [Fact]
        public void Should_Send_NoteOnMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, velocity =>
            {
                var noteOnMessage = new NoteOnMessage(channel, key, velocity);
                _sut.Send(in noteOnMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(NoteOnMessage(channel, key, velocity), msg);
            })));
        }

        [Fact]
        public void Should_Send_PolyphonicKeyPressureMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, pressure =>
            {
                var polyphonicKeyPressureMessage = new PolyphonicKeyPressureMessage(channel, key, pressure);
                _sut.Send(in polyphonicKeyPressureMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(PolyphonicKeyPressureMessage(channel, key, pressure), msg);
            })));
        }

        [Fact]
        public void Should_Send_ControlChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, control => AllInRange(0, 127, value =>
            {
                var controlChangeMessage = new ControlChangeMessage(channel, control, value);
                _sut.Send(in controlChangeMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ControlChangeMessage(channel, control, value), msg);
            })));
        }

        [Fact]
        public void Should_Send_ProgramChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, program =>
            {
                var programChangeMessage = new ProgramChangeMessage(channel, program);
                _sut.Send(in programChangeMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ProgramChangeMessage(channel, program), msg);
            }));
        }

        [Fact]
        public void Should_Send_ChannelPressureMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, pressure =>
            {
                var channelPressureMessage = new ChannelPressureMessage(channel, pressure);
                _sut.Send(in channelPressureMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(ChannelPressureMessage(channel, pressure), msg);
            }));
        }

        [Fact]
        public void Should_Send_PitchBendMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 16383, value =>
            {
                var pitchBendMessage = new PitchBendMessage(channel, value);
                _sut.Send(in pitchBendMessage);
                Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));
                Assert.Equal(PitchBendMessage(channel, value), msg);
            }));
        }

        [Fact]
        public void Should_Send_NrpnMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                var nrpnMessage = new NrpnMessage(channel, parameter, value);
                _sut.Send(in nrpnMessage);

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

        [Fact]
        public void Should_Send_SysExMessage()
        {
            byte[] syx = {0x7E, 0x7F, 0x06, 0x01}; // Universal Device Inquiry message
            var sysExMessage = new SysExMessage(syx);            
            _sut.Send(in sysExMessage);
            Assert.True(_outputDeviceMock.Messages.TryDequeue(out var msg));

            Assert.Equal(Midi.Status.SysExStart, msg[0]);
            for (int i = 1; i < msg.Length - 1; i++)
                Assert.Equal(syx[i - 1], msg[i]);
            Assert.Equal(Midi.Status.SysExEnd, msg[msg.Length - 1]);
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
