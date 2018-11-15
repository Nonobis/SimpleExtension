using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension
{
    /// <summary>
    /// Class BitmapExtension.
    /// </summary>
    public static class BitmapExtension
    {
        /// <summary>
        /// Converts to grayscale.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap ToGrayScale(this Bitmap image)
        {
            var newBitmap = new Bitmap(image.Width, image.Height);
            using (var g = Graphics.FromImage(newBitmap))
            {
                //The grayscale ColorMatrix
                var colorMatrix = new ColorMatrix(new float[][] {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                    g.Dispose();
                    return newBitmap;

                }
            }
        }

        /// <summary>
        /// Converts to array.
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
