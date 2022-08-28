using System.IO;
using System.Text;

namespace SimpleExtension.Core
{
    public static class MemoryStreamExtension
    {
        /// <summary>
        ///     Returns the content of the stream as a string
        /// </summary>
        public static string ToString(this MemoryStream pMemoryStream, Encoding pEncoding)
        {
            return pEncoding.GetString(pMemoryStream.ToArray());
        }

        /// <summary>
        ///     Returns the content of the stream as a string
        /// </summary>
        public static string ToString(this MemoryStream pMemoryStream)
        {
            return ToString(pMemoryStream, Encoding.GetEncoding(0));
        }

        /// <summary>
        ///     Writes the specified string into the memory stream
        /// </summary>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput, Encoding pEncoding)
        {
            var buffer = pEncoding.GetBytes(pInput);
            pMemoryStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Writes the specified string into the memory stream
        /// </summary>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput)
        {
            WriteString(pMemoryStream, pInput, Encoding.GetEncoding(0));
        }
    }
}