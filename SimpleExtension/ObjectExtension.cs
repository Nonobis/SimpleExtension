﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleExtension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="pObject">The p object.</param>
        /// <param name="pSeparator">The p separator.</param>
        /// <param name="pType">Type of the p.</param>
        /// <returns>System.String.</returns>
        public static string ObjectToString(this object pObject, string pSeparator, ObjectToStringTypes pType)
        {
            var fi = pObject.GetType().GetFields();
            var output = string.Empty;

            if ((pType == ObjectToStringTypes.Properties) || (pType == ObjectToStringTypes.PropertiesAndFields))
                foreach (var property in pObject.GetType().GetProperties())
                    try
                    {
                        output += $"{property.Name}:{property.GetValue(pObject, null)}{pSeparator}";
                    }
                    catch
                    {
                        output += $"{property.Name}: n/a{pSeparator}";
                    }

            if ((pType == ObjectToStringTypes.Fields) || (pType == ObjectToStringTypes.PropertiesAndFields))
                foreach (var field in fi)
                    try
                    {
                        output = $"{output}{field.Name}: {field.GetValue(pObject)}{pSeparator}";
                    }
                    catch
                    {
                        output = $"{output}{field.Name}: n/a{pSeparator}";
                    }
            return output;
        }

        /// <summary>
        /// Converts to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pXmlData">The p XML data.</param>
        /// <returns>T.</returns>
        public static T DeserializeXMLToObject<T>(string pXmlData)
          where T : new()
        {
            if (string.IsNullOrEmpty(pXmlData))
                return default(T);

            TextReader tr = new StringReader(pXmlData);
            var DocItms = new T();
            var xms = new XmlSerializer(DocItms.GetType());
            DocItms = (T)xms.Deserialize(tr);

            return DocItms == null ? default(T) : DocItms;
        }

        /// <summary>
        /// Converts to xml.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject">The p object.</param>
        /// <returns>System.String.</returns>
        public static string SeralizeObjectToXML<T>(T pObject)
        {
            if (pObject == null)
                return string.Empty;

            var sbTR = new StringBuilder();
            var xmsTR = new XmlSerializer(pObject.GetType());
            var xwsTR = new XmlWriterSettings();
            var xmwTR = XmlWriter.Create(sbTR, xwsTR);
            xmsTR.Serialize(xmwTR, pObject);
            return sbTR.ToString();
        }

        /// <summary>
        /// Deeps the clone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject">The p object.</param>
        /// <returns>T.</returns>
        public static T DeepClone<T>(this T pObject) where T : new()
        {
            var GetString = SeralizeObjectToXML<T>(pObject);
            return DeserializeXMLToObject<T>(GetString);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString<T>(this T instance) where T : class
        {
            if (instance == null)
                return string.Empty;

            var strListType = typeof(List<string>);
            var strArrType = typeof(string[]);

            var arrayTypes = new[] {strListType, strArrType};
            var handledTypes = new[]
            {
                typeof(int), typeof(string), typeof(bool), typeof(DateTime), typeof(double), typeof(decimal),
                strListType, strArrType
            };

            var validProperties = instance.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(prop => handledTypes.Contains(prop.PropertyType))
                .Where(prop => prop.GetValue(instance, null) != null)
                .ToList();

            var format = $"{{0,-{validProperties.Max(prp => prp.Name.Length)}}} : {{1}}";

            return string.Join(
                Environment.NewLine,
                validProperties.Select(prop => string.Format(format,
                    prop.Name,
                    arrayTypes.Contains(prop.PropertyType)
                        ? string.Join(", ", prop.GetValue(instance, null) as string[])
                        : prop.GetValue(instance, null))) as string[]);
        }
    }
}