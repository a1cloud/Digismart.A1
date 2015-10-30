using Digismart.A1.Domain.Model;
using Digismart.A1.Infrastructure.Transaction;
using System;

namespace Digismart.A1.Domain.Repository
{
    /// <summary>
    /// 表示实现该接口的类型是仓储上下文
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取仓储上下文的ID
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 将指定的根实体标注为“新建”状态
        /// </summary>
        /// <typeparam name="TRootEntity">需要标注状态的根实体类型</typeparam>
        /// <param name="obj">需要标注状态的根实体</param>
        void RegisterNew<TRootEntity>(TRootEntity obj)
            where TRootEntity : class, IRootEntity;
        /// <summary>
        /// 将指定的根实体标注为“更改”状态
        /// </summary>
        /// <typeparam name="TRootEntity">需要标注状态的根实体类型</typeparam>
        /// <param name="obj">需要标注状态的根实体</param>
        void RegisterModified<TRootEntity>(TRootEntity obj)
            where TRootEntity : class, IRootEntity;
        /// <summary>
        /// 将指定的根实体标注为“删除”状态
        /// </summary>
        /// <typeparam name="TRootEntity">需要标注状态的根实体类型</typeparam>
        /// <param name="obj">需要标注状态的根实体</param>
        void RegisterDeleted<TRootEntity>(TRootEntity obj)
            where TRootEntity : class, IRootEntity;
    }
}
