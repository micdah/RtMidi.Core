namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Some master device that controls sequence playback sends this message to make a slave device resume playback
    /// from its current "Song Position". The current Song Position is the point when the song/sequence was previously
    /// stopped, or previously cued with a Song Position Pointer message.
    /// </summary>
    public readonly struct ContinueMessage: IMessage
    {
        public ContinueMessage(double timestamp)
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