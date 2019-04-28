namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Some master device that controls sequence playback sends this message to make a slave device stop playback of a
    /// song/sequence.
    /// </summary>
    public readonly struct StopMessage: IMessage
    {
        public StopMessage(double timestamp)
        {
            Timestamp = timestamp;
        }

        /// <summary>
        /// SysEx Data Array
        /// </summary>
        public double Timestamp { get; }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}";
        }
    }
}