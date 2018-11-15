using System.IO;

namespace SimpleExtension
{
    public static class FileExtension
    {
        /// <summary>
        /// Converts to bytearray.
        /// </summary>
        /// <param name="pFilepath">The p filepath.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] FileToByteArray(this string pFilepath)
        {
            if (!File.Exists(pFilepath))
                return null;
            var imageData = File.ReadAllBytes(pFilepath);
            return imageData;
        }
    }
}