using System;
using System.Diagnostics;
using RtMidi.Core.Messages;
using RtMidi.Core.Unmanaged.Devices;
using Serilog;
using RtMidi.Core.Devices.Nrpn;

namespace RtMidi.Core.Devices
{
    internal class MidiInputDevice : MidiDevice, IMidiInputDevice
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<MidiInputDevice>();
        private readonly IRtMidiInputDevice _inputDevice;
        private readonly NrpnInterpreter[] _nrpnInterpreters;

        public MidiInputDevice(IRtMidiInputDevice rtMidiInputDevice, string name) : base(rtMidiInputDevice, name)
        {
            _inputDevice = rtMidiInputDevice;
            _inputDevice.Message += RtMidiInputDevice_Message;

            _nrpnInterpreters = new NrpnInterpreter[16];
            for (var i = 0; i < 16; i++)
            {
                _nrpnInterpreters[i] = new NrpnInterpreter(OnControlChange, OnNrpn);
            }

            // Set default NRPN mode
            SetNrpnMode(NrpnMode.On);
        }
        
        public event NoteOffMessageHandler NoteOff;
        public event NoteOnMessageHandler NoteOn;
        public event PolyphonicKeyPressureMessageHandler PolyphonicKeyPressure;
        public event ControlChangeMessageHandler ControlChange;
        public event ProgramChangeMessageHandler ProgramChange;
        public event ChannelPressureMessageHandler ChannelPressure;
        public event PitchBendMessageHandler PitchBend;
        public event NrpnMessageHandler Nrpn;
        public event SysExMessageHandler SysEx;
        public event ClockMessageHandler Clock;
        public event StartMessageHandler Start;
        public event StopMessageHandler Stop;
        public event ContinueMessageHandler Continue;
        public event SongPositionPointerMessageHandler SongPositionPointer;

        private void RtMidiInputDevice_Message(object sender, RtMidiReceivedEventArgs message)
        {
            if (message.Data == null)
            {
                Log.Error("Received null message from device");
                return;
            }

            if (message.Data.Length == 0) 
            {
                Log.Error("Received empty message from device");
                return;
            }

            // TODO Decode and propagate midi events on separate thread as not to block receiving thread

            try 
            {
                Decode(message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception occurred while decoding midi message");
            }
        }

        private void Decode(RtMidiReceivedEventArgs message)
        {
            var messageData = message.Data;
            byte status = messageData[0];
            switch (status & 0b1111_0000)
            {
                case Midi.Status.NoteOffBitmask:
                    if (NoteOffMessage.TryDecode(message.Timestamp, messageData, out var noteOffMessage))
                        NoteOff?.Invoke(this, in noteOffMessage);
                    break;
                case Midi.Status.NoteOnBitmask:
                    if (NoteOnMessage.TryDecode(message.Timestamp, messageData, out var noteOnMessage))
                        NoteOn?.Invoke(this, in noteOnMessage);
                    break;
                case Midi.Status.PolyphonicKeyPressureBitmask:
                    if (PolyphonicKeyPressureMessage.TryDecode(message.Timestamp, messageData, out var polyphonicKeyPressureMessage))
                        PolyphonicKeyPressure?.Invoke(this, in polyphonicKeyPressureMessage);
                    break;
                case Midi.Status.ControlChangeBitmask:
                    if (ControlChangeMessage.TryDecode(message.Timestamp, messageData, out var controlChangeMessage))
                        _nrpnInterpreters[(int)controlChangeMessage.Channel].HandleControlChangeMessage(in controlChangeMessage);
                    break;
                case Midi.Status.ProgramChangeBitmask:
                    if (ProgramChangeMessage.TryDecode(message.Timestamp, messageData, out var programChangeMessage))
                        ProgramChange?.Invoke(this, in programChangeMessage);
                    break;
                case Midi.Status.ChannelPressureBitmask:
                    if (ChannelPressureMessage.TryDecode(message.Timestamp, messageData, out var channelPressureMessage))
                        ChannelPressure?.Invoke(this, in channelPressureMessage);
                    break;
                case Midi.Status.PitchBendChange:
                    if (PitchBendMessage.TryDecode(message.Timestamp, messageData, out var pitchBendMessage))
                        PitchBend?.Invoke(this, in pitchBendMessage);
                    break;

                case Midi.Status.System:
                    switch (status)
                    {
                        case Midi.Status.SysExStart:
                            if (SysExMessage.TryDecode(message.Timestamp, messageData, out var sysExMessage))
                                SysEx?.Invoke(this, in sysExMessage);
                            break;
                        case Midi.Status.SongPositionPointer:
                            if (SongPositionPointerMessage.TryDecode(message.Timestamp, messageData, out var songPositionPointerMessage))
                                SongPositionPointer?.Invoke(this, in songPositionPointerMessage);
                            break;
                        case Midi.Status.Clock:
                            Clock?.Invoke(this, new ClockMessage(message.Timestamp));
                            break;
                        case Midi.Status.Start:
                            Start?.Invoke(this, new StartMessage(message.Timestamp));
                            break;
                        case Midi.Status.Stop:
                            Stop?.Invoke(this, new StopMessage(message.Timestamp));
                            break;
                        case Midi.Status.Continue:
                            Continue?.Invoke(this, new ContinueMessage(message.Timestamp));
                            break;
                        default:
                            Debug.WriteLine("Unknown system message type {Status}", $"{status:X2}");
                            Log.Error("Unknown system message type {Status}", $"{status:X2}");
                            break;
                    }
                    break;
                
                default:
                    Debug.WriteLine("Unknown message type {Bitmask}", $"{status & 0b1111_0000:X2}");
                    Log.Error("Unknown message type {Bitmask}", $"{status & 0b1111_0000:X2}");
                    break;
            }
        }
        
        protected override void Disposing()
        {
            _inputDevice.Message -= RtMidiInputDevice_Message;
            
            // Clear all subscribers
            NoteOff = null;
            NoteOn = null;
            PolyphonicKeyPressure = null;
            ControlChange = null;
            ProgramChange = null;
            ChannelPressure = null;
            PitchBend = null;
            SysEx = null;
            Clock = null;
            Start = null;
            Stop = null;
            Continue = null;
            SongPositionPointer = null;
        }

        private void OnControlChange(in ControlChangeMessage e)
        {
            ControlChange?.Invoke(this, in e);
        }

        private void OnNrpn(in NrpnMessage e)
        {
            Nrpn?.Invoke(this, in e);
        }
        
        public void SetNrpnMode(NrpnMode nrpnMode)
        {
            foreach (var nrpnInterpreter in _nrpnInterpreters)
            {
                nrpnInterpreter.SetNrpnMode(nrpnMode);
            }
        }
    }
}
