using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Class EnumExtension.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Converts to description.
        /// </summary>
        /// <param name="pEnumeration">The p enumeration.</param>
        /// <returns>System.String.</returns>
        public static string ToDescription(this Enum pEnumeration)
        {
            // get attributes  
            var field = pEnumeration.GetType().GetField(pEnumeration.ToString());
            var attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute?.Description ?? "";
        }

        /// <summary>
        /// Converts to list.
        /// </summary>
        /// <param name="pEnumeration">The p enumeration.</param>
        /// <returns>List&lt;Enum&gt;.</returns>
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