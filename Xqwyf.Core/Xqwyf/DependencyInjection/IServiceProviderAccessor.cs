using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.DependencyInjection
{
    /// <summary>
    /// IServiceProvider的访问器
    /// </summary>
    public interface IServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; }
    }
}
