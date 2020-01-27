using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.Modularity.PlugIns;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// <see cref="IModuleLoader"/>实现类，通过本对象，进行Module加载
    /// </summary>
    public class ModuleLoader : IModuleLoader
    {
        /// <summary>
        /// 根据<paramref name="startupModuleType"/>,遍历所有依赖模块，并进行加载
        /// </summary>
        /// <param name="services">系统服务</param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        /// <returns></returns>
        public IXqModuleDescriptor[] LoadModules( IServiceCollection services,Type startupModuleType,PlugInSourceList plugInSources)
        {
            XqCheck.NotNull(services, nameof(services));
            XqCheck.NotNull(startupModuleType, nameof(startupModuleType));
            XqCheck.NotNull(plugInSources, nameof(plugInSources));

            var moduledescs = GetDescriptors(services, startupModuleType, plugInSources);

            moduledescs = SortByDependency(moduledescs, startupModuleType);
            ConfigureServices(moduledescs, services);

            return moduledescs.ToArray();
        }

        protected virtual void SetDependencies(List<XqModuleDescriptor> modules)
        {
            foreach (var module in modules)
            {
                SetDependencies(modules, module);
            }
        }
        /// <summary>
        /// 记录<paramref name="module"/>的依赖
        /// </summary>
        /// <param name="moduledescs"></param>
        /// <param name="module"></param>
        protected virtual void SetDependencies(List<XqModuleDescriptor> moduledescs, XqModuleDescriptor module)
        {
            foreach (var dependedModuleType in XqModuleHelper.FindDependedModuleTypes(module.Type))
            {
                var dependedModule = moduledescs.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule == null)
                {
                    throw new XqException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }

        protected virtual List<IXqModuleDescriptor> SortByDependency(List<IXqModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        /// <summary>
        /// 获取所有Module的<see cref="IXqModuleDescriptor"/>，如果相关Module不存在，则创建
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        /// <returns></returns>
        private List<IXqModuleDescriptor> GetDescriptors(IServiceCollection services,Type startupModuleType,PlugInSourceList plugInSources)
        {
            var moduledescs = new List<XqModuleDescriptor>();

            FillModules(moduledescs, services, startupModuleType, plugInSources);
            SetDependencies(moduledescs);

            return moduledescs.Cast<IXqModuleDescriptor>().ToList();
        }

        protected virtual void FillModules(List<XqModuleDescriptor> moduledescs,IServiceCollection services,Type startupModuleType,PlugInSourceList plugInSources)
        {
            ///从startupModule,获取所有Module
            foreach (var moduleType in XqModuleHelper.FindAllModuleTypes(startupModuleType))
            {
                moduledescs.Add(CreateModuleDescriptor(services, moduleType));
            }

            //Plugin modules
            foreach (var moduleType in plugInSources.GetAllModules())
            {
                if (moduledescs.Any(m => m.Type == moduleType))
                {
                    continue;
                }

                moduledescs.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
            }
        }

        /// <summary>
        /// 创建某个<paramref name="moduleType"/>的<see cref="XqModuleDescriptor"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleType"></param>
        /// <param name="isLoadedAsPlugIn"></param>
        /// <returns></returns>
        protected virtual XqModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
        {
            return new XqModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
        }

        /// <summary>
        /// 根据<paramref name="moduleType"/>,创建<see cref="IXqModule"/>的单例，并放入<paramref name="services"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        protected virtual IXqModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IXqModule)Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }

        /// <summary>
        /// 对每个Module进行配置
        /// </summary>
        /// <param name="modules"><see cref="IXqModuleDescriptor"/>的列表</param>
        /// <param name="services">服务集合</param>
        protected virtual void ConfigureServices(List<IXqModuleDescriptor> modules, IServiceCollection services)
        {
            ///服务配置上下文，配置完成后，每个Module中ServiceConfigurationContext将被清空
            var context = new ServiceConfigurationContext(services);

            services.AddSingleton(context);

            foreach (var module in modules)
            {
                if (module.Instance is XqModule xqModule)
                {
                    xqModule.ServiceConfigurationContext = context;
                }
            }

            //预配置服务
            foreach (var module in modules.Where(m => m.Instance is IPreConfigureServices))
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }

            //配置服务
            foreach (var module in modules)
            {
                if (module.Instance is XqModule xqModule)
                {
                    //是否跳过服务的自动注册，默认为 false
                    if (!xqModule.SkipAutoServiceRegistration)
                    {
                        services.AddAssembly(module.Type.Assembly);
                    }
                }
                module.Instance.ConfigureServices(context);
            }

            //PostConfigureServices
            foreach (var module in modules.Where(m => m.Instance is IPostConfigureServices))
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }

            foreach (var module in modules)
            {
                if (module.Instance is XqModule xqModule)
                {
                    xqModule.ServiceConfigurationContext = null;
                }
            }
        }

    }
}
