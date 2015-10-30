
namespace Digismart.A1.Infrastructure.Transaction
{
    internal sealed class NormalTransactionCoordinator : TransactionCoordinator
    {
        public NormalTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
        }
    }
}
