using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Xqwyf.DependencyInjection;

namespace Xqwyf.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager, ISingletonDependency
    {
        public IUnitOfWork Current => GetCurrentUnitOfWork();

        private readonly IXqServiceScopeFactory _serviceScopeFactory;
        private readonly IAmbientUnitOfWork _ambientUnitOfWork;

        public UnitOfWorkManager(
            IAmbientUnitOfWork ambientUnitOfWork,
            IXqServiceScopeFactory serviceScopeFactory)
        {
            _ambientUnitOfWork = ambientUnitOfWork;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 获得一个工作单元,如果<paramref name="requiresNew"/>为False，将把当前的Uow作为一个<see cref="ChildUnitOfWork"/>返回
        /// 如果<paramref name="requiresNew"/>为True，创建一个Uow
        /// </summary>
        /// <param name="options"></param>
        /// <param name="requiresNew"></param>
        /// <returns></returns>
        public IUnitOfWork Begin(XqUnitOfWorkOptions options, bool requiresNew = false)
        {
            XqCheck.NotNull(options, nameof(options));

            var currentUow = Current;
            if (currentUow != null && !requiresNew)
            {
                return new ChildUnitOfWork(currentUow);
            }

            var unitOfWork = CreateNewUnitOfWork();
            unitOfWork.Initialize(options);

            return unitOfWork;
        }

        /// <summary>
        /// 保留<see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="reservationName"></param>
        /// <param name="requiresNew"></param>
        /// <returns></returns>
        public IUnitOfWork Reserve(string reservationName, bool requiresNew = false)
        {
            XqCheck.NotNull(reservationName, nameof(reservationName));

            if (!requiresNew &&
                _ambientUnitOfWork.UnitOfWork != null &&
                _ambientUnitOfWork.UnitOfWork.IsReservedFor(reservationName))
            {
                return new ChildUnitOfWork(_ambientUnitOfWork.UnitOfWork);
            }

            var unitOfWork = CreateNewUnitOfWork();
            unitOfWork.Reserve(reservationName);

            return unitOfWork;
        }

        public void BeginReserved(string reservationName, XqUnitOfWorkOptions options)
        {
            if (!TryBeginReserved(reservationName, options))
            {
                throw new XqException($"Could not find a reserved unit of work with reservation name: {reservationName}");
            }
        }

        public bool TryBeginReserved(string reservationName, XqUnitOfWorkOptions options)
        {
            XqCheck.NotNull(reservationName, nameof(reservationName));

            var uow = _ambientUnitOfWork.UnitOfWork;

            //Find reserved unit of work starting from current and going to outers
            while (uow != null && !uow.IsReservedFor(reservationName))
            {
                uow = uow.Outer;
            }

            if (uow == null)
            {
                return false;
            }

            uow.Initialize(options);

            return true;
        }

        /// <summary>
        /// 获取当前的<see cref="UnitOfWork"/>
        /// </summary>
        /// <returns></returns>
        private IUnitOfWork GetCurrentUnitOfWork()
        {
            var uow = _ambientUnitOfWork.UnitOfWork;

            //Skip reserved unit of work
            while (uow != null && (uow.IsReserved || uow.IsDisposed || uow.IsCompleted))
            {
                uow = uow.Outer;
            }

            return uow;
        }

        /// <summary>
        /// 创建一个新的<see cref="IUnitOfWork"/>
        /// </summary>
        /// <returns></returns>
        private IUnitOfWork CreateNewUnitOfWork()
        {
            var scope = _serviceScopeFactory.CreateScope();
            try
            {
                var outerUow = _ambientUnitOfWork.UnitOfWork;

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                unitOfWork.SetOuter(outerUow);

                _ambientUnitOfWork.SetUnitOfWork(unitOfWork);

                unitOfWork.Disposed += (sender, args) =>
                {
                    _ambientUnitOfWork.SetUnitOfWork(outerUow);
                    scope.Dispose();
                };

                return unitOfWork;
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
