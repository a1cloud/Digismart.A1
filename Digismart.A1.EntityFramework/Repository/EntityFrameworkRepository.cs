using Digismart.A1.Domain.Model;
using Digismart.A1.Domain.Repository;
using Digismart.A1.Domain.Specification;
using Digismart.A1.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Digismart.A1.EntityFramework.Repository
{
    public class EntityFrameworkRepository<TRootEntity> : Repository<TRootEntity>
        where TRootEntity : class, IRootEntity
    {
        private readonly IEntityFrameworkRepositoryContext efContext;

        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                this.efContext = context as IEntityFrameworkRepositoryContext;
        }

        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("lambda");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("lambda");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TRootEntity, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }

        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return this.efContext; }
        }

        protected override void DoAdd(TRootEntity rootEntity)
        {
            efContext.RegisterNew<TRootEntity>(rootEntity);
        }

        protected override TRootEntity DoGetByKey(Guid key)
        {
            return efContext.Context.Set<TRootEntity>().Where(p => p.ID == key).First();
        }

        protected override IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder);
            if (results == null || results.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return results;
        }

        protected override PagedResult<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, System.Linq.Expressions.Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
            if (results == null || results == PagedResult<TRootEntity>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return results;
        }

        protected override IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, System.Linq.Expressions.Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = efContext.Context.Set<TRootEntity>()
                .Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return query.ToList();
        }

        protected override PagedResult<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, System.Linq.Expressions.Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            var query = efContext.Context.Set<TRootEntity>()
                .Where(specification.GetExpression());
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TRootEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                    case SortOrder.Descending:
                        var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TRootEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序");
        }

        protected override TRootEntity DoGet(ISpecification<TRootEntity> specification)
        {
            TRootEntity result = this.DoFind(specification);
            if (result == null)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return result;
        }

        protected override TRootEntity DoFind(ISpecification<TRootEntity> specification)
        {
            return efContext.Context.Set<TRootEntity>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }

        protected override bool DoExists(ISpecification<TRootEntity> specification)
        {
            var count = efContext.Context.Set<TRootEntity>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }

        protected override void DoRemove(TRootEntity rootEntity)
        {
            efContext.RegisterDeleted<TRootEntity>(rootEntity);
        }

        protected override void DoUpdate(TRootEntity rootEntity)
        {
            efContext.RegisterModified<TRootEntity>(rootEntity);
        }

        protected override TRootEntity DoFind(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TRootEntity>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.GetExpression()).FirstOrDefault();
            }
            else
                return dbset.Where(specification.GetExpression()).FirstOrDefault();
        }

        protected override IEnumerable<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
            if (results == null || results.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return results;
        }

        protected override PagedResult<TRootEntity> DoGetAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
            if (results == null || results == PagedResult<TRootEntity>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return results;
        }

        protected override IEnumerable<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TRootEntity>();
            IQueryable<TRootEntity> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return queryable.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return queryable.ToList();
        }

        protected override PagedResult<TRootEntity> DoFindAll(ISpecification<TRootEntity> specification, Expression<Func<TRootEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            var dbset = efContext.Context.Set<TRootEntity>();
            IQueryable<TRootEntity> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending = queryable.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TRootEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                    case SortOrder.Descending:
                        var pagedGroupDescending = queryable.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TRootEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序");
        }

        protected override TRootEntity DoGet(ISpecification<TRootEntity> specification, params Expression<Func<TRootEntity, dynamic>>[] eagerLoadingProperties)
        {
            TRootEntity result = this.DoFind(specification, eagerLoadingProperties);
            if (result == null)
                throw new ArgumentException("无法根据指定的查询条件找到所需的根实体");
            return result;
        }
    }
}
