using System;

namespace RtMidi.Core.Unmanaged.Devices
{
    public class RtMidiReceivedEventArgs: EventArgs
    {
        public RtMidiReceivedEventArgs(double timestamp, byte[] data)
        {
            Data = data;
            Timestamp = timestamp;
        }
        
        
        public byte[] Data { get; }
        public double Timestamp { get; }
    }
    
}