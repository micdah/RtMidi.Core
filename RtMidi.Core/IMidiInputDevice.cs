using System.Runtime.CompilerServices;
using System;
using RtMidi.Core.Messages;

[assembly: InternalsVisibleTo("RtMidi.Core.Tests")]

namespace RtMidi.Core
{
    public interface IMidiInputDevice : IMidiDevice
    {
        /// <summary>
        /// Note Off event.
        /// </summary>
        event EventHandler<NoteOffMessage> NoteOff;

        /// <summary>
        /// Note On event.
        /// </summary>
        event EventHandler<NoteOnMessage> NoteOn;

        /// <summary>
        /// Polyphonic Key Pressure (Aftertouch).
        /// </summary>
        event EventHandler<PolyphonicKeyPressureMessage> PolyphonicKeyPressure;

        /// <summary>
        /// Control Change.
        /// </summary>
        event EventHandler<ControlChangeMessage> ControlChange;
    }
}
