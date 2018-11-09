using System;
using System.Globalization;
using System.Text;

namespace Dtc.Common.Extensions
{
    public static class StringExtensions
    {
        public static string EncodeToBase64(this string value)
        {
            var toEncodeAsBytes = Encoding.ASCII.GetBytes(value);
            var base64Str = Convert.ToBase64String(toEncodeAsBytes);
            return base64Str;
        }

        static public string DecodeFromBase64(this string value)
        {
            var encodedDataAsBytes = Convert.FromBase64String(value);
            string returnValue = Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string ToHexString(this string value)
        {
            var toEncodeAsBytes = Encoding.ASCII.GetBytes(value);

            var s = new StringBuilder();
            foreach (byte b in toEncodeAsBytes)
            {
                s.Append(b.ToString("x2"));
            }
            var result = s.ToString();
            return result;
        }

        public static string ToHexString2(this string value)
        {
            var result = string.Empty;
            foreach(var ch in value)
            {
                var i = (int)ch;
                result += i.ToString("X10");
            }
            return result;
        }


        public static string FromHexBytes(this string hex)
        {
            int l = hex.Length / 2;
            var b = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            string result = Encoding.ASCII.GetString(b);
            return result;
        }


        public static string FromHexBytes2(this string hex)
        {
            var pos = 0;
            var hexLen = 10;
            var result = string.Empty;
            while (hex.Length >= (pos + hexLen))
            {
                var kus = hex.Substring(pos, hexLen);
                int value = Convert.ToInt32(kus, 16);
                var ch = (char)value;
                result += ch;
                pos += hexLen;
            }
            return result;
        }


        /// <summary>
        /// remove text at beginning of string
        /// </summary>
        public static string RemoveStartText(this string value, string startWith, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if ((value != null) && !string.IsNullOrEmpty(startWith))
            {
                if (value.StartsWith(startWith, comparisonType))
                {
                    value = value.Substring(startWith.Length, value.Length - startWith.Length);
                }
            }
            return value;
        }

        public static string RemoveStartText(this string value, char startWith, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            return RemoveStartText(value, startWith.ToString(), comparisonType);
        }

        /// <summary>
        /// remove text at end of string
        /// </summary>
        public static string RemoveEndText(this string value, string endWith, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if ((value != null) && !string.IsNullOrEmpty(endWith))
            {
                if (value.EndsWith(endWith, comparisonType))
                {
                    value = value.Substring(0, value.Length - endWith.Length);
                }
            }
            return value;
        }

        public static string RemoveEndText(this string value, char endWith, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            return RemoveEndText(value, endWith.ToString(), comparisonType);
        }

        /// <summary>
        /// remove end text to specified char
        /// </summary>
        public static string RemoveEndTextTo(this string value, char endWith)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var index = value.LastIndexOf(endWith);
                if (index > -1)
                    return value.Substring(0, index);
            }
            return value;
        }

        /// <summary>
        /// remove start text to specified char
        /// </summary>
        public static string RemoveStartTextTo(this string value, char endWith)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var index = value.IndexOf(endWith);
                if (index > -1)
                    return value.Substring(++index);
            }
            return value;
        }

        public static string TrimToMaxLen(this string value, int maxLen, string endString = null)
        {
            if (!string.IsNullOrEmpty(value) && (value.Length > maxLen))
            {
                maxLen = string.IsNullOrEmpty(endString) ? maxLen : (maxLen - endString.Length);
                if (maxLen <= 0)
                {
                    value = string.IsNullOrEmpty(endString) ? string.Empty : endString;
                }
                else
                {
                    value = value.Remove(maxLen);
                    if (!string.IsNullOrEmpty(endString))
                    {
                        value += endString;
                    }
                }
            }
            return value;
        }





        /// <summary>
        /// get substring to first occurence of char ch
        /// </summary>
        /// <param name="value">string instance</param>
        /// <param name="ch">ending char</param>
        /// <returns>string</returns>
        public static string SusbstrToChar(this string value, char ch, string notFoundValue = null)
        {
            if (value != null)
            {
                var chIndex = value.IndexOf(ch);
                if (chIndex >= 0)
                {
                    return value.Substring(0, chIndex);
                }
            }
            return notFoundValue;
        }


        public static string SusbstrFromChar(this string value, char ch, string notFoundValue = null)
        {
            if (value != null)
            {
                int chIndex = value.IndexOf(ch);
                if ((chIndex >= 0) && ((chIndex + 1) < value.Length))
                {
                    return value.Substring(chIndex + 1, value.Length - chIndex - 1);
                }
            }
            return notFoundValue;
        }


        /// <summary>
        /// Split string to half
        /// </summary>
        /// <param name="s">source string</param>
        /// <param name="separator">separator char</param>
        /// <returns>Tuple result</returns>
        public static Tuple<string, string> Split2Half(this string s, char separator)
        {
            if (s != null)
            {
                return new Tuple<string, string>(s.SusbstrToChar(separator, s), s.SusbstrFromChar(separator, string.Empty));
            }
            return new Tuple<string, string>(null, null);
        }


        /// <summary>
        /// Lefts the specified given number.
        /// </summary>
        public static string Left(this string s, int count)
        {
            if ((count > 0) & (s != null))
            {
                return (count >= s.Length) ? s : s.Substring(0, count);
            }
            return string.Empty;
        }


        /// <summary>
        /// Right givenNumber characters of given string.
        /// </summary>
        public static string Right(this string s, int count)
        {
            if ((count > 0) && (s != null))
            {
                return (count >= s.Length) ? s : s.Substring(s.Length - count);
            }
            return string.Empty;
        }

        public static DateTime? SafeConvertToDateTime(this string s, string dateTimeFormat = "d.M.yyyy")
        {
            try
            {
                // https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
                return DateTime.ParseExact(s, dateTimeFormat, CultureInfo.InvariantCulture); 
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static bool StartsWith(this string s, char ch)
        {
            return !string.IsNullOrEmpty(s) && s.StartsWith(ch.ToString());
        }

        public static bool EndsWith(this string s, char ch)
        {
            return !string.IsNullOrEmpty(s) && s.EndsWith(ch.ToString());
        }

    }
}
