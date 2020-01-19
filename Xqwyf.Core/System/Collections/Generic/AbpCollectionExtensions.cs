using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace System.Collections.Generic
{
    /// <summary>
    /// ICollection<T>的扩展类
    /// </summary>
    public static class AbpCollectionExtensions
    {
        /// <summary>
        /// 检查当前ICollection<T>是否为空或者没有数据
        /// </summary>
        /// <typeparam name="T">集合中的类型</typeparam>
        /// <param name="source">当前ICollection<T></param>
        /// <returns>True：为空或者数据量==0；False：不为空并且数据项数据量大于0</returns>
        public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            Check.NotNull(source, nameof(source));

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}
