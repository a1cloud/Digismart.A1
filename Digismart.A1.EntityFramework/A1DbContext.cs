using System.Data.Entity;

namespace Digismart.A1.EntityFramework
{
    /// <summary>
    /// 表示公共数据库访问上下文
    /// </summary>
    public sealed partial class A1DbContext : DbContext
    {
        #region Ctor
        /// <summary>
        /// 构造函数
        /// </summary>
        public A1DbContext()
            : base("A1")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        #endregion
    }
}
