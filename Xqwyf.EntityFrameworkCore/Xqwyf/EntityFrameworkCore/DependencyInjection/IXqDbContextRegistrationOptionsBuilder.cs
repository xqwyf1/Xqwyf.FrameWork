using System;
using JetBrains.Annotations;
using Xqwyf.DependencyInjection;
using Xqwyf.Domain.Entities;

namespace Xqwyf.EntityFrameworkCore.DependencyInjection
{
    public interface IXqDbContextRegistrationOptionsBuilder : IXqCommonDbContextRegistrationOptionsBuilder
    {
        /// <summary>
        /// 执行参数为<see cref="XqEntityOption{TEntity}"/>的<paramref name="optionsAction"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="optionsAction"></param>
        void Entity<TEntity>([NotNull] Action<XqEntityOption<TEntity>> optionsAction)
            where TEntity : IEntity;
    }
}
