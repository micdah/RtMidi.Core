namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Some master device that controls sequence playback sends this timing message to keep a slave device in sync
    /// with the master. A MIDI Clock message is sent at regular intervals (based upon the master's Tempo) in order to
    /// accomplish this.
    ///
    /// There are 24 MIDI Clocks in every quarter note.
    /// </summary>
    public readonly struct ClockMessage: IMessage
    {
        public ClockMessage(double timestamp)
        {
            Timestamp = timestamp;
        }

        /// <summary>
        /// The timestamp when this message was received
        /// </summary>
        public double Timestamp { get; }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}";
        }
    }
}