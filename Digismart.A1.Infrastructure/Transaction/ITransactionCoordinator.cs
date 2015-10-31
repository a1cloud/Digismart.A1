using Digismart.A1.Infrastructure.Common;
using System;

namespace Digismart.A1.Infrastructure.Transaction
{
    public interface ITransactionCoordinator : IUnitOfWork, IDisposable
    {
    }
}
