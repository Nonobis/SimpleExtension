﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension
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
        public static string ToBase64String(this byte[] pArray)
        {
            return Convert.ToBase64String(pArray);
        }

        /// <summary>
        /// Converts to image.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>Image.</returns>
        public static Image ByteArrayToImage(this byte[] pBytes)
        {
            var ms = new MemoryStream(pBytes);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>System.String.</returns>
        public static string BytesToString(this byte[] pBytes)
        {
            var chars = new char[pBytes.Length / sizeof(char)];
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
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(pBytes);

                var s = new StringBuilder();
                foreach (var b in hash)
                    s.Append(b.ToString("x2").ToLower());
                return s.ToString();
            }
        }

        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns>System.String.</returns>
        public static string GetSha256Hash(this byte[] pBytes)
        {
            using (var sha = SHA256.Create())
            {
                var checksum = sha.ComputeHash(pBytes);
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
        }

    }
}
