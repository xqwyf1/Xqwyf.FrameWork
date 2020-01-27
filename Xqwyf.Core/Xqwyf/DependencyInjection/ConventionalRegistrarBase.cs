using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.Reflection;

namespace  Xqwyf.DependencyInjection
{
    public abstract class ConventionalRegistrarBase : IConventionalRegistrar
    {
        /// <summary>
        /// 添加<paramref name="assembly"/>中所有的组件，包括<see cref="IScopedDependency"/>、<see cref="ISingletonDependency"/>和<see cref="ITransientDependency"/>
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="assembly">程序集</param>
        public virtual void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = AssemblyHelper
                .GetAllTypes(assembly)
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType
                ).ToArray();

            AddTypes(services, types);
        }

        /// <summary>
        ///  添加<paramref name="types"/>中所有的组件，包括<see cref="IScopedDependency"/>、<see cref="ISingletonDependency"/>和<see cref="ITransientDependency"/>
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="types">类型数组</param>
        public virtual void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(services, type);
            }
        }
        /// <summary>
        ///  添加<paramref name="type"/>组件，包括<see cref="IScopedDependency"/>、<see cref="ISingletonDependency"/>和<see cref="ITransientDependency"/>
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        public abstract void AddType(IServiceCollection services, Type type);

        /// <summary>
        /// 返回<paramref name="type"/>的<see cref="DisableConventionalRegistrationAttribute"/>，如果没有
        /// <see cref="DisableConventionalRegistrationAttribute"/>，返回False;如果有，返回True;
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        protected virtual void TriggerServiceExposing(IServiceCollection services, Type implementationType, List<Type> serviceTypes)
        {
            var exposeActions = services.GetExposingActionList();
            if (exposeActions.Any())
            {
                var args = new OnServiceExposingContext(implementationType, serviceTypes);
                foreach (var action in exposeActions)
                {
                    action(args);
                }
            }
        }
    }
}
