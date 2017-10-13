using RtMidi.Core.Enums.Core;
using RtMidi.Core.Messages;

namespace RtMidi.Core.Enums
{
    /// <summary>
    /// Control function for <see cref="ControlChangeMessage.Control"/> as 
    /// defined by MIDI specification:
    /// https://www.midi.org/specifications/item/table-3-control-change-messages-data-bytes-2
    /// </summary>
    public enum Control
    {
        [EnumDisplayName("Undefined")]
        Undefined = -1,

        [EnumDisplayName("Bank Select")]
        BankSelect = 0,

        [EnumDisplayName("ModulationWheelOrLever")]
        ModulationWheelOrLever = 1,

        [EnumDisplayName("Breath Controller")]
        BreathController = 2,

        [EnumDisplayName("Foot Controller")]
        FootController = 4,

        [EnumDisplayName("Portamento Time")]
        PortamentoTime = 5,

        [EnumDisplayName("Data Entry MSB")]
        DataEntryMSB = 6,

        [EnumDisplayName("Channel Volume")]
        ChannelVolume = 7,

        [EnumDisplayName("Balance")]
        Balance = 8,

        [EnumDisplayName("Pan")]
        Pan = 10,

        [EnumDisplayName("Expression Controller")]
        ExpressionController = 11,

        [EnumDisplayName("Effect Control 1")]
        EffectControl1 = 12,

        [EnumDisplayName("Effect Control 2")]
        EffectControl2 = 13,

        [EnumDisplayName("General Purpose Controller 1")]
        GeneralPurposeController1 = 16,

        [EnumDisplayName("General Purpose Controller 2")]
        GeneralPurposeController2 = 17,

        [EnumDisplayName("General Purpose Controller 3")]
        GeneralPurposeController3 = 18,

        [EnumDisplayName("General Purpose Controller 4")]
        GeneralPurposeController4 = 19,

        [EnumDisplayName("LSB for Control 0 (Bank Select)")]
        LSBForControl0BankSelect = 32,

        [EnumDisplayName("LSB for Control 1 (Modulation Wheel or Lever")]
        LSBForControl1 = 33,

        [EnumDisplayName("LSB for Control 2 (Breath Controller)")]
        LSBForControl2 = 34,

        [EnumDisplayName("LSB for Control 3 (Undefined)")]
        LSBForControl3 = 35,

        [EnumDisplayName("LSB for Control 4 (Foot Controller)")]
        LSBForControl4 = 36,

        [EnumDisplayName("LSB for Control 5 (Portamento Time)")]
        LSBForControl5 = 37,

        [EnumDisplayName("LSB for Control 6 (Data Entry)")]
        LSBForControl6 = 38,

        [EnumDisplayName("LSB for Control 7 (Channel Volume)")]
        LSBForControl7 = 39,

        [EnumDisplayName("LSB for Control 8 (Balance)")]
        LSBForControl8 = 40,

        [EnumDisplayName("LSB for Control 9 (Undefined)")]
        LSBForControl9 = 41,

        [EnumDisplayName("LSB for Control 10 (Pan)")]
        LSBForControl10 = 42,

        [EnumDisplayName("LSB for Contorl 11 (Expression Controller)")]
        LSBForControl11 = 43,

        [EnumDisplayName("LSB for Control 12 (Effect Control 1)")]
        LSBForControl12 = 44,

        [EnumDisplayName("LSB for Control 13 (Effect Control 2)")]
        LSBForControl13 = 45,

        [EnumDisplayName("LSB for Control 14 (Undefined)")]
        LSBForControl14 = 46,

        [EnumDisplayName("LSB for Control 15 (Undefined)")]
        LSBForControl15 = 47,

        [EnumDisplayName("LSB for Control 16 (General Purpose Controller 1)")]
        LSBForControl16 = 48,

        [EnumDisplayName("LSB for Control 17 (General Purpose Controller 2)")]
        LSBForControl17 = 49,

