using System;
using System.Linq;
using RtMidi.Core.Devices;

namespace RtMidi.Core.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
        }

        private readonly IMidiInputDevice _inputDevice;

        public Program() 
        {
            Console.WriteLine("Available MIDI API's:");
            var apis = MidiDeviceManager.Instance.GetAvailableMidiApis();
            foreach (var api in apis)
                Console.WriteLine($"API: {api}");

            Console.WriteLine("Available MIDI devices:");
            var inputDevices = MidiDeviceManager.Instance.InputDevices.ToList();
            foreach (var device in inputDevices)
                Console.WriteLine($"Input Device: {device.Name}");

            var outputDevices = MidiDeviceManager.Instance.OutputDevices.ToList();
            foreach (var device in outputDevices)
                Console.WriteLine($"Output Device: {device.Name}");

            _inputDevice = inputDevices.First().CreateDevice();

            try {
                _inputDevice.ControlChange += (_, msg) => Console.WriteLine($"Received Control Change: {msg}");
                _inputDevice.Nrpn += (_, msg) => Console.WriteLine($"Received NRPN: {msg}");
                _inputDevice.Open();

                Console.ReadLine();
            }
            finally
            {
                _inputDevice.Dispose();
            }
        }

        private void InputDevice_Message(object sender, byte[] message)
        {
            var msg = string.Join(" ", message.Select(b => $"{b:X2}/{b}"));
            Console.WriteLine($"Received: {msg} (length {message.Length})");
        }
    }
}
