using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleExtension
{
    /// <summary>
    ///     Class ImageExtension.
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        ///     Bytes the array to image.
        /// </summary>
        /// <param name="byteArrayIn">The byte array in.</param>
        /// <returns>Image.</returns>
        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        ///     Byteses to string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string BytesToString(this byte[] bytes)
        {
            var chars = new char[bytes.Length/sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        ///     Gets the image format.
        /// </summary>
        /// <param name="pImage">The p image.</param>
        /// <returns>ImageFormat.</returns>
        public static ImageFormat GetImageFormat(this Image pImage)
        {
            if (pImage.RawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (pImage.RawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            if (pImage.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            if (pImage.RawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            if (pImage.RawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            if (pImage.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            if (pImage.RawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            if (pImage.RawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.Png;
            if (pImage.RawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            return ImageFormat.Wmf;
        }

        /// <summary>
        ///     Gets the MD5 hash.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns></returns>
        public static string GetMd5Hash(this byte[] pBytes)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var hash = md5.ComputeHash(pBytes);
                var s = new StringBuilder();
                foreach (var b in hash)
                    s.Append(b.ToString("x2").ToLower());
                return s.ToString();
            }
        }

        /// <summary>
        ///     Gets the sha256 hash.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns></returns>
        public static string GetSha256Hash(this byte[] pBytes)
        {
            using (var sha = new SHA256Managed())
            {
                var checksum = sha.ComputeHash(pBytes);
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
        }

        /// <summary>
        ///     Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            return new Bitmap(imageIn).ToArray(imageIn.GetImageFormat());
        }

        /// <summary>
        ///     Strings to bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] StringToBytes(this string str)
        {
            var bytes = new byte[str.Length*sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        ///     To the array.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToArray(this Bitmap image, ImageFormat format)
        {
            byte[] array;
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, Equals(format, ImageFormat.MemoryBmp) ? ImageFormat.Bmp : format);
                array = memoryStream.ToArray();
            }
            return array;
        }
    }
}