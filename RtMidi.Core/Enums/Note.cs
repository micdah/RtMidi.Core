using System;
namespace RtMidi.Core.Enums
{
    public enum Note
    {
        Note_0, Note_1, Note_2, Note_3, Note_4, Note_5, Note_6, Note_7, Note_8, Note_9, Note_10, Note_11,
        Note_12, Note_13, Note_14, Note_15, Note_16, Note_17, Note_18, Note_19, Note_20, Note_21, Note_22, Note_23, 
        Note_24, Note_25, Note_26, Note_27, Note_28, Note_29, Note_30, Note_31, Note_32, Note_33, Note_34, Note_35,
        Note_36, Note_37, Note_38, Note_39, Note_40, Note_41, Note_42, Note_43, Note_44, Note_45, Note_46, Note_47,
        Note_48, Note_49, Note_50, Note_51, Note_52, Note_53, Note_54, Note_55, Note_56, Note_57, Note_58, Note_59,
        Note_60, Note_61, Note_62, Note_63, Note_64, Note_65, Note_66, Note_67, Note_68, Note_69, Note_70, Note_71,
        Note_72, Note_73, Note_74, Note_75, Note_76, Note_77, Note_78, Note_79, Note_80, Note_81, Note_82, Note_83, 
        Note_84, Note_85, Note_86, Note_87, Note_88, Note_89, Note_90, Note_91, Note_92, Note_93, Note_94, Note_95, 
        Note_96, Note_97, Note_98, Note_99, Note_100, Note_101, Note_102, Note_103, Note_104, Note_105, Note_106, Note_107,
        Note_108, Note_109, Note_110, Note_111, Note_112, Note_113, Note_114, Note_115, Note_116, Note_117, Note_118, Note_119,
        Note_120, Note_121, Note_122, Note_123, Note_124, Note_125, Note_126, Note_127
    }

    public static class NoteExtensions
    {
        /// <summary>
        /// Get note octave (0-11)
        /// </summary>
        /// <returns>Octave</returns>
        public static int Octave(this Note note)
        {
            var noteValue = (int)note;
            if (noteValue < 0 || noteValue > 127)
                throw new ArgumentOutOfRangeException(nameof(note));

            // 12 notes per octave
            return noteValue / 12;
        }
    }
}
