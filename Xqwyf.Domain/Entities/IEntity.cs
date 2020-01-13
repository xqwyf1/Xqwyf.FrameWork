using System;

namespace Xqwyf.Domain
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 返回该实体的主键列表
        /// </summary>
        /// <returns></returns>
        object[] GetKeys();
    }
}
