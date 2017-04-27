using System.IO;

namespace SimpleExtension.Core
{
    public static class FileExtension
    {
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