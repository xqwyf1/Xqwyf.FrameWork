using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class XqDictionaryExtensions
    {
        /// <summary>
        ///从字典中获取给定键的值，如没有，返回给定值类型的默认值
        /// </summary>
        /// <param name="dictionary">从中获取值的字典</param>
        /// <param name="key">查找键</param>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <returns>查找到，返回值，没有找到，返回值类型的默认值</returns>
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default;
        }

        /// <summary>
        /// 从字典中获取给定键的值，如没有，返回给定值类型的默认值
        /// </summary>
        /// <param name="dictionary">从中获取值的字典</param>
        /// <param name="key">查找键</param>
        /// <typeparam name="TKey">T键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <returns>查找到，返回值，没有找到，返回值类型的默认值</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var obj) ? obj : default;
        }
    }
}
