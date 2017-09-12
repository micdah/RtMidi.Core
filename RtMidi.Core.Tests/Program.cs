using System;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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

        static void HandleRtMidiCallback(double timestamp, IntPtr messagePtr, UIntPtr messageSize, IntPtr userData)
        {
            try
            {
                var size = (int)messageSize;
                var message = new byte[size];
                Marshal.Copy(messagePtr, message, 0, size);

                var msg = string.Join(" ", message.Select(b => $"{b:X2}/{b}"));

                Console.WriteLine($"Received: {msg} (length {messageSize})");
            }
            catch(Exception e) 
            {
                Console.WriteLine($"Exception receiving message: {e}");
            }
        }
    }
}
