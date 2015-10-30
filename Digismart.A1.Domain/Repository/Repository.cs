using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Digismart.A1.Domain.Model;
using Digismart.A1.Domain.Specification;
using Digismart.A1.Infrastructure.Common;

namespace Digismart.A1.Domain.Repository
{
    /// <summary>
    /// Represents the base class for repositories.
    /// </summary>
    /// <typeparam name="TRootEntity">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public abstract class Repository<TRootEntity> : IRepository<TRootEntity>
        where TRootEntity : class, IRootEntity
    {
        #region Private Fields
        private readonly IRepositoryContext context;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="context">The repository context being used by the repository.</param>
        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be added to the repository.</param>
        protected abstract void DoAdd(TRootEntity rootEntity);
        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected abstract TRootEntity DoGetByKey(Guid key);
        /// <summary>
        /// Gets all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll()
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), null, SortOrder.Unspecified);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository for the specified page, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual PagedResult<TRootEntity> DoGetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification)
        {
            return DoGetAll(specification, null, SortOrder.Unspecified);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository for the specified page, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual PagedResult<TRootEntity> DoGetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoGetAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        protected virtual IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoGetAll(specification, null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// Gets all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Gets all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract PagedResult<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// Gets all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract PagedResult<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll()
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), null, SortOrder.Unspecified);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual PagedResult<TRootEntity> DoFindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification)
        {
            return DoFindAll(specification, null, SortOrder.Unspecified);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository for the specified page, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        protected virtual PagedResult<TRootEntity> DoFindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TRootEntity>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        protected virtual IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(specification, null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// Finds all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract PagedResult<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// Gets all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Gets all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected abstract PagedResult<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Gets a single aggregate root instance from repository by using the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The aggregate root instance.</returns>
        protected abstract TRootEntity DoGet(ISpecification<TRootEntity> specification);
        /// <summary>
        /// Finds a single aggregate root that matches the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected abstract TRootEntity DoFind(ISpecification<TRootEntity> specification);
        /// <summary>
        /// Gets a single aggregate root instance from repository by using the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The aggregate root instance.</returns>
        protected abstract TRootEntity DoGet(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Finds a single aggregate root that matches the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected abstract TRootEntity DoFind(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Checkes whether the aggregate root, which matches the given specification, exists in the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>True if the aggregate root exists, otherwise false.</returns>
        protected abstract bool DoExists(ISpecification<TRootEntity> specification);
        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be removed.</param>
        protected abstract void DoRemove(TRootEntity rootEntity);
        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be updated.</param>
        protected abstract void DoUpdate(TRootEntity rootEntity);
        #endregion

        #region IRepository
        /// <summary>
        /// Gets the <see cref="Repositories.IRepositoryContext"/> instance.
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this.context; }
        }
        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be added to the repository.</param>
        public void Add(TRootEntity rootEntity)
        {
            this.DoAdd(rootEntity);
        }
        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        public TRootEntity GetByKey(Guid key)
        {
            return this.DoGetByKey(key);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        public IEnumerable<TRootEntity> GetAll()
        {
            return this.DoGetAll();
        }
        /// <summary>
        /// Gets all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        public IEnumerable<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoGetAll(sortPredicate, sortOrder);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        public IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification)
        {
            return this.DoGetAll(specification);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        public IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoGetAll(specification, sortPredicate, sortOrder);
        }
        /// <summary>
        /// Gets all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository for the specified page, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        public PagedResult<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoGetAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// Gets all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        public PagedResult<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoGetAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be removed.</param>
        public void Remove(TRootEntity rootEntity)
        {
            this.DoRemove(rootEntity);
        }
        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="rootEntity">The aggregate root to be updated.</param>
        public void Update(TRootEntity rootEntity)
        {
            this.DoUpdate(rootEntity);
        }
        /// <summary>
        /// Gets a single aggregate root instance from repository by using the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The aggregate root instance.</returns>
        public TRootEntity Get(ISpecification<TRootEntity> specification)
        {
            return this.DoGet(specification);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <returns>All the aggregate roots got from the repository.</returns>
        public IEnumerable<TRootEntity> FindAll()
        {
            return this.DoFindAll();
        }
        /// <summary>
        /// Finds all the aggregate roots from repository, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        public IEnumerable<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>All the aggregate roots that match the given specification.</returns>
        public IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification)
        {
            return this.DoFindAll(specification);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        public IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository with paging enabled, sorting by using the provided sort predicate
        /// and the specified sort order.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots got from the repository, with the aggregate roots being sorted by
        /// using the provided sort predicate and the sort order.</returns>
        public PagedResult<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        public PagedResult<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// Finds a single aggregate root that matches the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        public TRootEntity Find(ISpecification<TRootEntity> specification)
        {
            return this.DoFind(specification);
        }

        public TRootEntity Find(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification, eagerLoadingProperties);
        }
        /// <summary>
        /// Checkes whether the aggregate root, which matches the given specification, exists in the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>True if the aggregate root exists, otherwise false.</returns>
        public bool Exists(ISpecification<TRootEntity> specification)
        {
            return this.DoExists(specification);
        }
        
        public IEnumerable<TRootEntity> GetAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }

        public PagedResult<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(specification, eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }

        public PagedResult<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        public TRootEntity Get(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGet(specification, eagerLoadingProperties);
        }

        public PagedResult<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> FindAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }

        public PagedResult<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, eagerLoadingProperties);
        }

        public IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }
        #endregion
    }
}
