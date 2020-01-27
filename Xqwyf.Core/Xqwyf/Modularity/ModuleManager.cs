using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xqwyf.DependencyInjection;
using Xqwyf.App;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// <see cref="IModuleManager"/>实现类，
    /// </summary>
    public class ModuleManager : IModuleManager
    {
        private readonly IModuleContainer _moduleContainer;
        private readonly IEnumerable<IModuleLifecycleContributor> _lifecycleContributors;
        private readonly ILogger<ModuleManager> _logger;

        public ModuleManager(
            IModuleContainer moduleContainer,
            ILogger<ModuleManager> logger,
            IOptions<XqModuleLifecycleOptions> options,
            IServiceProvider serviceProvider)
        {
            _moduleContainer = moduleContainer;
            _logger = logger;

            _lifecycleContributors = options.Value
                .Contributors
                .Select(serviceProvider.GetRequiredService)
                .Cast<IModuleLifecycleContributor>()
                .ToArray();
        }

        public void InitializeModules(ApplicationInitializationContext context)
        {
            LogListOfModules();

            foreach (var Contributor in _lifecycleContributors)
            {
                foreach (var module in _moduleContainer.Modules)
                {
                    Contributor.Initialize(context, module.Instance);
                }
            }

            _logger.LogInformation("Initialized all ABP modules.");
        }

        /// <summary>
        /// Module加载时，显示的Log记录信息
        /// </summary>
        private void LogListOfModules()
        {
            _logger.LogInformation("Loaded Xq modules:");

            foreach (var module in _moduleContainer.Modules)
            {
                _logger.LogInformation("- " + module.Type.FullName);
            }
        }

        /// <summary>
        /// 应用关闭时逐个进行<see cref="XqModule"/>注销，关闭时，需要按照依赖关系，反向注销
        /// </summary>
        /// <param name="context">应用关闭时的上下文</param>
        public void ShutdownModules(ApplicationShutdownContext context)
        {
            var modules = _moduleContainer.Modules.Reverse().ToList();

            foreach (var Contributor in _lifecycleContributors)
            {
                foreach (var module in modules)
                {
                    Contributor.Shutdown(context, module.Instance);
                }
            }
        }
    }
}
