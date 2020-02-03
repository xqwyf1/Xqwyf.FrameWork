using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Xqwyf
{
    /// <summary>
    /// 提供用于 Dipose 被调用时，所使用的方法
    /// This class can be used to provide an action when
    /// Dipose method is called.
    /// </summary>
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// 创建<see cref="DisposeAction"/>对象，当这个对象被disposed时，执行<paramref name="action"/>
        /// </summary>
        public DisposeAction([NotNull] Action action)
        {
            XqCheck.NotNull(action, nameof(action));

            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
