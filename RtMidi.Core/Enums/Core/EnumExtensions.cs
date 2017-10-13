using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RtMidi.Core.Enums.Core
{
    internal static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<(Type EnumType, string EnumName), EnumDisplayNameAttribute> Cache =
            new ConcurrentDictionary<(Type EnumType, string EnumName), EnumDisplayNameAttribute>();

        public static EnumDisplayNameAttribute GetDisplayNameAttribute<TEnum>(TEnum enumValue)
            where TEnum : struct
        {
            var enumType = typeof(TEnum);
            var enumName = Enum.GetName(enumType, enumValue);
            if (enumName == null)
                return null;

            return Cache.GetOrAdd((enumType, enumName),
                key => key.EnumType
                    .GetField(key.EnumName)
                    .GetCustomAttributes(false)
                    .OfType<EnumDisplayNameAttribute>()
                    .Single());
        }
    }
}