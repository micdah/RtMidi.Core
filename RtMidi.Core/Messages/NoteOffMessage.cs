using RtMidi.Core.Enums;
namespace RtMidi.Core.Messages
{
    public struct NoteOffMessage
    {
        public NoteOffMessage(Channel channel, Key key, int velocity) 
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