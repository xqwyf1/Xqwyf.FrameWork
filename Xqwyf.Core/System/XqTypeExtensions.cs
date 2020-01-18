using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using JetBrains.Annotations;

namespace System
{
    public static   class XqTypeExtensions
    {
        /// <summary>
        /// 获取一个类型名称，包括所在的程序集
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }


        /// <summary>
        /// 确定是否可以将当前类型的实例分配给<typeparamref name="TTarget"></typeparamref>实例。
        /// </summary>
        /// <typeparam name="TTarget">目标类型</typeparam> (as reverse).
        public static bool IsAssignableTo<TTarget>(this Type type)
        {
            return type.IsAssignableTo(typeof(TTarget));
        }

        /// <summary>
        /// 确定是否可以将当前类型的实例分配给 <paramref name="targetType"></paramref>.实例。
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public static bool IsAssignableTo(this Type type, Type targetType)
        {
            return targetType.IsAssignableFrom(type);
        }


        /// <summary>
        /// 获取当前类型所有的基础类
        /// </summary>
        /// <param name="type">准备获取基类的类型</param>
        /// <param name="includeObject">True, 在返回数组中包括 <see cref="object"/>类型</param>
        public static Type[] GetBaseClasses(this Type type, bool includeObject = true)
        {
            var types = new List<Type>();
            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
            return types.ToArray();
        }

        /// <summary>
        /// 循环获取某个类型的基类
        /// </summary>
        /// <param name="types"></param>
        /// <param name="type"></param>
        /// <param name="includeObject"></param>
        private static void AddTypeAndBaseTypesRecursively(
          [NotNull] List<Type> types,
          [CanBeNull] Type type,
          bool includeObject)
        {
            if (type == null)
            {
                return;
            }

            if (!includeObject && type == typeof(object))
            {
                return;
            }

            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
            types.Add(type);
        }

        public static bool IsPrimitiveExtended(this Type type, bool includeNullables = true, bool includeEnums = false)
        {
            if (IsPrimitiveExtendedInternal(type, includeEnums))
            {
                return true;
            }

            if (includeNullables &&
                type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtendedInternal(type.GenericTypeArguments[0], includeEnums);
            }

            return false;
        }

        public static Type GetFirstGenericArgumentIfNullable(this Type t)
        {
            if (t.GetGenericArguments().Length > 0 && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return t.GetGenericArguments().FirstOrDefault();
            }

            return t;
        }

        private static bool IsPrimitiveExtendedInternal(Type type, bool includeEnums)
        {
            if (type.IsPrimitive)
            {
                return true;
            }

            if (includeEnums && type.IsEnum)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }
    }
}
