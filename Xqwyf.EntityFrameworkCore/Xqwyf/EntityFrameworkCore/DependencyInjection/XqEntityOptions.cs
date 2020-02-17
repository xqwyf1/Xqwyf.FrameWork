using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Xqwyf.Domain.Entities;


namespace Xqwyf.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 某个实体的配置选项
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class XqEntityOption<TEntity>
         where TEntity : IEntity
    {
        /// <summary>
        /// <typeparamref name="TEntity"/>的空XqEntityOptions
        /// </summary>
        public static XqEntityOption<TEntity> Empty { get; } = new XqEntityOption<TEntity>();

        /// <summary>
        /// 获取或者设置该实体的获取详细信息的方法
        /// </summary>
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> DefaultWithDetailsFunc { get; set; }
    }

    /// <summary>
    /// 所有实体的配置选项
    /// </summary>
    public class XqEntityOptions
    {
        private readonly IDictionary<Type, object> _options;

        public XqEntityOptions()
        {
            _options = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 获取类型为<typeparamref name="TEntity"/>的实体的<see cref="XqEntityOption{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public XqEntityOption<TEntity> GetOrNull<TEntity>()
            where TEntity : IEntity
        {
            return _options.GetOrDefault(typeof(TEntity)) as XqEntityOption<TEntity>;
        }

        /// <summary>
        /// 执行参数为<see cref="XqEntityOption{TEntity}"/>的<paramref name="optionsAction"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="optionsAction"></param>
        public void Entity<TEntity>([NotNull] Action<XqEntityOption<TEntity>> optionsAction)
            where TEntity : IEntity
        {
            XqCheck.NotNull(optionsAction, nameof(optionsAction));

            optionsAction(
                _options.GetOrAdd(
                    typeof(TEntity),
                    () => new XqEntityOption<TEntity>()
                ) as XqEntityOption<TEntity>
            );
        }
    }
}
