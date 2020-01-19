using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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



        protected ILogger Logger => _lazyLogger.Value;
        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected DomainService()
        {
            GuidGenerator = SimpleGuidGenerator.Instance;
        }
    }
}
