using System;
namespace RtMidi.Core
{
    public class MidiDeviceInfo
    {
        readonly RtMidiDevice manager;
        readonly int id;
        readonly int port;
        readonly bool is_input;

        internal MidiDeviceInfo(RtMidiDevice manager, int id, int port, bool isInput)
        {
            this.manager = manager;
            this.id = id;
            this.port = port;
            is_input = isInput;
        }

        public int ID
        {
            get { return id; }
        }

        public int Port
        {
            get { return port; }
        }

        public string Interface
        {
            get { return manager.CurrentApi.ToString(); }
        }

        public string Name
        {
            get { return manager.GetPortName(port); }
        }

        public bool IsInput { get { return is_input; } }

        public bool IsOutput { get { return !is_input; } }

        public override string ToString()
        {
            return String.Format("{0} - {1} ({2})", Interface, Name, IsInput ? (IsOutput ? "I/O" : "Input") : (IsOutput ? "Output" : "N/A"));
        }
    }
}
