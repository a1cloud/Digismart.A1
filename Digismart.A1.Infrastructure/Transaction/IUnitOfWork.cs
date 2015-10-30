
namespace Digismart.A1.Infrastructure.Transaction
{
    /// <summary>
    /// 表示所有集成于该接口的类型都是Unit Of Work的一种实现。
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 当前的Unit Of Work是否支持Microsoft分布式事务处理机制
        /// </summary>
        bool DistributedTransactionSupported { get; }
        /// <summary>
        /// 当前的Unit Of Work事务是否已被提交。
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 提交当前的Unit Of Work事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚当前的Unit Of Work事务
        /// </summary>
        void Rollback();
    }
}
