using System;

namespace SimpleExtension.Core
{
    public static class TimespanExtension
    {
        /// <summary>
        ///     To the human time string.
        /// </summary>
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
        ///     Rounds to nearest.
        /// </summary>
        public static TimeSpan RoundToNearest(this TimeSpan a, TimeSpan roundTo)
        {
            var ticks = (long) (Math.Round(a.Ticks/(double) roundTo.Ticks)*roundTo.Ticks);
            return new TimeSpan(ticks);
        }

        /// <summary>
        ///     Strings to time span.
        /// </summary>
        public static TimeSpan ToTimespan(this string pTime)
        {
            TimeSpan timespan;
            var result = TimeSpan.TryParse(pTime, out timespan);
            return result ? timespan : new TimeSpan(0, 0, 0);
        }

        /// <summary>
        ///     Converts the seconds to a timespan object.
        /// </summary>
        public static TimeSpan ToTimespan(this int pSeconds)
        {
            return new TimeSpan(0, 0, pSeconds);
        }
    }
}