using Digismart.A1.Infrastructure.Common;
using System.Collections.Generic;

namespace Digismart.A1.Infrastructure.Transaction
{
    public abstract class TransactionCoordinator : DisposableObject, ITransactionCoordinator
    {
        private readonly List<IUnitOfWork> managedUnitOfWorks = new List<IUnitOfWork>();

        public TransactionCoordinator(params IUnitOfWork[] unitOfWorks)
        {
            if (unitOfWorks != null &&
                unitOfWorks.Length > 0)
            {
                foreach (var uow in unitOfWorks)
                    managedUnitOfWorks.Add(uow);
            }
        }

        protected override void Dispose(bool disposing)
        {
        }

        #region IUnitOfWork
        public bool DistributedTransactionSupported
        {
            get { return false; } // 没有意义
        }

        public bool Committed
        {
            get { return true; } // 没有意义
        }

        public virtual void Commit()
        {
            if (managedUnitOfWorks.Count > 0)
                foreach (var uow in managedUnitOfWorks)
                    uow.Commit();
        }

        public virtual void Rollback() // 基本上没有意义
        {

        }
        #endregion
    }
}
