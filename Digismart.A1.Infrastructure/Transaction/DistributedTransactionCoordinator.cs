using System.Transactions;

namespace Digismart.A1.Infrastructure.Transaction
{
    internal sealed class DistributedTransactionCoordinator : TransactionCoordinator
    {
        private readonly TransactionScope scope = new TransactionScope();

        public DistributedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                scope.Dispose();
        }


        public override void Commit()
        {
            base.Commit();
            scope.Complete();
        }
    }
}
