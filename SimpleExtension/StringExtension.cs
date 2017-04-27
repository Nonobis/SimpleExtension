using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SimpleExtension
{
    public static class StringExtension
    {

        /// <summary>
        ///     Strings to bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] StringToBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        ///     Gets the double.
        /// </summary>
        public static double GetDouble(this string value, double defaultValue)
        {
            double result;
            //Try parsing in the current culture
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                result = defaultValue;
            return result;
        }


        /// <summary>
        ///     takes a substring between two anchor strings (or the end of the string if that anchor is null)
        /// </summary>
        public static string Substring(this string pSource, string from = null, string until = null,
            StringComparison comparison = StringComparison.InvariantCulture)
        {
            var fromLength = (from ?? string.Empty).Length;
            var startIndex = !string.IsNullOrEmpty(from)
                ? pSource.IndexOf(from, comparison) + fromLength
                : 0;

            if (startIndex < fromLength)
                throw new ArgumentException("from: Failed to find an instance of the first anchor");

            var endIndex = !string.IsNullOrEmpty(until)
                ? pSource.IndexOf(until, startIndex, comparison)
                : pSource.Length;

            if (endIndex < 0) throw new ArgumentException("until: Failed to find an instance of the last anchor");

            var subString = pSource.Substring(startIndex, endIndex - startIndex);
            return subString;
        }

        /// <summary>
        /// Convert string to Stram
        /// </summary>
        public static Stream ToStream(this string pValue)
        {
            // convert string to stream
            if (pValue != null)
            {
                var byteArray = Encoding.UTF8.GetBytes(pValue);

                var stream = new MemoryStream(byteArray);
                return stream;
            }
            return null;
        }

        /// <summary>
        ///   Convert Hexadecimal to string
        /// </summary>
        public static byte[] BinHexToString(this string hex)
        {
            var offset = hex.StartsWith("0x", StringComparison.Ordinal) ? 2 : 0;
            if (hex.Length%2 != 0)
                throw new ArgumentException($"Invalid Hex String : {hex.Length}");

            var ret = new byte[(hex.Length - offset)/2];

            for (var i = 0; i < ret.Length; i++)
            {
                ret[i] = (byte) ((ParseHexChar(hex[offset]) << 4) |
                                 ParseHexChar(hex[offset + 1]));
                offset += 2;
            }
            return ret;
        }

        /// <summary>
        ///     Extracts the string.
        /// </summary>
        public static string ExtractString(this string source,
            string beginDelim,
            string endDelim,
            bool caseSensitive = false,
            bool allowMissingEndDelimiter = false,
            bool returnDelimiters = false)
        {
            int at1, at2;

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (caseSensitive)
            {
                at1 = source.IndexOf(beginDelim, StringComparison.Ordinal);
                if (at1 == -1)
                    return string.Empty;

                at2 = !returnDelimiters ? source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.Ordinal) : source.IndexOf(endDelim, at1, StringComparison.Ordinal);
            }
            else
            {
                //string Lower = source.ToLower();
                at1 = source.IndexOf(beginDelim, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (at1 == -1)
                    return string.Empty;

                at2 = !returnDelimiters ? source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.OrdinalIgnoreCase) : source.IndexOf(endDelim, at1, StringComparison.OrdinalIgnoreCase);
            }

            if (allowMissingEndDelimiter && (at2 == -1))
                return source.Substring(at1 + beginDelim.Length);

            if ((at1 > -1) && (at2 > 1))
            {
                if (!returnDelimiters)
                    return source.Substring(at1 + beginDelim.Length, at2 - at1 - beginDelim.Length);
                return source.Substring(at1, at2 - at1 + endDelim.Length);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Fixes the HTML for display.
        /// </summary>
        public static string FixHtmlForDisplay(this string html)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\"", "&quot;");
            return html;
        }

        /// <summary>
        ///     HTML-encodes a string and returns the encoded string.
        /// </summary>
        public static string HtmlEncode(this string text)
        {
            if (text == null)
                return string.Empty;

            var sb = new StringBuilder(text.Length);

            var len = text.Length;
            for (var i = 0; i < len; i++)
                switch (text[i])
                {
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int) text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                        {
                            sb.Append(text[i]);
                        }
                        break;
                }
            return sb.ToString();
        }

        /// <summary>
        ///     Parses an string into an decimal. If the value can't be parsed
        ///     a default value is returned instead
        /// </summary>
        public static decimal ParseDecimal(this string input, decimal defaultValue, IFormatProvider numberFormat)
        {
            decimal val;
            if (!decimal.TryParse(input, NumberStyles.Any, numberFormat, out val))
            {
                return defaultValue;
            }
            return val;
        }

        /// <summary>
        ///     Parses an string into an integer. If the value can't be parsed
        ///     a default value is returned instead
        /// </summary>
        public static int ParseInt(this string input, int defaultValue, IFormatProvider numberFormat)
        {
            int val;
            if (!int.TryParse(input, NumberStyles.Any, numberFormat, out val))
            {
                return defaultValue;
            }
            return val;
        }

        /// <summary>
        ///     Parses an string into an integer. If the value can't be parsed
        ///     a default value is returned instead
        /// </summary>
        public static int ParseInt(this string input, int defaultValue)
        {
            return ParseInt(input, defaultValue, CultureInfo.CurrentCulture.NumberFormat);
        }

        /// <summary>
        ///     Return a string in proper Case format
        /// </summary>
        public static string ProperCase(string input)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        /// <summary>
        ///     Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        public static string ReplaceString(this string origString, string findString,
            string replaceString, bool caseInsensitive)
        {
            var at1 = 0;
            while (true)
            {
                if (caseInsensitive)
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1,
                        StringComparison.OrdinalIgnoreCase);
                else
                    at1 = origString.IndexOf(findString, at1, StringComparison.Ordinal);

                if (at1 == -1)
                    break;

                origString =
                    $"{origString.Substring(0, at1)}{replaceString}{origString.Substring(at1 + findString.Length)}";

                at1 += replaceString.Length;
            }

            return origString;
        }

        /// <summary>
        ///     String replace function that support
        /// </summary>>
        public static string ReplaceStringInstance(this string origString, string findString,
            string replaceWith, int instance,
            bool caseInsensitive)
        {
            if (instance == -1)
                return ReplaceString(origString, findString, replaceWith, caseInsensitive);

            var at1 = 0;
            for (var x = 0; x < instance; x++)
            {
                if (caseInsensitive)
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1,
                        StringComparison.OrdinalIgnoreCase);
                else
                    at1 = origString.IndexOf(findString, at1, StringComparison.Ordinal);

                if (at1 == -1)
                    return origString;

                if (x < instance - 1)
                    at1 += findString.Length;
            }

            return $"{origString.Substring(0, at1)}{replaceWith}{origString.Substring(at1 + findString.Length)}";
        }

        /// <summary>
        ///     Replicates an input string n number of times
        /// </summary>
        public static string Replicate(this string input, int charCount)
        {
            return new StringBuilder().Insert(0, input, charCount).ToString();
        }

        /// <summary>
        ///     Replicates a character n number of times and returns a string
        /// </summary>
        public static string Replicate(this char character, int charCount)
        {
            return new string(character, charCount);
        }

        /// <summary>
        ///     Sets the property.
        /// </summary>
        public static string SetProperty(this string propertyString, string key, string value)
        {
            var extract = ExtractString(propertyString, $"<{key}>", $"</{key}>");

            if (string.IsNullOrEmpty(value) && (extract != string.Empty))
                return propertyString.Replace(extract, "");

            var xmlLine = $"<{key}>{value}</{key}>";

            // replace existing
            if (extract != string.Empty)
                return propertyString.Replace(extract, xmlLine);

            // add new
            return $"{propertyString}{xmlLine}\r\n";
        }

        /// <summary>
        ///     Creates a Stream from a string. Internally creates
        ///     a memory stream and returns that.
        /// </summary>
        public static Stream StringToStream(this string text, Encoding encoding)
        {
            var ms = new MemoryStream(text.Length*2);
            var data = encoding.GetBytes(text);
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        ///     Creates a Stream from a string. Internally creates
        ///     a memory stream and returns that.
        /// </summary>
        public static Stream StringToStream(this string text)
        {
            return StringToStream(text, Encoding.GetEncoding(0));
        }

        /// <summary>
        ///     Strips HTML tags out of an HTML string and returns just the text.
        /// </summary>
        public static string StripHtml(this string html)
        {
            html = Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            html = html.Replace("\t", " ");
            html = html.Replace("\r\n", string.Empty);
            html = html.Replace("   ", " ");
            return html.Replace("  ", " ");
        }

        /// <summary>
        ///     Strips all non digit values from a string and only
        ///     returns the numeric string.
        /// </summary>
        public static string StripNonNumber(this string input)
        {
            var chars = input.ToCharArray();
            var sb = new StringBuilder();
            foreach (var chr in chars)
                if (char.IsNumber(chr) || char.IsSeparator(chr))
                    sb.Append(chr);

            return sb.ToString();
        }

        /// <summary>
        ///     Terminates a string with the given end string/character, but only if the
        ///     value specified doesn't already exist and the string is not empty.
        /// </summary>
        public static string TerminateString(this string value, string terminator)
        {
            if (string.IsNullOrEmpty(value) || value.EndsWith(terminator))
                return value;

            return $"{value}{terminator}";
        }

        /// <summary>
        ///     Returns an abstract of the provided text by returning up to Length characters
        ///     of a text string. If the text is truncated a ... is appended.
        /// </summary>
        public static string TextAbstract(this string text, int length)
        {
            if (text == null)
                return string.Empty;

            if (text.Length <= length)
                return text;

            text = text.Substring(0, length);
            text = text.Substring(0, text.LastIndexOf(" ", StringComparison.Ordinal));
            return $"{text}...";
        }

        /// <summary>
        ///     Takes a phrase and turns it into CamelCase text.
        ///     White Space, punctuation and separators are stripped
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>System.String.</returns>
        public static string ToCamelCase(this string phrase)
        {
            if (phrase == null)
                return string.Empty;

            var sb = new StringBuilder(phrase.Length);
            // First letter is always upper case
            var nextUpper = true;
            foreach (var ch in phrase)
            {
                if (char.IsWhiteSpace(ch) || char.IsPunctuation(ch) || char.IsSeparator(ch))
                {
                    nextUpper = true;
                    continue;
                }
                sb.Append(nextUpper ? char.ToUpper(ch) : char.ToLower(ch));
                nextUpper = false;
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Trims a sub string from a string
        /// </summary>
        public static string TrimStart(this string text, string textToTrim, bool caseInsensitive)
        {
            while (true)
            {
                var match = text.Substring(0, textToTrim.Length);
                if ((match == textToTrim) ||
                    (caseInsensitive && (string.Equals(match, textToTrim, StringComparison.CurrentCultureIgnoreCase))))
                    text = text.Length <= match.Length ? "" : text.Substring(textToTrim.Length);
                else
                    break;
            }
            return text;
        }

        /// <summary>
        ///     Trims a string to a specific number of max characters
        /// </summary>
        public static string TrimTo(this string value, int charCount)
        {
            if (value == null)
                return string.Empty;

            if (value.Length > charCount)
                return value.Substring(0, charCount);

            return value;
        }

        /// <summary>
        ///     Parses the hexadecimal character.
        /// </summary>
        private static int ParseHexChar(this char c)
        {
            if ((c >= '0') && (c <= '9'))
                return c - '0';
            if ((c >= 'A') && (c <= 'F'))
                return c - 'A' + 10;
            if ((c >= 'a') && (c <= 'f'))
                return c - 'a' + 10;

            throw new ArgumentException($"Invalid Hex String{c}");
        }
    }
}