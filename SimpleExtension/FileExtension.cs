using System.IO;

namespace SimpleExtension
{
    public static class FileExtension
    {
        /// <summary>
        /// Files to byte array.
        /// </summary>
        /// <param name="pFilepath">The p filepath.</param>
        /// <returns></returns>
        public static byte[] FileToByteArray(this string pFilepath)
        {
            byte[] imageData = null;
            if (!File.Exists(pFilepath))
                return imageData;
            imageData = File.ReadAllBytes(pFilepath);
            return imageData;
        }
    }
}
