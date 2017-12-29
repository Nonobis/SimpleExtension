using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleExtension.Core
{
    public static class ObjectExtension
    {
        /// <summary>
        ///     Returns a string of all the field value pairs of a given object. Works only on non-statics.
        /// </summary>
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
        /// Deserializes the XML.
        /// </summary>
        public static T DeserializeXmlToObject<T>(string pXmlData)
          where T : new()
        {
            if (string.IsNullOrEmpty(pXmlData))
                return default(T);

            TextReader tr = new StringReader(pXmlData);
            var docItms = new T();
            var xms = new XmlSerializer(docItms.GetType());
            docItms = (T)xms.Deserialize(tr);

            return docItms == null ? default(T) : docItms;
        }

        /// <summary>
        /// Seralizes the object to XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject">The XML object.</param>
        /// <returns></returns>
        public static string SeralizeObjectToXml<T>(T pObject)
        {
            if (pObject == null)
                return string.Empty;

            var sbTr = new StringBuilder();
            var xmsTr = new XmlSerializer(pObject.GetType());
            var xwsTr = new XmlWriterSettings();
            var xmwTr = XmlWriter.Create(sbTr, xwsTr);
            xmsTr.Serialize(xmwTr, pObject);
            return sbTr.ToString();
        }

        /// <summary>
        /// Deeps the clone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject">The object clone.</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T pObject) where T : new()
        {
            var getString = SeralizeObjectToXml<T>(pObject);
            return DeserializeXmlToObject<T>(getString);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
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