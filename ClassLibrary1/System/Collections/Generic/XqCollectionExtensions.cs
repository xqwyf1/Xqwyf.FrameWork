using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;
using Xqwyf;

namespace System.Collections.Generic
{
    /// <summary>
    /// ICollection<T>的扩展类
    /// </summary>
    public static class XqCollectionExtensions
    {
        /// <summary>
        /// 检查<paramref name="source"/>是否为空或者没有数据
        /// </summary>
        /// <typeparam name="T">集合中的类型</typeparam>
        /// <param name="source">当前ICollection<T></param>
        /// <returns>True：为空或者数据量==0；False：不为空并且数据项数据量大于0</returns>
        public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
        /// <summary>
        /// 如果在<<paramref name="source"/>不存在，则增加<paramref name="item"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            XqCheck.NotNull(source, nameof(source));

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}
