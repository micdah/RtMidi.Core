using System.Collections.Generic;
using RtMidi.Core.Devices;
using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Xunit;
using Xunit.Abstractions;
using RtMidi.Core.Devices.Nrpn;

namespace RtMidi.Core.Tests
{
    public class MidiInputDeviceTests : MidiDeviceTestBase
    {
        private readonly RtMidiInputDeviceMock _inputDeviceMock;
        private readonly MidiInputDevice _sut;
        private readonly Queue<NoteOffMessage> _noteOffMessages = new Queue<NoteOffMessage>();
        private readonly Queue<NoteOnMessage> _noteOnMessages = new Queue<NoteOnMessage>();
        private readonly Queue<PolyphonicKeyPressureMessage> _polyphonicKeyPressureMessages = new Queue<PolyphonicKeyPressureMessage>();
        private readonly Queue<ControlChangeMessage> _controlChangeMessages = new Queue<ControlChangeMessage>();
        private readonly Queue<ProgramChangeMessage> _programChangeMessages = new Queue<ProgramChangeMessage>();
        private readonly Queue<ChannelPressureMessage> _channelPressureMessages = new Queue<ChannelPressureMessage>();
        private readonly Queue<PitchBendMessage> _pitchBendMessages = new Queue<PitchBendMessage>();
        private readonly Queue<NrpnMessage> _nrpnMessages = new Queue<NrpnMessage>();
        private readonly Queue<SysExMessage> _sysExMessages = new Queue<SysExMessage>();
        private readonly Queue<MidiTimeCodeQuarterFrameMessage> _midiTimeCodeQuarterFrameMessages = new Queue<MidiTimeCodeQuarterFrameMessage>();
        private readonly Queue<SongPositionPointerMessage> _songPositionPointerMessages = new Queue<SongPositionPointerMessage>();
        private readonly Queue<SongSelectMessage> _songSelectMessages = new Queue<SongSelectMessage>();
        private readonly Queue<TuneRequestMessage> _tuneRequestMessages = new Queue<TuneRequestMessage>();
        
        public MidiInputDeviceTests(ITestOutputHelper output) : base(output)
        {
            _inputDeviceMock = new RtMidiInputDeviceMock();
            _sut = new MidiInputDevice(_inputDeviceMock, string.Empty);

            _sut.NoteOff += (IMidiInputDevice sender, in NoteOffMessage e) => _noteOffMessages.Enqueue(e);
            _sut.NoteOn += (IMidiInputDevice sender, in NoteOnMessage e) => _noteOnMessages.Enqueue(e);
            _sut.PolyphonicKeyPressure += (IMidiInputDevice sender, in PolyphonicKeyPressureMessage e) => _polyphonicKeyPressureMessages.Enqueue(e);
            _sut.ControlChange += (IMidiInputDevice sender, in ControlChangeMessage e) => _controlChangeMessages.Enqueue(e);
            _sut.ProgramChange += (IMidiInputDevice sender, in ProgramChangeMessage e) => _programChangeMessages.Enqueue(e);
            _sut.ChannelPressure += (IMidiInputDevice sender, in ChannelPressureMessage e) => _channelPressureMessages.Enqueue(e);
            _sut.PitchBend += (IMidiInputDevice sender, in PitchBendMessage e) => _pitchBendMessages.Enqueue(e);
            _sut.Nrpn += (IMidiInputDevice sender, in NrpnMessage e) => _nrpnMessages.Enqueue(e);
            _sut.SysEx += (IMidiInputDevice sender, in SysExMessage e) => _sysExMessages.Enqueue(e);
            _sut.MidiTimeCodeQuarterFrame += (IMidiInputDevice sender, in MidiTimeCodeQuarterFrameMessage e) => _midiTimeCodeQuarterFrameMessages.Enqueue(e);
            _sut.SongPositionPointer += (IMidiInputDevice sender, in SongPositionPointerMessage e) => _songPositionPointerMessages.Enqueue(e);
            _sut.SongSelect += (IMidiInputDevice sender, in SongSelectMessage e) => _songSelectMessages.Enqueue(e);
            _sut.TuneRequest += (IMidiInputDevice sender, in TuneRequestMessage e) => _tuneRequestMessages.Enqueue(e);
        }

