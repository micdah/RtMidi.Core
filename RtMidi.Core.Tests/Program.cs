using System;
using System.Linq;
using RtMidi.Core.Unmanaged;

namespace RtMidi.Core.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Available MIDI API's:");
            var apis = RtMidiApiManager.GetAvailableApis();
            foreach (var api in apis)
                Console.WriteLine($"API: {api}");

            Console.WriteLine("Available MIDI devices:");
            var inputDevices = RtMidiDeviceManager.Instance.InputDevices.ToList();
            foreach (var device in inputDevices) 
            {
                Console.WriteLine($"Device: {device.Name}:{device.Port}");
            }

            using (var inputDevice = inputDevices.First().CreateDevice())
            {
                inputDevice.Message += InputDevice_Message;
                inputDevice.Open();

                Console.ReadLine();
            }
        }

        static void InputDevice_Message(object sender, byte[] message)
        {
            var msg = string.Join(" ", message.Select(b => $"{b:X2}/{b}"));
            Console.WriteLine($"Received: {msg} (length {message.Length})");
        }
    }
}
