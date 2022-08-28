using System;
using System.IO;

namespace SimpleExtension.Core
{
    public static class FileExtension
    {
        /// <summary>
        /// Return Image File as Base 64
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReturnImageFileAsBase64(this string filePath)
        {
            if (File.Exists(filePath))
            {
                var bytes = File.ReadAllBytes(filePath);
                var ext = Path.GetExtension(filePath).Replace(".", "");
                var base64 = Convert.ToBase64String(bytes, 0, bytes.Length);
                return $"data:image/{ext};base64,{base64}";
            }
            return string.Empty;
        }

        /// <summary>
        ///     Files to byte array.
        /// </summary>
        /// <param name="pFilepath">The p filepath.</param>
        /// <returns></returns>
        public static byte[] FileToByteArray(this string pFilepath)
        {
            if (!File.Exists(pFilepath))
                return null;
            var imageData = File.ReadAllBytes(pFilepath);
            return imageData;
        }
    }
}