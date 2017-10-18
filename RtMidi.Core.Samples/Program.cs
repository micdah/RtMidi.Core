using System;
using System.Linq;
using RtMidi.Core.Devices;
using RtMidi.Core.Messages;

namespace RtMidi.Core.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
        }

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

            var inputDeviceInfo = inputDevices.First();
            var inputDevice = inputDeviceInfo.CreateDevice();
            var outputDevice = outputDevices.FirstOrDefault(x => x.Name == inputDeviceInfo.Name)?.CreateDevice();

            try {
                inputDevice.ControlChange += (_, msg) => Console.WriteLine($"Received Control Change: {msg}");
                inputDevice.Nrpn += (_, msg) =>
                {
                    Console.WriteLine($"Received NRPN: {msg}");
                    outputDevice?.Send(new NrpnMessage(msg.Channel, msg.Parameter + 1, msg.Value));
                };
                inputDevice.Open();
                outputDevice?.Open();

                Console.ReadLine();
            }
            finally
            {
                inputDevice.Dispose();
                outputDevice?.Dispose();
            }
        }
    }
}
