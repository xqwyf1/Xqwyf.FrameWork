using JetBrains.Annotations;

namespace  Xqwyf.DependencyInjection
{
    /// <summary>
    /// 对方访问器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectAccessor<out T>
    {
        [CanBeNull]
        T Value { get; }
    }
}
