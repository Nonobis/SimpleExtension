using System.IO;
using System.Text;

namespace SimpleExtension.Core
{
    public static class MemoryStreamExtension
    {
        /// <summary>
        /// Returns the content of the stream as a string
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pEncoding">The p encoding.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this MemoryStream pMemoryStream, Encoding pEncoding) => pEncoding.GetString(pMemoryStream.ToArray());

        /// <summary>
        /// Returns the content of the stream as a string
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this MemoryStream pMemoryStream) => ToString(pMemoryStream, Encoding.GetEncoding(0));

        /// <summary>
        /// Writes the specified string into the memory stream
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pInput">The p input.</param>
        /// <param name="pEncoding">The p encoding.</param>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput, Encoding pEncoding)
        {
            byte[] buffer = pEncoding.GetBytes(pInput);
            pMemoryStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Writes the specified string into the memory stream
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pInput">The p input.</param>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput) => WriteString(pMemoryStream, pInput, Encoding.GetEncoding(0));
    }
}