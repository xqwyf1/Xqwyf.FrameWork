using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JetBrains.Annotations;

using Xqwyf.Domain.Entities;

namespace Xqwyf.Domain.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 创建某个<see cref="IAggregateRoot"/>的XqEntityOption
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class XqEntityOptions<TAggregateRoot>
          where TAggregateRoot : IAggregateRoot
    {
        public static XqEntityOptions<TAggregateRoot> Empty { get; } = new XqEntityOptions<TAggregateRoot>();

        public Func<IQueryable<TAggregateRoot>, IQueryable<TAggregateRoot>> DefaultWithDetailsFunc { get; set; }
    }

    /// <summary>
    /// 所有<see cref="IAggregateRoot"/>的XqEntityOptions
    /// </summary>
    public class XqEntityOptions
    {
        private readonly IDictionary<Type, object> _options;

        public XqEntityOptions()
        {
            _options = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 获取某个<see cref="IAggregateRoot"/>的XqEntityOption
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <returns></returns>
        public XqEntityOptions<TAggregateRoot> GetOrNull<TAggregateRoot>()
            where TAggregateRoot : IAggregateRoot
        {
            return _options.GetOrDefault(typeof(TAggregateRoot)) as XqEntityOptions<TAggregateRoot>;
        }

        public void Entity<TAggregateRoot>([NotNull] Action<XqEntityOptions<TAggregateRoot>> optionsAction)
            where TAggregateRoot : IAggregateRoot
        {
            XqCheck.NotNull(optionsAction, nameof(optionsAction));

            optionsAction(
                _options.GetOrAdd(
                    typeof(TAggregateRoot),
                    () => new XqEntityOptions<TAggregateRoot>()
                ) as XqEntityOptions<TAggregateRoot>
            );
        }
    }
}
