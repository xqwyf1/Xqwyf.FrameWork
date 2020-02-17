using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Application.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to request a paged and sorted result.
    /// </summary>
    public interface IPagedAndSortedResultRequest : IPagedResultRequest, ISortedResultRequest
    {

    }
}
