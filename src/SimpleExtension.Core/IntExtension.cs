using System;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Int Extension Methods
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Betweens the specified p maximum.
        /// </summary>
        /// <param name="pMin">The p minimum.</param>
        /// <param name="pMax">The p maximum.</param>
        /// <returns></returns>
        public static bool Between(this int pMin, int pMax) => pMax > pMin;

        /// <summary>
        /// Determines whether the specified minimum is within.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is within; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithin(this int value, int minimum, int maximum) => value >= minimum && value <= maximum;

        /// <summary>
        /// Return number of digits in a integer
        /// </summary>
        /// <param name="pNumber">The p number.</param>
        /// <returns></returns>
        public static int CountDigits(this int pNumber)
        {
            // In case of negative numbers
            pNumber = Math.Abs(pNumber);

            if (pNumber >= 10)
            {
                return CountDigits(pNumber / 10) + 1;
            }

            return 1;
        }
    }
}
