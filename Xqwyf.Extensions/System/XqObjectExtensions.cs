using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

using System.Reflection;

namespace System
{
    /// <summary>
    /// <see cref="Object"/>的扩展方法
    /// </summary>
    public static class XqObjectExtensions
    {
        /// <summary>
        /// 用于将对象<paramref name="obj"/>反射到<typeparamref name="T"/>类型的对象。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">被投射的对象</param>
        /// <returns>结果类型</returns>
        public static T As<T>(this object obj)
           where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// 判断某个对象是否为Func
        /// </summary>
        /// <param name="obj">被判断的对象</param>
        /// <returns>是：true；不是：false</returns>
        public static bool IsFunc(this object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }


        /// <summary>
        /// 判断某个对象是否为<see cref="Func{TReturn}"/>
        /// </summary>
        /// <typeparam name="TReturn">Func的泛型</typeparam>
        /// <param name="obj">被判断的对象</param>
        /// <returns>是：true；不是：false</returns>
        public static bool IsFunc<TReturn>(this object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        /// <summary>
        ///  使用 <see cref="Convert.ChangeType(object,System.Type)"/> 方法，转换当前对象为<typeparamref name="T"/>
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 检查<paramref name="item"/>是否在<paramref name="list"/>
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }

}
