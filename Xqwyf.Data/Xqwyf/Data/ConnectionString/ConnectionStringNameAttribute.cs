using System;
using System.Reflection;
using JetBrains.Annotations;

namespace  Xqwyf.Data
{
    public class ConnectionStringNameAttribute : Attribute
    {
        /// <summary>
        /// 连接串名称
        /// </summary>
        [NotNull]
        public string Name { get; }


        public ConnectionStringNameAttribute([NotNull] string name)
        {
            XqCheck.NotNull(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// 获取<typeparamref name="T"/>中<see cref="ConnectionStringNameAttribute"/>值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetConnStringName<T>()
        {
            return GetConnStringName(typeof(T));
        }

        /// <summary>
        /// 获取<paramref name="type"/>中<see cref="ConnectionStringNameAttribute"/>值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConnStringName(Type type)
        {
            var nameAttribute = type.GetTypeInfo().GetCustomAttribute<ConnectionStringNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.Name;
        }
    }
}
