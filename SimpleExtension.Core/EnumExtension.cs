using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SimpleExtension.Core
{
    public static class EnumExtension
    {
        /// <summary>
        ///    Return Attribut 'Description' of an Enumeration
        /// </summary>
        public static string ToDescription(this Enum pEnumeration)
        {
            var da =
                (DescriptionAttribute[])
                pEnumeration.GetType().GetField(pEnumeration.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : pEnumeration.ToString();
        }

        /// <summary>
        ///   Return a list of item in Enumeration
        /// </summary>
        public static List<Enum> ToList(this Enum pEnumeration)
        {
            return
                pEnumeration.GetType()
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fieldInfo => (Enum) fieldInfo.GetValue(pEnumeration))
                    .ToList();
        }
    }
}