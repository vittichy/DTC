using System;
using System.ComponentModel;

namespace Dtc.Common.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// vraci hodnotu z Description attributu nad hodnotou enumu
        /// </summary>
        /// <returns>hodnota z decription attributu</returns>
        public static string ToDescriptionString(this Enum val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
