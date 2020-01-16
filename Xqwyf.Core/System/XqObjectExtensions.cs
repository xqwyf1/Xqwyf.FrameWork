using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// <see cref="Object"/>的扩展方法
    /// </summary>
    public static class XqObjectExtensions
    {
        /// <summary>
        /// 用于将对象投射到类型。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">被投射的对象</param>
        /// <returns>结果类型</returns>
        public static T As<T>(this object obj)
           where T : class
        {
            return (T)obj;
        }
    }
}
