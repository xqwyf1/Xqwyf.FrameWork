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

        /// <summary>
        /// 从字典中获取给定键的值，如没有，通过<paramref name="factory"/>创建值，并返回值
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }

        /// <summary>
        /// 从字典中获取给定键的值，如没有，通过<paramref name="factory"/>创建值，并返回值
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }


    }
}
