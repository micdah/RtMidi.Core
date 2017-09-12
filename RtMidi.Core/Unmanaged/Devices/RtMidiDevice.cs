using System;
using System.Runtime.InteropServices;
using RtMidiPtr = System.IntPtr;
using RtMidi.Core.Unmanaged.API;

namespace RtMidi.Core.Unmanaged.Devices
{
    /// <summary>
    /// Abstract RtMidi device
    /// </summary>
    public abstract class RtMidiDevice : IDisposable
    {
        // no idea when to use it...
        public static void Error(RtMidiErrorType errorType, string message)
        {
            RtMidiC.Error(errorType, message);
        }

        RtMidiPtr handle;
        bool is_port_open;

        protected RtMidiDevice(RtMidiPtr handle)
        {
            this.handle = handle;
        }

        public RtMidiPtr Handle
        {
            get { return handle; }
        }

        public int PortCount
        {
            get { return (int)RtMidiC.GetPortCount(handle); }
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (is_port_open)
            {
                is_port_open = false;
                RtMidiC.ClosePort(handle);
            }
            ReleaseDevice();
        }

        public string GetPortName(int portNumber)
        {
            return RtMidiC.GetPortName(handle, (uint)portNumber);
        }

        public void OpenVirtualPort(string portName)
        {
            try
            {
                RtMidiC.OpenVirtualPort(handle, portName);
            }
            finally
            {
                is_port_open = true;
            }
        }

        public void OpenPort(int portNumber, string portName)
        {
            try
            {
                RtMidiC.OpenPort(handle, (uint)portNumber, portName);
            }
            finally
            {
                is_port_open = true;
            }
        }

        protected abstract void ReleaseDevice();

        public abstract RtMidiApi CurrentApi { get; }
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