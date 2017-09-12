using System.Collections.Generic;
using System;

namespace RtMidi.Core.Unmanaged.Devices
{
    public class RtMidiDeviceManager : IDisposable
    {
        public static readonly RtMidiDeviceManager Instance = new RtMidiDeviceManager();

        private readonly RtMidiOutputDevice DefaultOutput = new RtMidiOutputDevice();
        private readonly RtMidiInputDevice DefaultInput = new RtMidiInputDevice();
        private bool _disposed;

        private RtMidiDeviceManager() 
        {
        }

        public IEnumerable<RtMidiDeviceInfo> AllDevices
        {
            get
            {
                for (var port = 0; port < DefaultInput.PortCount; port++)
                    yield return new RtMidiDeviceInfo(DefaultInput.GetPortName(port), port, true);

                for (var port = 0; port < DefaultOutput.PortCount; port++)
                    yield return new RtMidiDeviceInfo(DefaultOutput.GetPortName(port), port, true);
            }
        }

        public RtMidiInputDevice OpenInput(RtMidiDeviceInfo deviceInfo)
        {
            var dev = new RtMidiInputDevice();
            dev.OpenPort(deviceInfo.Port, deviceInfo.Name);
            return dev;
        }

        public RtMidiOutputDevice OpenOutput(RtMidiDeviceInfo deviceInfo)
        {
            var dev = new RtMidiOutputDevice();
            dev.OpenPort(deviceInfo.Port, deviceInfo.Name);
            return dev;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                DefaultInput.Dispose();
                DefaultOutput.Dispose();

                _disposed = true;
            }
        }

        ~RtMidiDeviceManager() {
          Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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