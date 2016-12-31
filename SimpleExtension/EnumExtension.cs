using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SimpleExtension
{ 
    public static class EnumExtension
    {
        /// <summary>
        /// To the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <param name="pEnumeration"></param>
        /// <returns></returns>
        public static List<Enum> ToList(this Enum pEnumeration)
        {
            return pEnumeration.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fieldInfo => (Enum)fieldInfo.GetValue(pEnumeration)).ToList();
        }
    }
}
