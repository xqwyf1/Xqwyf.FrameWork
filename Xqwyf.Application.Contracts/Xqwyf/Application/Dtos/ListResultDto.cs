using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Application.Dtos
{
    /// <summary>
    /// 向客户端返回类型为<typeparamref name="T"/>的数据项列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <inheritdoc />
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// 创建一个 <see cref="ListResultDto{T}"/> 对象
        /// </summary>
        public ListResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
