using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SimpleExtension.Enum;

namespace SimpleExtension
{
    public static class SerializationExtension
    {
        /// <summary>
        /// Deserializes an object from file and returns a reference.
        /// </summary>
        public static object DeSerializeObject(this string fileName, Type objectType, bool binarySerialization)
        {
            return DeSerializeObject(fileName, objectType, binarySerialization, false);
        }

        /// <summary>
        /// Deserializes an object from file and returns a reference.
        /// </summary>
        public static object DeSerializeObject(this string fileName, Type objectType, bool binarySerialization, bool throwExceptions)
        {
            object instance = null;

            if (!binarySerialization)
            {
                XmlReader reader = null;
                FileStream fs = null;
                try
                {
                    // Create an instance of the XmlSerializer specifying type and namespace.
                    var serializer = new XmlSerializer(objectType);

                    // A FileStream is needed to read the XML document.
                    fs = new FileStream(fileName, FileMode.Open);
                    reader = new XmlTextReader(fs);

                    instance = serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    if (throwExceptions)
                    {
                        throw;
                    }

                    var message = ex.Message;
                    return null;
                }
                finally
                {
                    fs?.Close();
                    reader?.Close();
                }
            }
            else
            {
                FileStream fs = null;
                try
                {
                    var serializer = new BinaryFormatter();
                    fs = new FileStream(fileName, FileMode.Open);
                    instance = serializer.Deserialize(fs);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    fs?.Close();
                }
            }

            return instance;
        }

        /// <summary>
        /// Deserialize an object from an XmlReader object.
        /// </summary>
        public static object DeSerializeObject(this XmlReader reader, Type objectType)
        {
            var serializer = new XmlSerializer(objectType);
            var instance = serializer.Deserialize(reader);
            reader.Close();
            return instance;
        }

        /// <summary>
        /// Des the serialize object.
        /// </summary>
        public static object DeSerializeObject(this string xml, Type objectType)
        {
            var reader = new XmlTextReader(xml, XmlNodeType.Document, null);
            return DeSerializeObject(reader, objectType);
        }

        /// <summary>
        /// Des the serialize object.
        /// </summary>
        public static object DeSerializeObject(this byte[] buffer, bool throwExceptions = false)
        {
            MemoryStream ms = null;
            object instance;

            try
            {
                var serializer = new BinaryFormatter();
                ms = new MemoryStream(buffer);
                instance = serializer.Deserialize(ms);
            }
            catch
            {
                if (throwExceptions)
                {
                    throw;
                }
                return null;
            }
            finally
            {
                ms?.Close();
            }
            return instance;
        }

        /// <summary>
        /// Returns a string of all the field value pairs of a given object. Works only on non-statics.
        /// </summary>
        public static string ObjectToString(this object instanc, string separator, ObjectToStringTypes type)
        {
            var fi = instanc.GetType().GetFields();
            var output = string.Empty;

            if (type == ObjectToStringTypes.Properties || type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var property in instanc.GetType().GetProperties())
                {
                    try
                    {
                        output += $"{property.Name}:{property.GetValue(instanc, null)}{separator}";
                    }
                    catch
                    {
                        output += $"{property.Name}: n/a{separator}";
                    }
                }
            }

            if (type == ObjectToStringTypes.Fields || type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var field in fi)
                {
                    try
                    {
                        output = $"{output}{field.Name}: {field.GetValue(instanc)}{separator}";
                    }
                    catch
                    {
                        output = $"{output}{field.Name}: n/a{separator}";
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// Serializes an object instance to a file.
        /// </summary>
        public static bool SerializeObject(this object instance, string fileName, bool binarySerialization)
        {
            var retVal = true;

            if (!binarySerialization)
            {
                XmlTextWriter writer = null;
                try
                {
                    var serializer =
                        new XmlSerializer(instance.GetType());

                    // Create an XmlTextWriter using a FileStream.
                    Stream fs = new FileStream(fileName, FileMode.Create);
                    writer = new XmlTextWriter(fs, new UTF8Encoding())
                    {
                        Formatting = Formatting.Indented,
                        IndentChar = ' ',
                        Indentation = 3
                    };

                    // Serialize using the XmlTextWriter.
                    serializer.Serialize(writer, instance);
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    writer?.Close();
                }
            }
            else
            {
                Stream fs = null;
                try
                {
                    var serializer = new BinaryFormatter();
                    fs = new FileStream(fileName, FileMode.Create);
                    serializer.Serialize(fs, instance);
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    fs?.Close();
                }
            }

            return retVal;
        }

        /// <summary>
        /// Overload that supports passing in an XML TextWriter.
        /// </summary>
        public static bool SerializeObject(this object instance, XmlTextWriter writer, bool throwExceptions)
        {
            var retVal = true;

            try
            {
                var serializer =
                    new XmlSerializer(instance.GetType());

                // Create an XmlTextWriter using a FileStream.
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 3;

                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, instance);
            }
            catch
            {
                if (throwExceptions)
                {
                    throw;
                }

                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// Serializes an object into an XML string variable for easy 'manual' serialization
        /// </summary>
        public static bool SerializeObject(this object instance, out string xmlResultString)
        {
            return SerializeObject(instance, out xmlResultString, false);
        }

        /// <summary>
        /// Serializes an object into a string variable for easy 'manual' serialization
        /// </summary>
        public static bool SerializeObject(this object instance, out string xmlResultString, bool throwExceptions)
        {
            xmlResultString = string.Empty;
            var ms = new MemoryStream();

            var writer = new XmlTextWriter(ms, new UTF8Encoding());

            if (!SerializeObject(instance, writer, throwExceptions))
            {
                ms.Close();
                return false;
            }

            xmlResultString = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);

            ms.Close();
            writer.Close();

            return true;
        }

        /// <summary>
        /// Serializes an object instance to a file.
        /// </summary>
        public static bool SerializeObject(this object instance, out byte[] resultBuffer, bool throwExceptions = false)
        {
            var retVal = true;

            MemoryStream ms = null;
            try
            {
                var serializer = new BinaryFormatter();
                ms = new MemoryStream();
                serializer.Serialize(ms, instance);
            }
            catch
            {
                retVal = false;

                if (throwExceptions)
                {
                    throw;
                }
            }
            finally
            {
                ms?.Close();
            }
            resultBuffer = ms.ToArray();
            return retVal;
        }

        /// <summary>
        /// Serializes the object to byte array.
        /// </summary>
        public static byte[] SerializeObjectToByteArray(this object instance, bool throwExceptions = false)
        {
            byte[] byteResult;
            if (!SerializeObject(instance, out byteResult, throwExceptions))
            {
                return null;
            }
            return byteResult;
        }

        /// <summary>
        /// Serializes an object to an XML string. Unlike the other SerializeObject overloads this
        /// methods *returns a string* rather than a bool result!
        /// </summary>
        public static string SerializeObjectToString(this object instance, bool throwExceptions = false)
        {
            string xmlResultString;
            if (!SerializeObject(instance, out xmlResultString, throwExceptions))
            {
                return null;
            }
            return xmlResultString;
        }
    }
}
