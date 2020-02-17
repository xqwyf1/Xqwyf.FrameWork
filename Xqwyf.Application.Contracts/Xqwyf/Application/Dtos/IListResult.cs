using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Application.Dtos
{
    /// <summary>
    /// 定义此接口是为了以标准化方式将项目列表返回给客户端。
    /// </summary>
    /// <typeparam name="T">列表中对象类型</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// 数据项列表
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}
