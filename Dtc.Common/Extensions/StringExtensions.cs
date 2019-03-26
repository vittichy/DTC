using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;

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
        public static string SubstrTo(this string value, string toStr)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var chIndex = value.IndexOf(toStr);
            return (chIndex > -1) ? value.Substring(0, chIndex) : value;
        }


        public static string SubstrTo(this string value, char toChar)
        {
            return SubstrTo(value, toChar.ToString());
        }


        /// <summary>
        /// get substring to first occurence of char ch
        /// </summary>
        /// <param name="value">string instance</param>
        /// <param name="ch">ending char</param>
        /// <returns>string</returns>
        public static string SubstrToChars(this string value, params char[] chrs)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            
            var existingChar = new List<char>(chrs).FirstOrDefault(p => value.Contains(p));
            return value.SubstrTo(existingChar);
        }
        

        public static string SubstrFrom(this string value, string fromStr)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(fromStr))
                return string.Empty;
            
            int chIndex = value.IndexOf(fromStr);
            if (chIndex >= 0)
                return value.Substring(chIndex + fromStr.Length);

            return string.Empty;
        }


        public static string SubstrFrom(this string value, char fromCh)
        {
            return SubstrFrom(value, fromCh.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string SubstrFromToChar(this string value, char start, char end)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var startIndex = Math.Max(value.IndexOf(start), 0);
                var endIndex = value.IndexOf(end);
                if (endIndex < 0)
                    endIndex = value.Length - 1;

                return value.Substring(startIndex, (endIndex - startIndex) + 1);
            }
            return null;
        }


        /// <summary>
        /// Split string to half
        /// 
        /// "ABCDE" + C separator => "AB"   + "DE"
        /// "ABCDE" + A separator => Empty  + "BCDE"
        /// "ABCDE" + E separator => "ABCD" + Empty
        /// 
        /// </summary>
        /// <param name="value">source string</param>
        /// <param name="separator">separator char</param>
        /// <returns>Tuple result</returns>
        public static Tuple<string, string> Split2Half(this string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return new Tuple<string, string>(string.Empty, string.Empty);

            var separatorExists = value.Contains(separator);
            return separatorExists
                    ? new Tuple<string, string>(value.SubstrTo(separator), value.SubstrFrom(separator))   // splitted strings
                    : new Tuple<string, string>(value, string.Empty);                                       // separator not found                    
        }


        public static Tuple<string, string> Split2Half(this string value, char separator)
        {
            return Split2Half(value, separator.ToString());
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
               

        public static string ExtendToLength(this string s, int extendToLength, char extendChar)
        {
            var addCount = extendToLength - (s ?? string.Empty).Length;
            return (addCount > 0) ? s + new string(extendChar, addCount) : s;
        }


    }
}
