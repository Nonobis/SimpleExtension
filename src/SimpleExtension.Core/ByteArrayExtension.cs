using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Class ByteArrayExtension.
    /// </summary>
    public static class ByteArrayExtension
    {
        /// <summary>
        /// Converts to base64string.
        /// </summary>
        /// <param name="pArray">The p array.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64String(this byte[] pArray) => Convert.ToBase64String(pArray);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>System.String.</returns>
        public static string BytesToString(this byte[] pBytes)
        {
            char[] chars = new char[pBytes.Length / sizeof(char)];
            Buffer.BlockCopy(pBytes, 0, chars, 0, pBytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>System.String.</returns>
        public static string GetMd5Hash(this byte[] pBytes)
        {
            using MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(pBytes);

            StringBuilder s = new();
            foreach (byte b in hash)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }

        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>System.String.</returns>
        public static string GetSha256Hash(this byte[] pBytes)
        {
            using SHA256 sha = SHA256.Create();
            byte[] checksum = sha.ComputeHash(pBytes);
            return BitConverter.ToString(checksum).Replace("-", string.Empty);
        }

    }
}
