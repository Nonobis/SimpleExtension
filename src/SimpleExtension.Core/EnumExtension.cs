using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Enumeration Extensions Methods
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get the Description from the DescriptionAttribute.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())[0]
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentException">T must be of type '{typeof(Enum)}'</exception>
        public static IEnumerable<T> GetEnumValues<T>()
        {
            // Can't use type constraints on value types, so have to do check like this
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new ArgumentException($"T must be of type '{typeof(Enum)}'");
            }

            return Enum
                .GetValues(typeof(T))?
                .Cast<T>();
        }

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumMemberText">The enum member text.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException">Could not resolve value {enumMemberText} in enum {typeof(T).FullName}</exception>
        public static T GetEnumValue<T>(this string enumMemberText) where T : struct, Enum
        {
            if (Enum.TryParse(enumMemberText, out T retVal))
            {
                return retVal;
            }

            IEnumerable<T> enumVals = GetEnumValues<T>();
            Dictionary<string, T> enumMemberNameMappings = new();
            foreach (T enumVal in enumVals)
            {
                string enumMember = enumVal.GetDescription();
                enumMemberNameMappings.Add(enumMember, enumVal);
            }

            return enumMemberNameMappings.ContainsKey(enumMemberText)
                ? enumMemberNameMappings[enumMemberText]
                : throw new SerializationException($"Could not resolve value {enumMemberText} in enum {typeof(T).FullName}");
        }

        /// <summary>
        /// Converts to list.
        /// </summary>
        /// <param name="pEnumeration">The p enumeration.</param>
        /// <returns>List&lt;Enum&gt;.</returns>
        public static List<Enum> ToList(this Enum pEnumeration) => pEnumeration.GetType()
                    .GetTypeInfo().GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fieldInfo => (Enum)fieldInfo.GetValue(pEnumeration))
                    .ToList();
    }
}