using System;
using JetBrains.Annotations;

namespace Xqwyf.DependencyInjection
{
    /// <summary>
    /// 对象访问器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T Value { get; set; }

        public ObjectAccessor()
        {

        }

        public ObjectAccessor([CanBeNull] T obj)
        {
            Value = obj;
        }
    }
}
