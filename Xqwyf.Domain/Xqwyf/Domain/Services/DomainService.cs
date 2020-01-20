using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Xqwyf.Guids;
using Xqwyf.Timing;

namespace Xqwyf.Domain.Services
{
    /// <summary>
    /// 领域服务基础类
    /// </summary>
    public abstract class DomainService : IDomainService
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new object();

        /// <summary>
        /// 获取指定的Service
        /// </summary>
        /// <typeparam name="TService">将被获取的service类型</typeparam>
        /// <param name="reference">服务存储</param>
        /// <returns></returns>
        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return reference;
        }

        public IClock Clock => LazyGetRequiredService(ref _clock);
        private IClock _clock;

        public IGuidGenerator GuidGenerator { get; set; }

        public ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
        private ILoggerFactory _loggerFactory;



        protected ILogger Logger => LazyLogger.Value;
        private Lazy<ILogger> LazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected DomainService()
        {
            GuidGenerator = SimpleGuidGenerator.Instance;
        }
    }
}
