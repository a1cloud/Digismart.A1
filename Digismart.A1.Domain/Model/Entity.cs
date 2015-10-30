using System;

namespace Digismart.A1.Domain.Model
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class Entity : IEntity
    {
        #region IEntity
        /// <summary>
        /// 实体类的全局唯一标识
        /// </summary>
        public Guid ID { get; set; }
        #endregion

        /// <summary>
        /// 确定指定对象是否等于实体类
        /// </summary>
        /// <param name="obj">要比较的对象</param>
        /// <returns>如果指定对象与实体类相等，则返回true，否则返回false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            IEntity entity = obj as IEntity;
            if (entity == null)
                return false;
            return this.ID == entity.ID;
        }

        /// <summary>
        /// 获取实体类哈希码
        /// </summary>
        /// <returns>实体类的哈希码</returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
