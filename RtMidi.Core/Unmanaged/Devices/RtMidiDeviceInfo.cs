using System;

namespace RtMidi.Core.Unmanaged.Devices
{
    public class RtMidiDeviceInfo
    {
        readonly RtMidiDevice manager;
        readonly int id;
        readonly int port;
        readonly bool is_input;

        internal RtMidiDeviceInfo(RtMidiDevice manager, int id, int port, bool isInput)
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

/**
 * This is a derived work, based on https://github.com/atsushieno/managed-midi
 * 
 * Copyright (c) 2010 Atsushi Eno
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 **/