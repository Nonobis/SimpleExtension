using System;
using System.Globalization;

namespace SimpleExtension
{
    public static class DateTimeExtension
    {
        /// <summary>
        ///     The mi n_ dat e_ value
        /// </summary>
        public static DateTime MinDateValue = new DateTime(1900, 1, 1, 0, 0, 0, 0, CultureInfo.InvariantCulture.Calendar,
            DateTimeKind.Utc);

        /// <summary>
        ///     Returns 12:00am time for the date passed. Useful for date only search ranges start value
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>
        ///     DateTime.
        /// </returns>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        ///     Lengthes the of time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string LengthOfTime(this DateTime date)
        {
            var lengthOfTime = DateTime.Now.Subtract(date);
            if (lengthOfTime.Minutes == 0)
                return lengthOfTime.Seconds + "s";
            if (lengthOfTime.Hours == 0)
                return lengthOfTime.Minutes + "m";
            if (lengthOfTime.Days == 0)
                return lengthOfTime.Hours + "h";
            return lengthOfTime.Days + "d";
        }

        /// <summary>
        ///     Returns the Start of the given month (the fist millisecond of the given date)
        /// </summary>
        /// <param name="obj">
        ///     DateTime Base, from where the calculation will be preformed.
        /// </param>
        /// <returns>
        ///     Returns the Start of the given month (the fist millisecond of the given date)
        /// </returns>
        public static DateTime BeginningOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Returns true if the date is between or equal to one of the two values.
        /// </summary>
        /// <param name="date">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        ///     boolean value indicating if the date is between or equal to one of the two values
        /// </returns>
        public static bool Between(this DateTime date, DateTime startDate, DateTime endDate)
        {
            var ticks = date.Ticks;
            return (ticks >= startDate.Ticks) && (ticks <= endDate.Ticks);
        }

        /// <summary>
        ///     Creates a DateTime value from date and time input values
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns>
        ///     DateTime.
        /// </returns>
        public static DateTime DateTimeFromDateAndTime(this string date, string time)
        {
            return DateTime.Parse($"{date} {time}");
        }

        /// <summary>
        ///     Creates a DateTime Value from a DateTime date and a string time value.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns>
        ///     DateTime.
        /// </returns>
        public static DateTime DateTimeFromDateAndTime(this DateTime date, string time)
        {
            return DateTime.Parse($"{date.ToShortDateString()} {time}");
        }

