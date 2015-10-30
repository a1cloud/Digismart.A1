using System;
using System.Collections.Generic;
using System.Threading;
using Digismart.A1.Infrastructure.Utils;
using Digismart.A1.Domain.Model;
using Digismart.A1.Infrastructure.Common;

namespace Digismart.A1.Domain.Repository
{
    /// <summary>
    /// Represents the base class for repository contexts.
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        private readonly Guid id = SequentialGuid.NewGuid();
        private readonly ThreadLocal<Dictionary<Guid, object>> localNewCollection = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());
        private readonly ThreadLocal<Dictionary<Guid, object>> localModifiedCollection = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());
        private readonly ThreadLocal<Dictionary<Guid, object>> localDeletedCollection = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());
        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);
        private readonly object sync = new object();

        public RepositoryContext()
        {
        }

        /// <summary>
        /// Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            this.localNewCollection.Value.Clear();
            this.localModifiedCollection.Value.Clear();
            this.localDeletedCollection.Value.Clear();
        }
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localCommitted.Dispose();
                this.localDeletedCollection.Dispose();
                this.localModifiedCollection.Dispose();
                this.localNewCollection.Dispose();
            }
        }

        protected abstract void DoCommit();

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be added to the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> NewCollection
        {
            get { return localNewCollection.Value; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> ModifiedCollection
        {
            get { return localModifiedCollection.Value; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> DeletedCollection
        {
            get { return localDeletedCollection.Value; }
        }

        #region IRepositoryContext
        /// <summary>
        /// Gets the ID of the repository context.
        /// </summary>
        public Guid ID
        {
            get { return id; }
        }
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TRootEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterNew<TRootEntity>(TRootEntity obj) where TRootEntity : class, IRootEntity
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("实体ID不可为空", "obj");
            if (localModifiedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException("实体已经被标记为修改状态, 不可再标记为新增状态");
            if (localNewCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException("实体已经被标记为新增状态");
            localNewCollection.Value.Add(obj.ID, obj);
            localCommitted.Value = false;
        }
        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TRootEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterModified<TRootEntity>(TRootEntity obj) where TRootEntity : class, IRootEntity
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("实体ID不可为空", "obj");
            if (localDeletedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException("实体已经被标记为删除状态, 不可再标记为修改状态");
            if (!localModifiedCollection.Value.ContainsKey(obj.ID) && !localNewCollection.Value.ContainsKey(obj.ID))
                localModifiedCollection.Value.Add(obj.ID, obj);
            localCommitted.Value = false;
        }
        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <typeparam name="TRootEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterDeleted<TRootEntity>(TRootEntity obj) where TRootEntity : class, IRootEntity
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("实体ID不可为空", "obj");
            if (localNewCollection.Value.ContainsKey(obj.ID))
            {
                if (localNewCollection.Value.Remove(obj.ID))
                    return;
            }
            bool removedFromModified = localModifiedCollection.Value.Remove(obj.ID);
            bool addedToDeleted = false;
            if (!localDeletedCollection.Value.ContainsKey(obj.ID))
            {
                localDeletedCollection.Value.Add(obj.ID, obj);
                addedToDeleted = true;
            }
            localCommitted.Value = !(removedFromModified || addedToDeleted);
        }
        #endregion

        #region IUnitOfWork
        /// <summary>
        /// 表示当前的Unit Of Work是否支持Microsoft分布式事务
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }
        /// <summary>
        /// value which indicates whether the UnitOfWork was committed
        /// </summary>
        public bool Committed
        {
            get { return localCommitted.Value; }
            protected set { localCommitted.Value = value; }
        }
        /// <summary>
        /// Commits the UnitOfWork.
        /// </summary>
        public virtual void Commit()
        {
            this.DoCommit();
        }
        /// <summary>
        /// Rolls-back the UnitOfWork.
        /// </summary>
        public abstract void Rollback();
        #endregion
    }
}