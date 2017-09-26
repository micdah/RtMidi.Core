using RtMidi.Core.Enums;
namespace RtMidi.Core.Messages
{
    /// <summary>
    /// This message is most often sent by pressing down on the key after it "bottoms out".
    /// </summary>
    public struct PolyphonicKeyPressureMessage
    {
        public PolyphonicKeyPressureMessage(Channel channel, Key key, int pressure)
        {
            Channel = channel;
            Key = key;
            Pressure = pressure;
        }

        public Channel Channel { get; }
        public Key Key { get; }
        public int Pressure { get; }
    }
}