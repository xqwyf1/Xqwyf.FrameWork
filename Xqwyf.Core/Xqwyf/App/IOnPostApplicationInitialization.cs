using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Xqwyf.App
{
    /// <summary>
    /// ?
    /// </summary>
    public interface IOnPostApplicationInitialization
    {
        void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context);
    }
}
