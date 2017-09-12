namespace RtMidi.Core.Unmanaged.API
{
    public enum RtMidiApi
    {
        /// <summary>
        /// Search for a working compiled API
        /// </summary>
        RT_MIDI_API_UNSPECIFIED,

        /// <summary>
        /// Macintosh OS-X Core Midi API.
        /// </summary>
        RT_MIDI_API_MACOSX_CORE,

        /// <summary>
        /// The Advanced Linux Sound Architecture API.
        /// </summary>
        RT_MIDI_API_LINUX_ALSA,

        /// <summary>
        /// The Jack Low-Latency MIDI Server API.
        /// </summary>
        RT_MIDI_API_UNIX_JACK,

        /// <summary>
        /// The Microsoft Multimedia MIDI API.
        /// </summary>
        RT_MIDI_API_WINDOWS_MM,

        /// <summary>
        /// The Microsoft Kernel Streaming MIDI API.
        /// </summary>
        RT_MIDI_API_WINDOWS_KS,

        /// <summary>
        /// A compilable but non-functional API.
        /// </summary>
        RT_MIDI_API_RTMIDI_DUMMY
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