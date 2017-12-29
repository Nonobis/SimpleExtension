using System;

namespace SimpleExtension.Core
{
    public static class DateTimeExtension
    {
        /// <summary>
        ///   Mininmun DateTime Value
        /// </summary>
        public static DateTime MinDateValue = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Returns 12:00am time for the date passed. Useful for date only search ranges start value
        /// </summary>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        ///     Lengthes the of time.
        /// </summary>
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
        public static DateTime BeginningOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Returns true if the date is between or equal to one of the two values.
        /// </summary>
        public static bool Between(this DateTime date, DateTime startDate, DateTime endDate)
        {
            var ticks = date.Ticks;
            return (ticks >= startDate.Ticks) && (ticks <= endDate.Ticks);
        }

        /// <summary>
        ///     Creates a DateTime value from date and time input values
        /// </summary>
        public static DateTime DateTimeFromDateAndTime(this string date, string time)
        {
            return DateTime.Parse($"{date} {time}");
        }

        /// <summary>
        ///     Creates a DateTime Value from a DateTime date and a string time value.
        /// </summary>
        public static DateTime DateTimeFromDateAndTime(this DateTime date, string time)
        {
            return DateTime.Parse($"{date.ShortDateString()} {time}");
        }

        /// <summary>
        ///     Returns 12:59:59pm time for the date passed. Useful for date only search ranges end value
        /// </summary>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        ///     Returns the very end of the given month (the last millisecond of the last hour for the
        ///     given date)
        /// </summary>
        public static DateTime EndOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month), 23, 59, 59, 999);
        }

        /// <summary>
        ///     Converts a fractional hour value like 1.25 to 1:15 hours:minutes format
        /// </summary>
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
        public static string FractionalHoursToString(this decimal hours)
        {
            return FractionalHoursToString(hours, null);
        }

        /// <summary>
        ///     Displays a date in friendly format.
        /// </summary>
        public static string FriendlyDateString(this DateTime date, bool showTime)
        {
            if (date < MinDateValue)
                return string.Empty;

            string formattedDate;
            if (date.Date == DateTime.Today)
                formattedDate = " Today";
            else if (date.Date == DateTime.Today.AddDays(-1))
                formattedDate = " Yesterday";
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
        public static string FriendlyElapsedTimeString(this TimeSpan elapsed)
        {
            return FriendlyElapsedTimeString((int) elapsed.TotalMilliseconds);
        }

        /// <summary>
        ///     Converts the passed date time value to Mime formatted time string
        /// </summary>
        public static string MimeDateTime(this DateTime time)
        {
            // TODO : Wait for netstandard 1.7
            /*
            var offset = TimeZone.CurrentTimeZone.GetUtcOffset(time);
            var sOffset = offset.Hours < 0 ? $"-{(offset.Hours*-1).ToString().PadLeft(2, '0')}" : $"+{offset.Hours.ToString().PadLeft(2, '0')}";
            sOffset += offset.Minutes.ToString().PadLeft(2, '0');
            return $"Date: {time.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture)} {sOffset}";
            */
            return string.Empty;
        }

        /// <summary>
        ///     Returns a short date time string
        /// </summary>
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
        public static string ShortDateString(this DateTime? date, bool showTime)
        {
            if (date == null)
                return string.Empty;

            return ShortDateString(date.Value, showTime);
        }
    }
}