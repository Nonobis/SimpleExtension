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
        ///     Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            return new Bitmap(imageIn).ToArray(imageIn.GetImageFormat());
        }
    }
}