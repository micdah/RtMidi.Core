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

        /// <summary>
        /// Get <see cref="TEnum"/> enumeration value for <paramref name="value"/> if it is defined
        /// (<see cref="Enum.IsDefined"/>) or <paramref name="defaultValue"/> if not.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum</typeparam>
        /// <param name="defaultValue">Default value if not defined</param>
        /// <param name="value">Value of enum to return, if defined</param>
        /// <returns>Value of enum or default value if not defined</returns>
        public static TEnum OrValueIfDefined<TEnum>(this TEnum defaultValue, object value)
            where TEnum : struct
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum) value;
            }
            return defaultValue;
        }
    }
}