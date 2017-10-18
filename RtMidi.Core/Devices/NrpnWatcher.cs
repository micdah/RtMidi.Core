using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Serilog;

namespace RtMidi.Core.Devices
{
    internal class NrpnWatcher
    {
        private readonly ControlChangeMessage[] _messages;
        private readonly MidiInputDevice _inputDevice;
        private bool _queueMessages;
        private ControlFunction _lastControlFunction;
        private int _index;

        public NrpnWatcher(MidiInputDevice inputDevice)
        {
            _inputDevice = inputDevice;
            _lastControlFunction = ControlFunction.Undefined;
            _messages = new ControlChangeMessage[4];
            _index = 0;
        }

        public void HandleControlChangeMessage(ControlChangeMessage msg)
        {
            var controlFunction = msg.ControlFunction;
            if (_queueMessages)
            {
                _messages[_index++] = msg;
                
                // Check if messages are following the NRPN protocol
                if (IsExpectedControlFunction(controlFunction))
                {
                    // When we have all four messages, we can assemble and send it
                    if (_index == 4)
                    {
                        SendNrpn();
                    }
                }
                else
                {
                    // Messages cannot be a NRPN, release queue
                    ReleaseQueue();
                }
            } else if (controlFunction == ControlFunction.NonRegisteredParameterNumberMSB)
            {
                // Might be start of NRPN, start queueing messages
                _queueMessages = true;
                _messages[_index++] = msg;
            }
            else
            {
                _inputDevice.OnControlChange(msg);
            }

            _lastControlFunction = controlFunction;
        }

        private void ReleaseQueue()
        {
            for (var i = 0; i < _index; i++)
            {
                _inputDevice.OnControlChange(_messages[i]);
            }

            _index = 0;
            _queueMessages = false;
        }

        private void SendNrpn()
        {
            if (NrpnMessage.TryDecode(_messages, out var msg))
            {
                _inputDevice.OnNrpn(msg);
            }
            else Log.Error("Could not parse NRPN message");

            _index = 0;
            _queueMessages = false;
        }

        private bool IsExpectedControlFunction(ControlFunction controlFunction)
        {
            switch (_lastControlFunction)
            {
                case ControlFunction.NonRegisteredParameterNumberMSB:
                    return controlFunction == ControlFunction.NonRegisteredParameterNumberLSB;
                case ControlFunction.NonRegisteredParameterNumberLSB:
                    return controlFunction == ControlFunction.DataEntryMSB;
                case ControlFunction.DataEntryMSB:
                    return controlFunction == ControlFunction.LSBForControl6DataEntry;
                default:
                    return false;
            }
        }
    }
}