using System.IO;
using System.Text;

namespace SimpleExtension
{
    /// <summary>
    /// Class MemoryStreamExtension.
    /// </summary>
    public static class MemoryStreamExtension
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pEncoding">The p encoding.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(this MemoryStream pMemoryStream, Encoding pEncoding)
        {
            return pEncoding.GetString(pMemoryStream.ToArray());
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(this MemoryStream pMemoryStream)
        {
            return ToString(pMemoryStream, Encoding.Default);
        }

        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pInput">The p input.</param>
        /// <param name="pEncoding">The p encoding.</param>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput, Encoding pEncoding)
        {
            var buffer = pEncoding.GetBytes(pInput);
            pMemoryStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="pMemoryStream">The p memory stream.</param>
        /// <param name="pInput">The p input.</param>
        public static void WriteString(this MemoryStream pMemoryStream, string pInput)
        {
            WriteString(pMemoryStream, pInput, Encoding.Default);
        }
    }
}