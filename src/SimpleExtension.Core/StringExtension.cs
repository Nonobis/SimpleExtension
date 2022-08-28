using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleExtension.Core
{
    /// <summary>
    /// String Extensions Methods
    /// </summary>
    public static class StringExtension
    {
        private const char HIGH_SURROGATE_START = '\uD800';
        private const char LOW_SURROGATE_START = '\uDC00';
        private const int UNICODE_PLANE01_START = 0x10000;

        /// <summary>
        /// Froms the base64.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns>System.String.</returns>
        public static string FromBase64(this string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Gets the URL from absolute path.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <param name="webRootPath">The web root path.</param>
        /// <returns>System.String.</returns>
        public static string GetUrlFromAbsolutePath(this string absolutePath, string webRootPath) => absolutePath.Replace(webRootPath, "").Replace(@"\", "/");

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T GetValue<T>(this string value) => (T)Convert.ChangeType(value, typeof(T));

        /// <summary>
        /// Called when [hexa].
        /// </summary>
        /// <param name="test">The test.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool OnlyHexa(this string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            const string Pat = @"\A\b[0-9a-fA-F]+\b\Z";
            return Regex.IsMatch(test, Pat);
        }

        /// <summary>
        /// Removes the script and style.
        /// </summary>
        /// <param name="HTML">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string RemoveScriptAndStyle(this string HTML)
        {
            const string Pat = "<(script|style)\\b[^>]*?>.*?</\\1>";
            return Regex.Replace(HTML, Pat, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        /// <summary>
        /// Converts to ascii.
        /// </summary>
        /// <param name="hexString">The hexadecimal string.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">hexString</exception>
        /// <exception cref="System.ArgumentException">'{nameof(hexString)}' must have an even length - hexString</exception>
        public static string ToAscii(this string hexString)
        {
            if (hexString == null)
            {
                throw new ArgumentNullException(nameof(hexString));
            }

            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException($"'{nameof(hexString)}' must have an even length", nameof(hexString));
            }

            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts to asciibytearray.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToAsciiByteArray(this string str) => Encoding.ASCII.GetBytes(str);

        /// <summary>
        /// Converts to base64.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64(this string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }


        /// <summary>
        /// Takes a phrase and turns it into CamelCase text.
        /// White Space, punctuation and separators are stripped
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string ToCamelCase(this string s)
        {
            string x = s.Replace("_", "");
            if (x.Length == 0)
            {
                return null;
            }

            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
        }

        /// <summary>
        /// Converts to firstchartoupper.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <exception cref="ArgumentNullException">input</exception>
        /// <exception cref="ArgumentException">input</exception>
        public static string ToFirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// Converts to hex.
        /// </summary>
        /// <param name="asciiString">The ASCII string.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">asciiString</exception>
        public static string ToHex(this string asciiString)
        {
            if (asciiString == null)
            {
                throw new ArgumentNullException(nameof(asciiString));
            }

            StringBuilder hex = new();
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex.AppendFormat("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex.ToString();
        }

        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt(this ushort value) => Convert.ToInt32(value);

        /// <summary>A string extension method that converts the s to a pascal case.</summary>
        /// <param name="s">The s to act on.</param>
        /// <returns>S as a string.</returns>
        public static string ToPascalCase(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string x = s.ToCamelCase();
                return char.ToUpper(x[0]) + x.Substring(1);
            }
            return s;
        }
        /// <summary>
        /// Converts to stream.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Stream.</returns>
        public static Stream ToStream(this string data)
        {
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            writer.Write(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Converts to UTF8.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public static string ToUTF8(this string plainText)
        {
            try
            {
                byte[] plainTextBytes = Encoding.Default.GetBytes(plainText);
                return Encoding.UTF8.GetString(plainTextBytes);
            }
            catch (Exception)
            {
                return plainText;
            }
        }

        /// <summary>
        /// Converts UTF 32 characters (in hexadecimal notation) to C# string.
        /// </summary>
        /// <param name="hex">The UTF-32 character in hexadecimal notation.</param>
        /// <returns>The C# string</returns>
        public static string UTF32HexToString(this string hex)
        {
            uint.TryParse(hex, NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out var smpChar);

            // Convertion en UTF32 extrait du code source du framework
            // cf : https://github.com/Microsoft/referencesource/blob/master/System/net/System/Net/WebUtility.cs
            int utf32 = (int)(smpChar - UNICODE_PLANE01_START);
            char leadingSurrogate = (char)((utf32 / 0x400) + HIGH_SURROGATE_START);
            char trailingSurrogate = (char)((utf32 % 0x400) + LOW_SURROGATE_START);

            var builder = new StringBuilder();
            builder.Append(leadingSurrogate);
            builder.Append(trailingSurrogate);

            return builder.ToString();
        }

        /// <summary>
        /// Convert Hexadecimal to string
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Hex String : {hex.Length}</exception>
        public static byte[] BinHexToString(this string hex)
        {
            int offset = hex.StartsWith("0x", StringComparison.Ordinal) ? 2 : 0;
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException($"Invalid Hex String : {hex.Length}");
            }

            byte[] ret = new byte[(hex.Length - offset) / 2];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (byte)((ParseHexChar(hex[offset]) << 4) |
                                 ParseHexChar(hex[offset + 1]));
                offset += 2;
            }
            return ret;
        }

        /// <summary>
        /// Extracts the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="beginDelim">The begin delimiter.</param>
        /// <param name="endDelim">The end delimiter.</param>
        /// <param name="caseSensitive">if set to <c>true</c> [case sensitive].</param>
        /// <param name="allowMissingEndDelimiter">if set to <c>true</c> [allow missing end delimiter].</param>
        /// <param name="returnDelimiters">if set to <c>true</c> [return delimiters].</param>
        /// <returns></returns>
        public static string ExtractString(this string source,
            string beginDelim,
            string endDelim,
            bool caseSensitive = false,
            bool allowMissingEndDelimiter = false,
            bool returnDelimiters = false)
        {
            int at1, at2;

            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            if (caseSensitive)
            {
                at1 = source.IndexOf(beginDelim, StringComparison.Ordinal);
                if (at1 == -1)
                {
                    return string.Empty;
                }

                at2 = !returnDelimiters ? source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.Ordinal) : source.IndexOf(endDelim, at1, StringComparison.Ordinal);
            }
            else
            {
                at1 = source.IndexOf(beginDelim, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (at1 == -1)
                {
                    return string.Empty;
                }

                at2 = !returnDelimiters ? source.IndexOf(endDelim, at1 + beginDelim.Length, StringComparison.OrdinalIgnoreCase) : source.IndexOf(endDelim, at1, StringComparison.OrdinalIgnoreCase);
            }

            if (allowMissingEndDelimiter && (at2 == -1))
            {
                return source.Substring(at1 + beginDelim.Length);
            }

            if ((at1 > -1) && (at2 > 1))
            {
                if (!returnDelimiters)
                {
                    return source.Substring(at1 + beginDelim.Length, at2 - at1 - beginDelim.Length);
                }

                return source.Substring(at1, at2 - at1 + endDelim.Length);
            }

            return string.Empty;
        }

        /// <summary>
        /// Fixes the HTML for display.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string FixHtmlForDisplay(this string html)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\"", "&quot;");
            return html;
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static double GetDouble(this string value, double defaultValue)
        {
            //Try parsing in the current culture
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out double result) &&
                //Then try in US english
                !double.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Gets the until or empty.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="stopAt">The stop at.</param>
        /// <returns></returns>
        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// HTML-encodes a string and returns the encoded string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string HtmlEncode(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
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
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                        {
                            sb.Append(text[i]);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Parses an string into an decimal. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="numberFormat">The number format.</param>
        /// <returns></returns>
        public static decimal ParseDecimal(this string input, decimal defaultValue, IFormatProvider numberFormat)
        {
            if (!decimal.TryParse(input, NumberStyles.Any, numberFormat, out decimal val))
            {
                return defaultValue;
            }
            return val;
        }

        /// <summary>
        /// Parses an string into an integer. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="numberFormat">The number format.</param>
        /// <returns></returns>
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
        /// Parses an string into an integer. If the value can't be parsed
        /// a default value is returned instead
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ParseInt(this string input, int defaultValue) => ParseInt(input, defaultValue, CultureInfo.CurrentCulture.NumberFormat);

        /// <summary>
        /// Transform string with proper case
        /// </summary>
        /// <param name="pInput">Text to format as string.</param>
        /// <returns></returns>
        public static string ProperCase(this string pInput) => InternalProperCase(pInput, false);

        /// <summary>
        /// Transform string with proper case
        /// </summary>
        /// <param name="pInput">Text to format as string.</param>
        /// <param name="pAddPointAfterSingleLetter">Add a period if desired.</param>
        /// <returns></returns>
        public static string ProperCase(this string pInput, bool pAddPointAfterSingleLetter) => InternalProperCase(pInput, pAddPointAfterSingleLetter);

        /// <summary>
        /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        /// <param name="origString">The original string.</param>
        /// <param name="findString">The find string.</param>
        /// <param name="replaceString">The replace string.</param>
        /// <param name="caseInsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns></returns>
        public static string ReplaceString(this string origString, string findString,
            string replaceString, bool caseInsensitive)
        {
            int at1 = 0;
            while (true)
            {
                if (caseInsensitive)
                {
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1,
                        StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    at1 = origString.IndexOf(findString, at1, StringComparison.Ordinal);
                }

                if (at1 == -1)
                {
                    break;
                }

                origString =
                    $"{origString.Substring(0, at1)}{replaceString}{origString.Substring(at1 + findString.Length)}";

                at1 += replaceString.Length;
            }

            return origString;
        }

        /// <summary>
        /// String replace function that support
        /// </summary>
        /// <param name="origString">The original string.</param>
        /// <param name="findString">The find string.</param>
        /// <param name="replaceWith">The replace with.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="caseInsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns></returns>
        /// &gt;
        public static string ReplaceStringInstance(this string origString, string findString,
            string replaceWith, int instance,
            bool caseInsensitive)
        {
            if (instance == -1)
            {
                return ReplaceString(origString, findString, replaceWith, caseInsensitive);
            }

            int at1 = 0;
            for (int x = 0; x < instance; x++)
            {
                if (caseInsensitive)
                {
                    at1 = origString.IndexOf(findString, at1, origString.Length - at1,
                        StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    at1 = origString.IndexOf(findString, at1, StringComparison.Ordinal);
                }

                if (at1 == -1)
                {
                    return origString;
                }

                if (x < instance - 1)
                {
                    at1 += findString.Length;
                }
            }

            return $"{origString.Substring(0, at1)}{replaceWith}{origString.Substring(at1 + findString.Length)}";
        }

        /// <summary>
        /// Replicates an input string n number of times
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="charCount">The character count.</param>
        /// <returns></returns>
        public static string Replicate(this string input, int charCount) => new StringBuilder().Insert(0, input, charCount).ToString();

        /// <summary>
        /// Replicates a character n number of times and returns a string
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="charCount">The character count.</param>
        /// <returns></returns>
        public static string Replicate(this char character, int charCount) => new(character, charCount);

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyString">The property string.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string SetProperty(this string propertyString, string key, string value)
        {
            string extract = ExtractString(propertyString, $"<{key}>", $"</{key}>");

            if (string.IsNullOrEmpty(value) && (extract != string.Empty))
            {
                return propertyString.Replace(extract, "");
            }

            string xmlLine = $"<{key}>{value}</{key}>";

            // replace existing
            if (extract != string.Empty)
            {
                return propertyString.Replace(extract, xmlLine);
            }

            // add new
            return $"{propertyString}{xmlLine}\r\n";
        }

        /// <summary>
        /// Strings to bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        /// System.Byte[].
        /// </returns>
        public static byte[] StringToBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        /// <summary>
        /// Creates a Stream from a string. Internally creates
        /// a memory stream and returns that.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static Stream StringToStream(this string text, Encoding encoding)
        {
            MemoryStream ms = new(text.Length * 2);
            byte[] data = encoding.GetBytes(text);
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Creates a Stream from a string. Internally creates
        /// a memory stream and returns that.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static Stream StringToStream(this string text) => StringToStream(text, Encoding.GetEncoding(0));

        /// <summary>
        /// Strips HTML tags out of an HTML string and returns just the text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string StripHtml(this string html)
        {
            html = Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            html = html.Replace("\t", " ");
            html = html.Replace("\r\n", string.Empty);
            html = html.Replace("   ", " ");
            return html.Replace("  ", " ");
        }

        /// <summary>
        /// Strips all non digit values from a string and only
        /// returns the numeric string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string StripNonNumber(this string input)
        {
            char[] chars = input.ToCharArray();
            StringBuilder sb = new();
            foreach (char chr in chars)
            {
                if (char.IsNumber(chr) || char.IsSeparator(chr))
                {
                    sb.Append(chr);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// takes a substring between two anchor strings (or the end of the string if that anchor is null)
        /// </summary>
        /// <param name="pSource">The p source.</param>
        /// <param name="from">From.</param>
        /// <param name="until">The until.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">from: Failed to find an instance of the first anchor
        /// or
        /// until: Failed to find an instance of the last anchor</exception>
        public static string Substring(this string pSource, string from = null, string until = null,
            StringComparison comparison = StringComparison.CurrentCulture)
        {
            int fromLength = (from ?? string.Empty).Length;
            int startIndex = !string.IsNullOrEmpty(from)
                ? pSource.IndexOf(from, comparison) + fromLength
                : 0;

            if (startIndex < fromLength)
            {
                throw new ArgumentException("from: Failed to find an instance of the first anchor");
            }

            int endIndex = !string.IsNullOrEmpty(until)
                ? pSource.IndexOf(until, startIndex, comparison)
                : pSource.Length;

            if (endIndex < 0)
            {
                throw new ArgumentException("until: Failed to find an instance of the last anchor");
            }

            return pSource.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Terminates a string with the given end string/character, but only if the
        /// value specified doesn't already exist and the string is not empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="terminator">The terminator.</param>
        /// <returns></returns>
        public static string TerminateString(this string value, string terminator)
        {
            if (string.IsNullOrEmpty(value) || value.EndsWith(terminator))
            {
                return value;
            }

            return $"{value}{terminator}";
        }

        /// <summary>
        /// Returns an abstract of the provided text by returning up to Length characters
        /// of a text string. If the text is truncated a ... is appended.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string TextAbstract(this string text, int length)
        {
            if (text == null)
            {
                return string.Empty;
            }

            if (text.Length <= length)
            {
                return text;
            }

            text = text.Substring(0, length);
            text = text.Substring(0, text.LastIndexOf(" ", StringComparison.Ordinal));
            return $"{text}...";
        }
        /// <summary>
        /// Trims a sub string from a string
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="textToTrim">The text to trim.</param>
        /// <param name="caseInsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns></returns>
        public static string TrimStart(this string text, string textToTrim, bool caseInsensitive)
        {
            while (true)
            {
                string match = text.Substring(0, textToTrim.Length);
                if ((match == textToTrim) ||
                    (caseInsensitive && (string.Equals(match, textToTrim, StringComparison.CurrentCultureIgnoreCase))))
                {
                    text = text.Length <= match.Length ? "" : text.Substring(textToTrim.Length);
                }
                else
                {
                    break;
                }
            }
            return text;
        }

        /// <summary>
        /// Trims a string to a specific number of max characters
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="charCount">The character count.</param>
        /// <returns></returns>
        public static string TrimTo(this string value, int charCount)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value.Length > charCount)
            {
                return value.Substring(0, charCount);
            }

            return value;
        }

        /// <summary>
        /// Return a string in proper Case format
        /// </summary>
        /// <param name="pInput">Text to format as string.</param>
        /// <param name="pAddPointAfterSingleLetter">Add a period if desired.</param>
        /// <returns></returns>
        private static string InternalProperCase(string pInput, bool pAddPointAfterSingleLetter)
        {
            //This code is modified from the http://www.windowsdevcenter.com/pub/a/oreilly/windows/news/csharp_0101.html link and related posts.

            string pattern = @"\w+|\W+";
            string result = "";
            bool capitalizeNext = true;

            foreach (Match m in Regex.Matches(pInput, pattern))
            {
                // get the matched string
                string x = m.ToString().ToLower();

                // if the first char is lower case
                if (char.IsLower(x[0]) && capitalizeNext)
                {
                    // capitalize it
                    x = char.ToUpper(x[0]) + x.Substring(1, x.Length - 1);
                }

                // Check if the word starts with Mc
                //if (x[0] == 'M' && x[1] == 'c' && !String.IsNullOrEmpty(x[2].ToString()))
                if (x[0] == 'M' && x.Length > 1 && x[1] == 'c' && !string.IsNullOrEmpty(x[2].ToString()))
                {
                    // Capitalize the letter after Mc
                    x = "Mc" + char.ToUpper(x[2]) + x.Substring(3, x.Length - 3);
                }

                if (capitalizeNext == false)
                {
                    capitalizeNext = true;
                }

                // if the apostrophe is at the end i.e. Andrew's
                // then do not capitalize the next letter
                if (x[0].ToString() == "'" && m.NextMatch().ToString().Length == 1)
                {
                    capitalizeNext = false;
                }

                // collect all text
                result += x;
            }

            if ((pAddPointAfterSingleLetter) && (result.Length == 1))
            {
                result = result + ".";
            }

            return result;
        }
        /// <summary>
        /// Parses the hexadecimal character.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Hex String{c}</exception>
        private static int ParseHexChar(this char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return c - '0';
            }

            if ((c >= 'A') && (c <= 'F'))
            {
                return c - 'A' + 10;
            }

            if ((c >= 'a') && (c <= 'f'))
            {
                return c - 'a' + 10;
            }

            throw new ArgumentException($"Invalid Hex String{c}");
        }
    }
}