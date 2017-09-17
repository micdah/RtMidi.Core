using System;
namespace RtMidi.Core.Enums
{
    public enum Key
    {
        Key_0, Key_1, Key_2, Key_3, Key_4, Key_5, Key_6, Key_7, Key_8, Key_9, Key_10, Key_11,
        Key_12, Key_13, Key_14, Key_15, Key_16, Key_17, Key_18, Key_19, Key_20, Key_21, Key_22, Key_23, 
        Key_24, Key_25, Key_26, Key_27, Key_28, Key_29, Key_30, Key_31, Key_32, Key_33, Key_34, Key_35,
        Key_36, Key_37, Key_38, Key_39, Key_40, Key_41, Key_42, Key_43, Key_44, Key_45, Key_46, Key_47,
        Key_48, Key_49, Key_50, Key_51, Key_52, Key_53, Key_54, Key_55, Key_56, Key_57, Key_58, Key_59,
        Key_60, Key_61, Key_62, Key_63, Key_64, Key_65, Key_66, Key_67, Key_68, Key_69, Key_70, Key_71,
        Key_72, Key_73, Key_74, Key_75, Key_76, Key_77, Key_78, Key_79, Key_80, Key_81, Key_82, Key_83, 
        Key_84, Key_85, Key_86, Key_87, Key_88, Key_89, Key_90, Key_91, Key_92, Key_93, Key_94, Key_95, 
        Key_96, Key_97, Key_98, Key_99, Key_100, Key_101, Key_102, Key_103, Key_104, Key_105, Key_106, Key_107,
        Key_108, Key_109, Key_110, Key_111, Key_112, Key_113, Key_114, Key_115, Key_116, Key_117, Key_118, Key_119,
        Key_120, Key_121, Key_122, Key_123, Key_124, Key_125, Key_126, Key_127
    }

    public static class KeyExtensions
    {
        /// <summary>
        /// Get key octave (0-11)
        /// </summary>
        /// <returns>Octave</returns>
        public static int Octave(this Key key)
        {
            var keyValue = (int)key;
            if (keyValue < 0 || keyValue > 127)
                throw new ArgumentOutOfRangeException(nameof(key));

            // 12 notes per octave
            return keyValue / 12;
        }

        /// <summary>
        /// Get key note (C-B)
        /// </summary>
        /// <returns>Note</returns>
        public static Note Note(this Key key)
        {
            var keyValue = (int)key;
            if (keyValue < 0 || keyValue > 127)
                throw new ArgumentOutOfRangeException(nameof(key));

            return (Note)(keyValue % 12);
        }
    }
}
