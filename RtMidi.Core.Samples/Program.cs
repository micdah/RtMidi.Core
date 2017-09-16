using System;
using System.Linq;
using RtMidi.Core.Unmanaged;
using RtMidi.Core.Unmanaged.Devices;

namespace RtMidi.Core.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
        }

        private readonly IRtMidiInputDevice _inputDevice;

        public Program() 
        {
            Console.WriteLine("Available MIDI API's:");
            var apis = RtMidiApiManager.GetAvailableApis();
            foreach (var api in apis)
                Console.WriteLine($"API: {api}");

            Console.WriteLine("Available MIDI devices:");
            var inputDevices = RtMidiDeviceManager.Instance.InputDevices.ToList();
            foreach (var device in inputDevices)
                Console.WriteLine($"Input Device: {device.Name}:{device.Port}");

            var outputDevices = RtMidiDeviceManager.Instance.OutputDevices.ToList();
            foreach (var device in outputDevices)
                Console.WriteLine($"Output Device: {device.Name}:{device.Port}");

            _inputDevice = inputDevices.First().CreateDevice();

            try {
                _inputDevice.Message += InputDevice_Message;
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
