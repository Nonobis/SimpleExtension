using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension
{
    /// <summary>
    /// Class IntExtension.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Betweens the specified p maximum.
        /// </summary>
        /// <param name="pMin">The p minimum.</param>
        /// <param name="pMax">The p maximum.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Between(this int pMin, int pMax)
        {
            return pMax > pMin;
        }

        /// <summary>
        /// Determines whether the specified minimum is within.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns><c>true</c> if the specified minimum is within; otherwise, <c>false</c>.</returns>
        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

        /// <summary>
        /// Return number of digits in a integer
        /// </summary>
        /// <param name="pNumber">The p number.</param>
        /// <returns>System.Int32.</returns>
        public static int CountDigits(this int pNumber)
        {
            // In case of negative numbers
            pNumber = Math.Abs(pNumber);

            if (pNumber >= 10)
                return CountDigits(pNumber / 10) + 1;
            return 1;
        }
    }
}
