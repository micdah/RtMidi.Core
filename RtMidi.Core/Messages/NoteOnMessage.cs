using RtMidi.Core.Enums;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is sent when a note is depressed (start). 
    /// </summary>
    public struct NoteOnMessage
    {
        public NoteOnMessage(Channel channel, Key key, int velocity)
        {
            Channel = channel;
            Key = key;
            Velocity = velocity;
        }

        public Channel Channel { get; }
        public Key Key { get; }
        public int Velocity { get; }
    }
}