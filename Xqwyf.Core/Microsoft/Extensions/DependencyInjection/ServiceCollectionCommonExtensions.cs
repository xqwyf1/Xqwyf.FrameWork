using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;
using Xqwyf;

namespace  Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionCommonExtensions
	{
		
		public static IServiceProvider BuildServiceProviderFromFactory([NotNull] this IServiceCollection services)
		{
			XqCheck.NotNull(services, nameof(services));

			foreach (var service in services)
			{
				var factoryInterface = service.ImplementationInstance?.GetType()
					.GetTypeInfo()
					.GetInterfaces()
					.FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
										 i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

				if (factoryInterface == null)
				{
					continue;
				}

				var containerBuilderType = factoryInterface.GenericTypeArguments[0];
				return (IServiceProvider)typeof(ServiceCollectionCommonExtensions)
					.GetTypeInfo()
					.GetMethods()
					.Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
					.MakeGenericMethod(containerBuilderType)
					.Invoke(null, new object[] { services, null });
			}

			return services.BuildServiceProvider();
		}
	}
}
