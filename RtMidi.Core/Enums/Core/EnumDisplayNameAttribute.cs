using System;

namespace RtMidi.Core.Enums.Core
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class EnumDisplayNameAttribute : Attribute
    {
        public EnumDisplayNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}