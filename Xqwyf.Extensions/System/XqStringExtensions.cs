using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf;

namespace System
{
    public static class XqStringExtensions
    {
       /// <summary>
       /// 如果某个字符不在字符串末尾，那么在字符串后追加该字符
       /// </summary>
       /// <param name="str">字符串</param>
       /// <param name="c">被追加的字符</param>
       /// <param name="comparisonType">比较类型</param>
       /// <returns>处理完成的字符串</returns>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            XqCheck.NotNull(str, nameof(str));

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 如果某个字符不在字符串开始，在字符串开始追加该字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">被追加的字符</param>
        /// <param name="comparisonType">比较类型</param>
        /// <returns>处理完成的字符串</returns>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            XqCheck.NotNull(str, nameof(str));

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 判断字符串是否为Null或者无内容
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为Null或者无内容或者为空格
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///从开始位置获取一个字符串
        /// </summary>
        /// <exception cref="ArgumentNullException">如果 <paramref name="str"/>为空</exception>
        /// <exception cref="ArgumentException">如果<paramref name="len"/> 大于字符串长度</exception>
        public static string Left(this string str, int len)
        {
            XqCheck.NotNull(str, nameof(str));

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }


        /// <summary>
        /// 将字符创中行结束符转换为 <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }


        /// <summary>
        /// 从结尾开始截取长度为<paramref name="len"/>的字符串
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
        public static string Right(this string str, int len)
        {
            XqCheck.NotNull(str, nameof(str));

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// 如果超过最大长度，则从字符串的开头获取字符串的子字符串。如果字符串被截断，它将在字符串末尾添加"..."
        /// 返回的字符串不能超过maxLength。
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// 如果超过最大长度，则从字符串的开头获取字符串的子字符串。如果字符串被截断，它将在字符串末尾添加给定的后缀。
        /// 返回的字符串不能超过maxLength。
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }


    }
}