        /// <summary>
        ///     Returns 12:59:59pm time for the date passed. Useful for date only search ranges end value
        /// </summary>
        /// <param name="date">
        ///     Date to convert
        /// </param>
        /// <returns>
        ///     DateTime.
        /// </returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        ///     Returns the very end of the given month (the last millisecond of the last hour for the
        ///     given date)
        /// </summary>
        /// <param name="obj">
        ///     DateTime Base, from where the calculation will be preformed.
        /// </param>
        /// <returns>
        ///     Returns the very end of the given month (the last millisecond of the last hour for the
        ///     given date)
        /// </returns>
        public static DateTime EndOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month), 23, 59, 59, 999);
        }

        /// <summary>
        ///     Converts a fractional hour value like 1.25 to 1:15 hours:minutes format
        /// </summary>
        /// <param name="hours">
        ///     Decimal hour value
        /// </param>
        /// <param name="format">
        ///     An optional format string where {0} is hours and {1} is minutes (ie: "{0}h:{1}m").
        /// </param>
        /// <returns>
        ///     System.String.
        /// </returns>
        public static string FractionalHoursToString(this decimal hours, string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "{0}:{1}";

            var tspan = TimeSpan.FromHours((double) hours);

            // Account for rounding error
            var minutes = tspan.Minutes;
            if (tspan.Seconds > 29)
                minutes++;

            return string.Format(format, tspan.Hours + tspan.Days*24, minutes);
        }

        /// <summary>
        ///     Converts a fractional hour value like 1.25 to 1:15 hours:minutes format
        /// </summary>
        /// <param name="hours">
        ///     Decimal hour value
        /// </param>
        /// <returns>
        ///     System.String.
        /// </returns>
        public static string FractionalHoursToString(this decimal hours)
        {
            return FractionalHoursToString(hours, null);
        }

        /// <summary>
        ///     Displays a date in friendly format.
        /// </summary>
        /// <param name="date">
        ///     The date.
        /// </param>
        /// <param name="showTime">
        ///     if set to <c>true</c> [show time].
        /// </param>
        /// <returns>
        ///     Today,Yesterday,Day of week or a string day (Jul 15, 2008)
        /// </returns>
        public static string FriendlyDateString(this DateTime date, bool showTime)
        {
            if (date < MinDateValue)
                return string.Empty;

            string formattedDate;
            if (date.Date == DateTime.Today)
                formattedDate = "Today"; //Resources.Resources.Today;
            else if (date.Date == DateTime.Today.AddDays(-1))
                formattedDate = "Yesterday"; //Resources.Resources.Yesterday;
            else if (date.Date > DateTime.Today.AddDays(-6))
                formattedDate = date.ToString("dddd");
            else
                formattedDate = date.ToString("MMMM dd, yyyy");

            if (showTime)
                formattedDate += $" @ {date.ToString("t").ToLower().Replace(" ", "")}";

            return formattedDate;
        }

        /// <summary>
        ///     Displays a number of milliseconds as friendly seconds, hours, minutes Pass -1 to get a
        ///     blank date.
        /// </summary>
        /// <param name="milliSeconds">
        ///     the elapsed milliseconds to display time for
        /// </param>
        /// <returns>
        ///     string in format of just now or 1m ago, 2h ago
        /// </returns>
        public static string FriendlyElapsedTimeString(this int milliSeconds)
        {
            if (milliSeconds < 0)
                return string.Empty;

            if (milliSeconds < 60000)
                return "just now";

            return milliSeconds < 3600000 ? $"{milliSeconds/60000}m ago" : $"{milliSeconds/3600000}h ago";
        }

        /// <summary>
        ///     Displays the elapsed time friendly seconds, hours, minutes
        /// </summary>
        /// <param name="elapsed">Timespan of elapsed time</param>
        /// <returns>
        ///     string in format of just now or 1m ago, 2h ago
        /// </returns>
        public static string FriendlyElapsedTimeString(this TimeSpan elapsed)
        {
            return FriendlyElapsedTimeString((int) elapsed.TotalMilliseconds);
        }

        /// <summary>
        ///     Converts the passed date time value to Mime formatted time string
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>
        ///     System.String.
        /// </returns>
        public static string MimeDateTime(this DateTime time)
        {
            var offset = TimeZone.CurrentTimeZone.GetUtcOffset(time);
            var sOffset = offset.Hours < 0 ? $"-{(offset.Hours*-1).ToString().PadLeft(2, '0')}" : $"+{offset.Hours.ToString().PadLeft(2, '0')}";
            sOffset += offset.Minutes.ToString().PadLeft(2, '0');
            return $"Date: {time.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture)} {sOffset}";
        }

        /// <summary>
        ///     Returns a short date time string
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="showTime">if set to <c>true</c> [show time].</param>
        /// <returns>
        ///     System.String.
        /// </returns>
        public static string ShortDateString(this DateTime date, bool showTime = false)
        {
            if (date < MinDateValue)
                return string.Empty;

            var dateString = date.ToString("MMM dd, yyyy");
            if (!showTime)
                return dateString;

            return $"{dateString} - {date.ToString("h:mmtt").ToLower()}";
        }

        /// <summary>
        ///     Returns a short date time string
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="showTime">if set to <c>true</c> [show time].</param>
        /// <returns>
        ///     System.String.
        /// </returns>
        public static string ShortDateString(this DateTime? date, bool showTime)
        {
            if (date == null)
                return string.Empty;

            return ShortDateString(date.Value, showTime);
        }
    }
}