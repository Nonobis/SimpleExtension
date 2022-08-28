using System;
using System.Threading.Tasks;
using System.Threading;

namespace SimpleExtension.Core
{
    /// <summary>
    /// TimeSpan Extensions Method
    /// </summary>
    public static class TimespanExtension
    {
        /// <summary>
        /// Timeouts the after.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>TResult.</returns>
        /// <exception cref="System.TimeoutException">The operation has timed out.</exception>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using CancellationTokenSource timeoutCancellationTokenSource = new();
            Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return await task;  // Very important in order to propagate exceptions
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }

        /// <summary>
        /// Elapseds the specified source dt.
        /// </summary>
        /// <param name="sourceDt">The source dt.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Elapsed(this DateTime sourceDt) => DateTime.Now.Subtract(sourceDt);

        /// <summary>
        /// Elapseds the specified substract dt.
        /// </summary>
        /// <param name="sourceDt">The source dt.</param>
        /// <param name="substractDt">The substract dt.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Elapsed(this DateTime sourceDt, DateTime substractDt) => sourceDt.Subtract(substractDt);

        /// <summary>
        /// Elapseds the UTC.
        /// </summary>
        /// <param name="sourceDtUTC">The source dt UTC.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan ElapsedUTC(this DateTime sourceDtUTC) => DateTime.UtcNow.Subtract(sourceDtUTC);

        /// <summary>
        /// Elapseds the UTC.
        /// </summary>
        /// <param name="sourceDtUTC">The source dt UTC.</param>
        /// <param name="substractDtUTC">The substract dt UTC.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan ElapsedUTC(this DateTime sourceDtUTC, DateTime substractDtUTC) => sourceDtUTC.Subtract(substractDtUTC);

        /// <summary>
        /// Gets the DTC time.
        /// </summary>
        /// <param name="nanoseconds">The nanoseconds.</param>
        /// <param name="ticksPerNanosecond">The ticks per nanosecond.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetDTCTime(this ulong nanoseconds, ulong ticksPerNanosecond)
        {
            DateTime pointOfReference = new(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long ticks = (long)(nanoseconds / ticksPerNanosecond);
            return pointOfReference.AddTicks(ticks);
        }

        /// <summary>
        /// Gets the DTC time.
        /// </summary>
        /// <param name="nanoseconds">The nanoseconds.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetUTCDateTime(this ulong nanoseconds) => GetDTCTime(nanoseconds, 100);

        /// <summary>
        /// To the human time string.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="significantDigits">The significant digits.</param>
        /// <returns></returns>
        public static string ToHumanTimeString(this TimeSpan span, int significantDigits = 3)
        {
            string format = "G" + significantDigits;
            if (span.TotalMinutes < 60)
            {
                if (span.TotalMilliseconds < 1000)
                {
                    return span.TotalMilliseconds.ToString(format) + " Miliseconds";
                }
                else if (span.TotalSeconds < 60)
                {
                    return $"{span.TotalSeconds.ToString(format)} Seconds";
                }
                else
                {
                    return (span.TotalMinutes.ToString(format) + " Minutes");
                }
            }
            else
            {
                if (span.TotalMilliseconds < 1000)
                {
                    return span.TotalMilliseconds.ToString(format) + " Miliseconds";
                }
                else if (span.TotalSeconds < 60)
                {
                    return $"{span.TotalSeconds.ToString(format)} Seconds";
                }
                else if (span.TotalHours < 24)
                {
                    return span.TotalHours.ToString(format) + " Hours";
                }
                else
                {
                    return span.TotalDays.ToString(format) + " Days";
                }
            }
        }

        /// <summary>
        /// Rounds to nearest.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="roundTo">The round to.</param>
        /// <returns></returns>
        public static TimeSpan RoundToNearest(this TimeSpan a, TimeSpan roundTo)
        {
            long ticks = (long) (Math.Round(a.Ticks/(double) roundTo.Ticks)*roundTo.Ticks);
            return new TimeSpan(ticks);
        }

        /// <summary>
        /// Strings to time span.
        /// </summary>
        /// <param name="pTime">The p time.</param>
        /// <returns></returns>
        public static TimeSpan ToTimespan(this string pTime)
        {
            bool result = TimeSpan.TryParse(pTime, out TimeSpan timespan);
            return result ? timespan : new TimeSpan(0, 0, 0);
        }

        /// <summary>
        /// Converts the seconds to a timespan object.
        /// </summary>
        /// <param name="pSeconds">The p seconds.</param>
        /// <returns></returns>
        public static TimeSpan ToTimespan(this int pSeconds) => new(0, 0, pSeconds);
    }
}