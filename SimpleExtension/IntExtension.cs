using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension
{
    public static class IntExtension
    {
        /// <summary>
        /// Return number of digits in a integer
        /// </summary>
        /// <param name="pNumber"></param>
        /// <returns></returns>
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
