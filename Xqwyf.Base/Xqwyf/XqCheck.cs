using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Xqwyf
{
    [DebuggerStepThrough]
    public static class XqCheck
    {
        /// <summary>
        /// 非空检查，如果为空，系统停止（"value:null => halt"），如果不为空，则返回被检查对象
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <param name="value">检查值</param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value,[InvokerParameterName] [NotNull] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        /// <summary>
        /// 非空检查，如果为空，系统停止（"value:null => halt"）
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <param name="value">检查值</param>
        /// <param name="parameterName"></param>
        /// <returns></returns>

        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName] [NotNull] string parameterName,string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        /// <summary>
        /// 判断字符串是否为空，是否全部为空格，长度是否超出最大限制，最小限制
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <param name="maxLength"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public static string NotNullOrWhiteSpace(
          string value,
          [InvokerParameterName] [NotNull] string parameterName,
          int maxLength = int.MaxValue,
          int minLength = 0)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
            }

            return value;
        }
        /// <summary>
        /// 判断字符串是否为null或者全部为空格
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为null或者全部为空格
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
