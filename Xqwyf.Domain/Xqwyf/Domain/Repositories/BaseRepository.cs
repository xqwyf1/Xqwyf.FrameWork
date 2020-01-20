using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Xqwyf.Domain.Entities;

namespace Xqwyf.Domain.Repositories
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
   public abstract  class BaseRepository<TAggregateRoot> : ReadOnlyRepository<TAggregateRoot>, IBasicRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        public abstract Task<TAggregateRoot> InsertAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);

        public virtual async Task DeleteAsync(object id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken).ConfigureAwait(false);
        }
    }
}
