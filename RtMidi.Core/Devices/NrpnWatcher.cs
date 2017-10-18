using System.Collections.Generic;
using RtMidi.Core.Enums;
using RtMidi.Core.Messages;
using Serilog;

namespace RtMidi.Core.Devices
{
    internal class NrpnWatcher
    {
        private readonly Queue<ControlChangeMessage> _messageQueue = new Queue<ControlChangeMessage>(4);
        private readonly MidiInputDevice _inputDevice;
        private bool _queueMessages;
        private ControlFunction _lastControlFunction;

        public NrpnWatcher(MidiInputDevice inputDevice)
        {
            _inputDevice = inputDevice;
            _lastControlFunction = ControlFunction.Undefined;
        }

        public void HandleControlChangeMessage(ControlChangeMessage msg)
        {
            var controlFunction = msg.ControlFunction;
            if (_queueMessages)
            {
                _messageQueue.Enqueue(msg);

                // Check if messages are following the NRPN protocol
                if (IsExpectedControlFunction(controlFunction))
                {
                    // When we have all four messages, we can assemble and send it
                    if (_messageQueue.Count == 4)
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
                _messageQueue.Enqueue(msg);
            }
            else
            {
                _inputDevice.OnControlChange(msg);
            }

            _lastControlFunction = controlFunction;
        }

        private void ReleaseQueue()
        {
            while (_messageQueue.Count > 0)
            {
                _inputDevice.OnControlChange(_messageQueue.Dequeue());
            }

            _queueMessages = false;
        }

        private void SendNrpn()
        {
            if (NRPNMessage.TryDecode(new[]
            {
                _messageQueue.Dequeue(),
                _messageQueue.Dequeue(),
                _messageQueue.Dequeue(),
                _messageQueue.Dequeue()
            }, out var msg))
            {
                _inputDevice.OnNrpn(msg);
            }
            else
            {
                Log.Fatal("Could not parse NRPN message");
            }

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