        [EnumDisplayName("LSB for Control 18 (General Purpose Controller 3)")]
        LSBForControl18 = 50,

        [EnumDisplayName("LSB for Control 19 (General Purpose Controller 4)")]
        LSBForControl19 = 51,

        [EnumDisplayName("LSB for Control 20 (Undefined)")]
        LSBForControl20 = 52,

        [EnumDisplayName("LSB for Control 21 (Undefined)")]
        LSBForControl21 = 53,

        [EnumDisplayName("LSB for Control 22 (Undefined)")]
        LSBForControl22 = 54,

        [EnumDisplayName("LSB for Control 23 (Undefined)")]
        LSBForControl23 = 55,

        [EnumDisplayName("LSB for Control 24 (Undefined)")]
        LSBForControl24 = 56,
       
        [EnumDisplayName("LSB for Control 25 (Undefined)")]
        LSBForControl25 = 57,

        [EnumDisplayName("LSB for Control 26 (Undefined)")]
        LSBForControl26 = 58,

        [EnumDisplayName("LSB for Control 27 (Undefined)")]
        LSBForControl27 = 59,

        [EnumDisplayName("LSB for Control 28 (Undefined)")]
        LSBForControl28 = 60,

        [EnumDisplayName("LSB for Control 29 (Undefined)")]
        LSBForControl29 = 61,

        [EnumDisplayName("LSB for Control 30 (Undefined)")]
        LSBForControl30 = 62,

        [EnumDisplayName("LSB for Control 31 (Undefined)")]
        LSBForControl31 = 63,

        [EnumDisplayName("Damper Pedal On/Off")]
        DamperPedalOnOff = 64,

        [EnumDisplayName("Portamento On/Off")]
        PortamentoOnOff = 65,

        [EnumDisplayName("Sostenuto On/Off")]
        SostenutoOnOff = 66,

        [EnumDisplayName("Soft Pedal On/Off")]
        SoftPedalOnOff = 67,

        [EnumDisplayName("Legato Footswitch")]
        LegatoFootswitch = 68,

        [EnumDisplayName("Hold 2")]
        Hold2 = 69,

        [EnumDisplayName("Sound Controller 1 (default: Sound Variation)")]
        SoundController1 = 70,

        [EnumDisplayName("Sound Controller 2 (default: Timbre/Harmonic Intens")]
        SoundController2 = 71,

        [EnumDisplayName("Sound Controller 3 (default: Release Time)")]
        SoundController3 = 72,

        [EnumDisplayName("Sound Controller 4 (default: Attach Time)")]
        SoundController4 = 73,

        [EnumDisplayName("Sound Controller 5 (default: Brightness")]
        SoundController5 = 74,

        [EnumDisplayName("Sound Controller 6 (default: Decay Time)")]
        SoundController6 = 75,

        [EnumDisplayName("Sound Controller 7 (default: Vibrato Rate)")]
        SoundController7 = 76,

        [EnumDisplayName("Sound Controller 8 (default: Vibrato Depth)")]
        SoundController8 = 77,

        [EnumDisplayName("Sound Controller 9 (default: Vibrato Delay)")]
        SoundController9 = 78,

        [EnumDisplayName("Sound Controller 10 (default: Undefined)")]
        SoundController10 = 79,

        [EnumDisplayName("General Purpose Controller 5")]
        GeneralPurposeController5 = 80,

        [EnumDisplayName("General Purpose Controller 6")]
        GeneralPurposeController6 = 81,

        [EnumDisplayName("General Purpose Controller 7")]
        GeneralPurposeController7 = 82,

        [EnumDisplayName("General Purpose Controller 8")]
        GeneralPurposeController8 = 83,
    }

    public static class ControlExtensions 
    {
        /// <summary>
        /// Get human readable display name of Control
        /// </summary>
        public static string DisplayName(this Control channel) => EnumExtensions.GetDisplayNameAttribute(channel)?.Name ?? string.Empty;
    }
}
