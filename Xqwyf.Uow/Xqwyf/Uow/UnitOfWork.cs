using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xqwyf.DependencyInjection;


namespace Xqwyf.Uow
{
    public class UnitOfWork : IUnitOfWork, ITransientDependency
    {

        #region IUnitOfWork实现

        public Guid Id { get; } = Guid.NewGuid();

     


        /// <summary>
        /// 注销事件
        /// </summary>
        public event EventHandler<UnitOfWorkEventArgs> Disposed;

        public IXqUnitOfWorkOptions Options { get; private set; }

        public IUnitOfWork Outer { get; private set; }

        public bool IsReserved { get; set; }

        public bool IsCompleted { get; private set; }

        public bool IsDisposed { get; private set; }

        public string ReservationName { get; set; }

        public IServiceProvider ServiceProvider { get; }

        protected List<Func<Task>> CompletedHandlers { get; } = new List<Func<Task>>();

        private Exception _exception;
        private bool _isCompleting;
        private bool _isRolledback;

        /// <summary>
        /// 执行所有数据库的SaveChanges
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var databaseApi in GetAllActiveDatabaseApis())
            {
                if (databaseApi is ISupportsSavingChanges)
                {
                    await (databaseApi as ISupportsSavingChanges).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_isRolledback)
            {
                return;
            }

            _isRolledback = true;

            await RollbackAllAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 执行所有的Rollback
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task RollbackAllAsync(CancellationToken cancellationToken)
        {
            foreach (var databaseApi in GetAllActiveDatabaseApis())
            {
                if (databaseApi is ISupportsRollback)
                {
                    try
                    {
                        await (databaseApi as ISupportsRollback).RollbackAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch { }
                }
            }

            foreach (var transactionApi in GetAllActiveTransactionApis())
            {
                if (transactionApi is ISupportsRollback)
                {
                    try
                    {
                        await (transactionApi as ISupportsRollback).RollbackAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch { }
                }
            }
        }


        public virtual void SetOuter(IUnitOfWork outer)
        {
            Outer = outer;
        }

        public virtual async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            if (_isRolledback)
            {
                return;
            }

            PreventMultipleComplete();

            try
            {
                _isCompleting = true;
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await CommitTransactionsAsync().ConfigureAwait(false);
                IsCompleted = true;
                await OnCompletedAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 处理完成事务时，如果已经处于完成状态，将抛出异常
        /// </summary>
        private void PreventMultipleComplete()
        {
            if (IsCompleted || _isCompleting)
            {
                throw new XqException("Complete is called before!");
            }
        }


        public virtual void Reserve(string reservationName)
        {
            XqCheck.NotNull(reservationName, nameof(reservationName));

            ReservationName = reservationName;
            IsReserved = true;
        }
        /// <summary>
        /// 获取所有当前的数据库
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IDatabaseApi> GetAllActiveDatabaseApis()
        {
            return _databaseApis.Values.ToImmutableList();
        }
        public void OnCompleted(Func<Task> handler)
        {
            CompletedHandlers.Add(handler);
        }

        protected virtual async Task OnCompletedAsync()
        {
            foreach (var handler in CompletedHandlers)
            {
                await handler.Invoke().ConfigureAwait(false);
            }
        }

        #endregion

        private readonly XqUnitOfWorkDefaultOptions _defaultOptions;

        /// <summary>
        /// 通过<paramref name="serviceProvider"/>和<paramref name="options"/>创建一个<see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        public UnitOfWork(IServiceProvider serviceProvider, IOptions<XqUnitOfWorkDefaultOptions> options)
        {
            ServiceProvider = serviceProvider;
            _defaultOptions = options.Value;

            _databaseApis = new Dictionary<string, IDatabaseApi>();
            _transactionApis = new Dictionary<string, ITransactionApi>();
        }

        public  bool IsReservedFor( string reservationName)
        {
            return this.IsReserved && this.ReservationName == reservationName;
        }

        /// <summary>
        /// 通过<paramref name="options"/>进行<see cref="IUnitOfWork"/>初始化
        /// </summary>
        /// <param name="options"></param>
        public virtual void Initialize(XqUnitOfWorkOptions options)
        {
            XqCheck.NotNull(options, nameof(options));

            if (Options != null)
            {
                throw new XqException("This unit of work is already initialized before!");
            }

            Options = _defaultOptions.Normalize(options.Clone());
            IsReserved = false;
        }

        #region ITransactionApiContainer实现

        /// <summary>
        /// 事务字典
        /// </summary>
        private readonly Dictionary<string, ITransactionApi> _transactionApis;

        public ITransactionApi FindTransactionApi(string key)
        {
            XqCheck.NotNull(key, nameof(key));

            return _transactionApis.GetOrDefault(key);
        }

        public ITransactionApi GetOrAddTransactionApi(string key, Func<ITransactionApi> factory)
        {
            XqCheck.NotNull(key, nameof(key));
            XqCheck.NotNull(factory, nameof(factory));

            return _transactionApis.GetOrAdd(key, factory);
        }

        public void AddTransactionApi(string key, ITransactionApi api)
        {
            XqCheck.NotNull(key, nameof(key));
            XqCheck.NotNull(api, nameof(api));

            if (_transactionApis.ContainsKey(key))
            {
                throw new XqException("There is already a transaction API in this unit of work with given key: " + key);
            }

            _transactionApis.Add(key, api);
        }

        #endregion

        #region IDatabaseApiContainer实现

        private readonly Dictionary<string, IDatabaseApi> _databaseApis;

        public IDatabaseApi FindDatabaseApi(string key)
        {
            return _databaseApis.GetOrDefault(key);
        }

        public void AddDatabaseApi(string key, IDatabaseApi api)
        {
            XqCheck.NotNull(key, nameof(key));
            XqCheck.NotNull(api, nameof(api));

            if (_databaseApis.ContainsKey(key))
            {
                throw new XqException("There is already a database API in this unit of work with given key: " + key);
            }

            _databaseApis.Add(key, api);
        }

        public IDatabaseApi GetOrAddDatabaseApi(string key, Func<IDatabaseApi> factory)
        {
            XqCheck.NotNull(key, nameof(key));
            XqCheck.NotNull(factory, nameof(factory));

            return _databaseApis.GetOrAdd(key, factory);
        }

        #endregion

        #region Failed事件

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 在工作单元释放时，如果发生异常，或者事务还没有执行完成，将触发<see cref="Failed"/>事件
        /// </summary>
        protected virtual void OnFailed()
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(this, _exception, _isRolledback));
        }

        #endregion

        #region IDisposable实现

    

        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this, new UnitOfWorkEventArgs(this));
        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            DisposeTransactions();

            if (!IsCompleted || _exception != null)
            {
                OnFailed();
            }

            OnDisposed();
        }

        private void DisposeTransactions()
        {
            foreach (var transactionApi in GetAllActiveTransactionApis())
            {
                try
                {
                    transactionApi.Dispose();
                }
                catch
                {
                }
            }
        }

        public IReadOnlyList<ITransactionApi> GetAllActiveTransactionApis()
        {
            return _transactionApis.Values.ToImmutableList();
        }

        /// <summary>
        /// 执行所有的提交事务
        /// </summary>
        protected virtual void CommitTransactions()
        {
            foreach (var transaction in GetAllActiveTransactionApis())
            {
                transaction.Commit();
            }
        }

        /// <summary>
        /// 执行所有的提交事务
        /// </summary>
        protected virtual async Task CommitTransactionsAsync()
        {
            foreach (var transaction in GetAllActiveTransactionApis())
            {
                await transaction.CommitAsync().ConfigureAwait(false);
            }
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }

        #endregion
    }
}
