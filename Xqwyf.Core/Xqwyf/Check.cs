using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Xqwyf
{
    [DebuggerStepThrough]
    public static class Check
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
    }
}
