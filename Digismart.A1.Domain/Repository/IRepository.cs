using Digismart.A1.Domain.Model;
using Digismart.A1.Domain.Specification;
using Digismart.A1.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Digismart.A1.Domain.Repository
{
    /// <summary>
    /// 表示实现该接口的类型是应用于某种根实体的仓储类型
    /// </summary>
    /// <typeparam name="TRootEntity">根实体类型</typeparam>
    public interface IRepository<TRootEntity>
        where TRootEntity : class, IRootEntity
    {
        #region Properties
        /// <summary>
        /// 获取当前仓储所使用的仓储上下文实例。
        /// </summary>
        IRepositoryContext Context { get; }
        #endregion

        #region Methods
        /// <summary>
        /// 将指定的根实体添加到仓储中
        /// </summary>
        /// <param name="rootEntity">需要添加到仓储的根实体实例</param>
        void Add(TRootEntity rootEntity);
        /// <summary>
        /// 根据根实体的ID值，从仓储中读取根实体。
        /// </summary>
        /// <param name="key">根实体的ID值。</param>
        /// <returns>根实体实例。</returns>
        TRootEntity GetByKey(Guid key);
        /// <summary>
        /// 从仓储中读取所有根实体。
        /// </summary>
        /// <returns>所有的根实体。</returns>
        IEnumerable<TRootEntity> GetAll();
        /// <summary>
        /// 以指定的排序字段和排序方式，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>排序后的所有根实体。</returns>
        IEnumerable<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 根据指定的规约，从仓储中获取所有符合条件的根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>所有符合条件的根实体。</returns>
        IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>所有符合条件的、排序后的根实体。</returns>
        IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>符合条件的、排序后的带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有的根实体。</returns>
        IEnumerable<TRootEntity> GetAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 以指定的排序字段和排序方式，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>排序后的所有根实体。</returns>
        IEnumerable<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> GetAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的根实体。</returns>
        IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的已经排序的根实体。</returns>
        IEnumerable<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的已经排序的根实体。</returns>
        PagedResult<TRootEntity> GetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约获取根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>根实体。</returns>
        TRootEntity Get(ISpecification<TRootEntity> specification);
        /// <summary>
        /// 根据指定的规约，以饥饿加载的方式获取根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>根实体。</returns>
        TRootEntity Get(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从仓储中查找所有根实体。
        /// </summary>
        /// <returns>所有的根实体。</returns>
        IEnumerable<TRootEntity> FindAll();
        /// <summary>
        /// 以指定的排序字段和排序方式，从仓储中查找所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>排序后的所有根实体。</returns>
        IEnumerable<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中查找所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 根据指定的规约，从仓储中查找所有符合条件的根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>所有符合条件的根实体。</returns>
        IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>所有符合条件的、排序后的根实体。</returns>
        IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>符合条件的、排序后的带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 以饥饿加载的方式查找所有根实体。
        /// </summary>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有的根实体。</returns>
        IEnumerable<TRootEntity> FindAll(params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 以指定的排序字段和排序方式，以饥饿加载的方式获取所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>排序后的所有根实体。</returns>
        IEnumerable<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，以饥饿加载的方式查找所有根实体。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>带有分页信息的根实体集合。</returns>
        PagedResult<TRootEntity> FindAll(Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以饥饿加载的方式查找所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的根实体。</returns>
        IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以饥饿加载的方式查找所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的已经排序的根实体。</returns>
        IEnumerable<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，以饥饿加载的方式查找所有根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>所有符合条件的已经排序的根实体。</returns>
        PagedResult<TRootEntity> FindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 根据指定的规约查找根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>根实体。</returns>
        TRootEntity Find(ISpecification<TRootEntity> specification);
        /// <summary>
        /// 根据指定的规约，以饥饿加载的方式查找根实体。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="eagerLoadingProperties">需要进行饥饿加载的属性Lambda表达式。</param>
        /// <returns>根实体。</returns>
        TRootEntity Find(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的根实体是否存在。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>如果符合指定规约条件的根实体存在，则返回true，否则返回false。</returns>
        bool Exists(ISpecification<TRootEntity> specification);
        /// <summary>
        /// 将指定的根实体从仓储中移除。
        /// </summary>
        /// <param name="rootEntity">需要从仓储中移除的根实体。</param>
        void Remove(TRootEntity rootEntity);
        /// <summary>
        /// 更新指定的根实体。
        /// </summary>
        /// <param name="rootEntity">需要更新的根实体。</param>
        void Update(TRootEntity rootEntity);
        #endregion
    }
}
