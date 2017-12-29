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
        ///   Return a list of item in Enumeration
        /// </summary>
        public static List<Enum> ToList(this Enum pEnumeration)
        {
            return
                pEnumeration.GetType()
                    .GetTypeInfo().GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fieldInfo => (Enum) fieldInfo.GetValue(pEnumeration))
                    .ToList();
        }
    }
}