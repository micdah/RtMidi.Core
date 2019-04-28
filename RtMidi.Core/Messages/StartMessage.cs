namespace RtMidi.Core.Messages
{
    /// <summary>
    /// Some master device that controls sequence playback sends this message to make a slave device start playback of
    /// some song/sequence from the beginning (ie, MIDI Beat 0).
    /// </summary>
    public readonly struct StartMessage: IMessage
    {
        public StartMessage(double timestamp)
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