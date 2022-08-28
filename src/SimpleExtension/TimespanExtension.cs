using System;

namespace SimpleExtension
{
    /// <summary>
    /// Class TimespanExtension.
    /// </summary>
    public static class TimespanExtension
    {
        /// <summary>
        /// Converts to humantimestring.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="significantDigits">The significant digits.</param>
        /// <returns>System.String.</returns>
        public static string ToHumanTimeString(this TimeSpan span, int significantDigits = 3)
        {
            var format = "G" + significantDigits;
            return span.TotalMilliseconds < 1000
                ? span.TotalMilliseconds.ToString(format) + " Miliseconds"
                : (span.TotalSeconds < 60
                    ? span.TotalSeconds.ToString(format) + " Seconds"
                    : (span.TotalMinutes < 60
                        ? span.TotalMinutes.ToString(format) + " Minutes"
                        : (span.TotalHours < 24
                            ? span.TotalHours.ToString(format) + " Hours"
                            : span.TotalDays.ToString(format) + " Days")));
        }

        /// <summary>
        /// Converts to nearest.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="roundTo">Converts to .</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan RoundToNearest(this TimeSpan a, TimeSpan roundTo)
        {
            var ticks = (long) (Math.Round(a.Ticks/(double) roundTo.Ticks)*roundTo.Ticks);
            return new TimeSpan(ticks);
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="pTime">The p time.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan ToTimespan(this string pTime)
        {
            TimeSpan timespan;
            var result = TimeSpan.TryParse(pTime, out timespan);
            return result ? timespan : new TimeSpan(0, 0, 0);
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="pSeconds">The p seconds.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan ToTimespan(this int pSeconds)
        {
            return new TimeSpan(0, 0, pSeconds);
        }
    }
}