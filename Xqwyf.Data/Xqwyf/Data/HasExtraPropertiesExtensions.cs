using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xqwyf;

namespace Xqwyf.Data
{
    /// <summary>
    /// <see cref="IHasExtraProperties"/>的扩展方法
    /// </summary>
    public static class HasExtraPropertiesExtensions
    {
        /// <summary>
        /// 判断扩展属性中是否有名称为<paramref name="name"/>的属性
        /// </summary>
        /// <param name="source">IHasExtraProperties对象</param>
        /// <param name="name">查找属性的名称</param>
        /// <returns></returns>
        public static bool HasProperty(this IHasExtraProperties source, string name)
        {
            return source.ExtraProperties.ContainsKey(name);
        }

        /// <summary>
        /// 从扩展属性中获取名称为<paramref name="name"/>的属性值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetProperty(this IHasExtraProperties source, string name)
        {
            return source.ExtraProperties?.GetOrDefault(name);
        }

        /// <summary>
        /// 从扩展属性中获取名称为<paramref name="name"/>并且类型为<typeparamref name="TProperty"/>的属性值
        /// </summary>
        /// <typeparam name="TProperty">值的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TProperty GetProperty<TProperty>(this IHasExtraProperties source, string name)
        {
            var value = source.GetProperty(name);
            if (value == default)
            {
                return default;
            }

            if (typeof(TProperty).IsPrimitiveExtended( includeEnums: true))
            {
                return (TProperty)Convert.ChangeType(value, typeof(TProperty), CultureInfo.InvariantCulture);
            }

            throw new XqException("GetProperty<TProperty> does not support non-primitive types. Use non-generic GetProperty method and handle type casting manually.");
        }


        /// <summary>
        /// 设置扩展属性
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="name">被设置的属性键</param>
        /// <param name="value">被设置的属性值</param>
        /// <returns></returns>
        public static TSource SetProperty<TSource>(this TSource source, string name, object value)
            where TSource : IHasExtraProperties
        {
            source.ExtraProperties[name] = value;
            return source;
        }

        /// <summary>
        /// 从扩展属性值移掉一个属性
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="name">被移除的属性键</param>
        /// <returns></returns>

        public static TSource RemoveProperty<TSource>(this TSource source, string name)
            where TSource : IHasExtraProperties
        {
            source.ExtraProperties.Remove(name);
            return source;
        }
    }
}
