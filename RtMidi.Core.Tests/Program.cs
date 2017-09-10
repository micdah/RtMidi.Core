using System;
using System.Xml.Linq;
using System.Linq;

namespace RtMidi.Core.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Available MIDI API's:");
            var apis = RtMidiDevice.GetAvailableApis();
            foreach (var api in apis)
                Console.WriteLine($"API: {api}");

            Console.WriteLine("Available MIDI devices:");
            foreach (var device in MidiDeviceManager.AllDevices) 
            {
                Console.WriteLine($"Device: {device.Name}:{device.Port}");
            }

            var inputDeviceInfo = MidiDeviceManager.AllDevices.Where(x => x.IsInput).First();
            var inputDevice = MidiDeviceManager.OpenInput(inputDeviceInfo.ID);
            inputDevice.SetCallback(HandleRtMidiCallback, IntPtr.Zero);

            Console.ReadLine();

            inputDevice.Close();
        }

        static void HandleRtMidiCallback(double timestamp, string message, IntPtr userData)
        {
            Console.WriteLine($"Input:{message} ({timestamp})");
        }
    }
}