        [Fact]
        public void Test_NoteOffMessage()
        {
            var noteOffMsg = NoteOffMessage(Channel.Channel4, Key.Key127, 11);
            Assert.NotNull(noteOffMsg);
            Assert.Equal(3, noteOffMsg.Length);
            Assert.Equal(0b1000_0011, noteOffMsg[0]);   // Channel
            Assert.Equal(0b0111_1111, noteOffMsg[1]);   // Key
            Assert.Equal(0b0000_1011, noteOffMsg[2]);   // Velocity
        }

        [Fact]
        public void Should_Fire_Events_On_Channel_1_Through_16()
        {
            AllEnums<Channel>(channel =>
            {
                _inputDeviceMock.OnMessage(NoteOffMessage(channel));
                Assert.True(_noteOffMessages.TryDequeue(out var noteOffMessage),
                    $"Expected Note Off message for channel={channel}");
                Assert.Equal(channel, noteOffMessage.Channel);
            });
        }

        [Fact]
        public void Should_Fire_NoteOffMessages() 
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0,127, velocity =>
            {
                _inputDeviceMock.OnMessage(NoteOffMessage(channel, key, velocity));
                Assert.True(_noteOffMessages.TryDequeue(out var msg),
                    $"Expected Note Off message for channel={channel} key={key} velocity={velocity}");

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(key, msg.Key);
                Assert.Equal(velocity, msg.Velocity);
            })));
        }

        [Fact]
        public void Should_Fire_NoteOnMessages()
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0, 127, velocity =>
            {
                _inputDeviceMock.OnMessage(NoteOnMessage(channel, key, velocity));
                Assert.True(_noteOnMessages.TryDequeue(out var msg),
                    $"Expected Note On message for channel={channel} key={key} velocity={velocity}");

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(key, msg.Key);
                Assert.Equal(velocity, msg.Velocity);
            })));
        }

        [Fact]
        public void Should_Fire_PolyphonicKeyPressureMessages() 
        {
            AllEnums<Channel>(channel => AllEnums<Key>(key => AllInRange(0,127, pressure => 
            {
                _inputDeviceMock.OnMessage(PolyphonicKeyPressureMessage(channel, key, pressure));
                Assert.True(_polyphonicKeyPressureMessages.TryDequeue(out var msg),
                    $"Expected Polyphonic Key Pressure message for channel={channel} key={key} pressure={pressure}");

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(key, msg.Key);
                Assert.Equal(pressure, msg.Pressure);
            })));
        }

        [Fact]
        public void Should_Fire_ControlChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, control => AllInRange(0, 127, value =>
            {
                _inputDeviceMock.OnMessage(ControlChangeMessage(channel, control, value));

                // Flush CC out of NRPN watcher
                if (control == (int)ControlFunction.NonRegisteredParameterNumberMSB)
                {
                    _inputDeviceMock.OnMessage(ControlChangeMessage(channel, 0, value));
                }
                
                Assert.True(_controlChangeMessages.TryDequeue(out var msg),
                    $"Expected Control Change message for channel={channel} control={control} value={value}");

                if (control == (int) ControlFunction.NonRegisteredParameterNumberMSB)
                {
                    Assert.True(_controlChangeMessages.TryDequeue(out var _), "Expected extra flushing message");
                }

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(control, msg.Control);
                Assert.Equal(value, msg.Value);
            })));
        }

        [Fact]
        public void Should_Fire_ProgramChangeMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, program =>
            {
                _inputDeviceMock.OnMessage(ProgramChangeMessage(channel, program));
                Assert.True(_programChangeMessages.TryDequeue(out var msg),
                    $"Expected Program Change message for channel={channel} program={program}");

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(program, msg.Program);
            }));
        }

        [Fact]
        public void Should_Fire_ChannelPressureMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 127, pressure =>
            {
                _inputDeviceMock.OnMessage(ChannelPressureMessage(channel, pressure));
                Assert.True(_channelPressureMessages.TryDequeue(out var msg));

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(pressure, msg.Pressure);
            }));
        }

        [Fact]
        public void Should_Fire_PitchBendMessages()
        {
            AllEnums<Channel>(channel => AllInRange(0, 16383, value =>
            {
                _inputDeviceMock.OnMessage(PitchBendMessage(channel, value));
                Assert.True(_pitchBendMessages.TryDequeue(out var msg));

                Assert.Equal(channel, msg.Channel);
                Assert.Equal(value, msg.Value);
            }));
        }

        [Fact]
        public void Should_Fire_NRPNMessage()
        {
            _sut.SetNrpnMode(NrpnMode.On);

            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                var parameterMsb = parameter >> 7;
                var parameterLsb = parameter & Midi.DataBitmask;
                var valueMsb = value >> 7;
                var valueLsb = value & Midi.DataBitmask;

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel, 
                    (int) ControlFunction.NonRegisteredParameterNumberMSB, parameterMsb));
                Assert.False(_controlChangeMessages.TryDequeue(out var _));

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberLSB, parameterLsb));
                Assert.False(_controlChangeMessages.TryDequeue(out var _));

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.DataEntryMSB, valueMsb));
                Assert.False(_controlChangeMessages.TryDequeue(out var _));

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.LSBForControl6DataEntry, valueLsb));
                Assert.False(_controlChangeMessages.TryDequeue(out var _));

                Assert.True(_nrpnMessages.TryDequeue(out var msg));
                Assert.Equal(channel, msg.Channel);
                Assert.Equal(parameter, msg.Parameter);
                Assert.Equal(value, msg.Value);
            })));
        }

        [Fact]
        public void Should_Fire_NRPNMessage_And_ControlChange() 
        {
            _sut.SetNrpnMode(NrpnMode.OnSendControlChange);

            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                var parameterMsb = parameter >> 7;
                var parameterLsb = parameter & Midi.DataBitmask;
                var valueMsb = value >> 7;
                var valueLsb = value & Midi.DataBitmask;

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberMSB, parameterMsb));
                
                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamMsb));
                Assert.Equal(channel, ccParamMsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberMSB, ccParamMsb.ControlFunction);
                Assert.Equal(parameterMsb, ccParamMsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberLSB, parameterLsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamLsb));
                Assert.Equal(channel, ccParamLsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberLSB, ccParamLsb.ControlFunction);
                Assert.Equal(parameterLsb, ccParamLsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.DataEntryMSB, valueMsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccValueMsb));
                Assert.Equal(channel, ccValueMsb.Channel);
                Assert.Equal(ControlFunction.DataEntryMSB, ccValueMsb.ControlFunction);
                Assert.Equal(valueMsb, ccValueMsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.LSBForControl6DataEntry, valueLsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccValueLsb));
                Assert.Equal(channel, ccValueLsb.Channel);
                Assert.Equal(ControlFunction.LSBForControl6DataEntry, ccValueLsb.ControlFunction);
                Assert.Equal(valueLsb, ccValueLsb.Value);

                Assert.True(_nrpnMessages.TryDequeue(out var msg));
                Assert.Equal(channel, msg.Channel);
                Assert.Equal(parameter, msg.Parameter);
                Assert.Equal(value, msg.Value);
            })));
        }

        [Fact]
        public void Should_Not_Fire_NRPNMessage()
        {
            _sut.SetNrpnMode(NrpnMode.Off);

            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                var parameterMsb = parameter >> 7;
                var parameterLsb = parameter & Midi.DataBitmask;
                var valueMsb = value >> 7;
                var valueLsb = value & Midi.DataBitmask;

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberMSB, parameterMsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamMsb));
                Assert.Equal(channel, ccParamMsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberMSB, ccParamMsb.ControlFunction);
                Assert.Equal(parameterMsb, ccParamMsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberLSB, parameterLsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamLsb));
                Assert.Equal(channel, ccParamLsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberLSB, ccParamLsb.ControlFunction);
                Assert.Equal(parameterLsb, ccParamLsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.DataEntryMSB, valueMsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccValueMsb));
                Assert.Equal(channel, ccValueMsb.Channel);
                Assert.Equal(ControlFunction.DataEntryMSB, ccValueMsb.ControlFunction);
                Assert.Equal(valueMsb, ccValueMsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.LSBForControl6DataEntry, valueLsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccValueLsb));
                Assert.Equal(channel, ccValueLsb.Channel);
                Assert.Equal(ControlFunction.LSBForControl6DataEntry, ccValueLsb.ControlFunction);
                Assert.Equal(valueLsb, ccValueLsb.Value);

                Assert.False(_nrpnMessages.TryDequeue(out var _));
            })));
        }

        [Fact]
        public void Should_Not_Release_Queued_ControlChange_Messages_When_NRPN_Fails_When_NrpnMode_On_With_Sending_ControlChange()
        {
            _sut.SetNrpnMode(NrpnMode.OnSendControlChange);

            AllEnums<Channel>(channel => AllInRange(0, 16383, 128, parameter => AllInRange(0, 16383, 128, value =>
            {
                var parameterMsb = parameter >> 7;
                var parameterLsb = parameter & Midi.DataBitmask;
                var valueMsb = value >> 7;

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberMSB, parameterMsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamMsb));
                Assert.Equal(channel, ccParamMsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberMSB, ccParamMsb.ControlFunction);
                Assert.Equal(parameterMsb, ccParamMsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.NonRegisteredParameterNumberLSB, parameterLsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccParamLsb));
                Assert.Equal(channel, ccParamLsb.Channel);
                Assert.Equal(ControlFunction.NonRegisteredParameterNumberLSB, ccParamLsb.ControlFunction);
                Assert.Equal(parameterLsb, ccParamLsb.Value);

                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.DataEntryMSB, valueMsb));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccValueMsb));
                Assert.Equal(channel, ccValueMsb.Channel);
                Assert.Equal(ControlFunction.DataEntryMSB, ccValueMsb.ControlFunction);
                Assert.Equal(valueMsb, ccValueMsb.Value);

                // Now send unexpected CC message, which normally would release the CC queue
                _inputDeviceMock.OnMessage(ControlChangeMessage(channel,
                    (int)ControlFunction.Balance, 0));

                Assert.False(_nrpnMessages.TryDequeue(out var _));

                Assert.True(_controlChangeMessages.TryDequeue(out var ccMsg));
                Assert.Equal(channel, ccMsg.Channel);
                Assert.Equal(ControlFunction.Balance, ccMsg.ControlFunction);
                Assert.Equal(0, ccMsg.Value);

                // Verify there should be no more messages
                Assert.False(_controlChangeMessages.TryDequeue(out var _));
            })));
        }

        [Fact]
        public void Should_Fire_SysExMessage()
        {
            byte[] syx = {0x7E, 0x7F, 0x06, 0x02}; // Universal Device Inquiry Response
            _inputDeviceMock.OnMessage(SysExMessage(syx));
            Assert.True(_sysExMessages.TryDequeue(out var msg),
                $"Expected SysEx message for {string.Join(", ", syx)}");
            
            for (int i = 0; i < msg.Data.Length; i++)
                Assert.Equal(syx[i], msg.Data[i]);
        }

        [Fact]
        public void Should_Fire_MidiTimeCodeQuarterFrameMessages()
        {
            AllInRange(0, 7, messageType => 
                AllInRange(0,7, values =>
                {
                    _inputDeviceMock.OnMessage(MidiTimeCodeQuarterFrameMessage(messageType, values));

                    Assert.True(_midiTimeCodeQuarterFrameMessages.TryDequeue(out var msg));
                    Assert.Equal(messageType, msg.MessageType);
                    Assert.Equal(values, msg.Values);
                }));
        }

        [Fact]
        public void Should_Fire_SongPositionPointerMessages()
        {
            AllInRange(0, 16383, midiBeats =>
            {
                _inputDeviceMock.OnMessage(SongPositionPointerMessage(midiBeats));

                Assert.True(_songPositionPointerMessages.TryDequeue(out var msg));
                Assert.Equal(midiBeats, msg.MidiBeats);
            });
        }

        [Fact]
        public void Should_Fire_SongSelectMessages()
        {
            AllInRange(0, 127, song =>
            {
                _inputDeviceMock.OnMessage(SongSelectMessage(song));

                Assert.True(_songSelectMessages.TryDequeue(out var msg));
                Assert.Equal(song, msg.Song);
            });
        }

        [Fact]
        public void Should_Fire_TuneRequestMessage()
        {
            _inputDeviceMock.OnMessage(TuneRequestMessage());

            Assert.True(_tuneRequestMessages.TryDequeue(out var _));
        }
    }
}