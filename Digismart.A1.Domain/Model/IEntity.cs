using System;

namespace Digismart.A1.Domain.Model
{
    /// <summary>
    /// 表示继承于该接口的类型是实体类
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 实体类的全局唯一标识
        /// </summary>
        Guid ID { get; }
    }
}
