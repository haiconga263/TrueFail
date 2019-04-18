using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class EnumHelper
    {
        public static string convertToString<T>(this T eff) where T : struct
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static T converToEnum<T>(this String enumValue) where T : struct
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }
    }
}
