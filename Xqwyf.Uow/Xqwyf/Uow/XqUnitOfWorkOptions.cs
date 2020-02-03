using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Xqwyf.Uow
{
    public class XqUnitOfWorkOptions : IXqUnitOfWorkOptions
    {
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsTransactional { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public XqUnitOfWorkOptions()
        {

        }

        public XqUnitOfWorkOptions(bool isTransactional = false, IsolationLevel? isolationLevel = null, TimeSpan? timeout = null)
        {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        public XqUnitOfWorkOptions Clone()
        {
            return new XqUnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout
            };
        }
    }
}
