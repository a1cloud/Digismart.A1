using Digismart.A1.Domain.Repository;
using System.Data.Entity;
using System.Threading;

namespace Digismart.A1.EntityFramework.Repository
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<A1DbContext> localCtx = new ThreadLocal<A1DbContext>(() => new A1DbContext());

        public EntityFrameworkRepositoryContext() { }

        public override void RegisterDeleted<TRootEntity>(TRootEntity obj)
        {
            localCtx.Value.Set<TRootEntity>().Remove(obj);
            Committed = false;
        }

        public override void RegisterModified<TRootEntity>(TRootEntity obj)
        {
            localCtx.Value.Entry<TRootEntity>(obj).State = EntityState.Modified;
            Committed = false;
        }

        public override void RegisterNew<TRootEntity>(TRootEntity obj)
        {
            localCtx.Value.Set<TRootEntity>().Add(obj);
            Committed = false;
        }

        public override bool DistributedTransactionSupported
        {
            get { return false; } //不支持分布式事务
        }

        public override void Rollback()
        {
            Committed = false;
        }

        protected override void DoCommit()
        {
            if (!Committed)
            {
                var validationErrors = localCtx.Value.GetValidationErrors();
                var count = localCtx.Value.SaveChanges();
                Committed = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                localCtx.Value.Dispose();
                localCtx.Dispose();
                base.Dispose(disposing);
            }
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context
        {
            get { return localCtx.Value; }
        }
        #endregion
    }
}
