using System.IO;
using System.Text;

namespace SimpleExtension
{
    public static class MemoryStreamExtension
    {
        /// <summary>
        /// Returns the content of the stream as a string
        /// </summary>
        /// <param name="ms">
        /// The ms.
        /// </param>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string GetAsString(this MemoryStream ms, Encoding encoding)
        {
            return encoding.GetString(ms.ToArray());
        }

        /// <summary>
        /// Returns the content of the stream as a string
        /// </summary>
        /// <param name="ms">
        /// The ms.
        /// </param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string GetAsString(this MemoryStream ms)
        {
            return GetAsString(ms, Encoding.Default);
        }

        /// <summary>
        /// Writes the specified string into the memory stream
        /// </summary>
        /// <param name="ms">
        /// The ms.
        /// </param>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        public static void WriteString(this MemoryStream ms, string inputString, Encoding encoding)
        {
            var buffer = encoding.GetBytes(inputString);
            ms.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Writes the specified string into the memory stream
        /// </summary>
        /// <param name="ms">
        /// The ms.
        /// </param>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        public static void WriteString(this MemoryStream ms, string inputString)
        {
            WriteString(ms, inputString, Encoding.Default);
        }
    }
}
