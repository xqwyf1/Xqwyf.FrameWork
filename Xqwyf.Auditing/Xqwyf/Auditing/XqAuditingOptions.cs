﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace  Xqwyf.Auditing
{
    public class XqAuditingOptions
    {
        //TODO: Consider to add an option to disable auditing for application service methods?

        /// <summary>
        /// If this value is true, auditing will not throw an exceptions and it will log it when an error occurred while saving AuditLog.
        /// Default: true.
        /// </summary>
        public bool HideErrors { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The name of the application or service writing audit logs.
        /// Default: null.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        public List<AuditLogContributor> Contributors { get; }

        /// <summary>
        /// 审计时，可以忽略的类型列表
        /// </summary>
        public List<Type> IgnoredTypes { get; }

        public IEntityHistorySelectorList EntityHistorySelectors { get; }

        //TODO: Move this to asp.net core layer or convert it to a more dynamic strategy?
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsEnabledForGetRequests { get; set; }

        public XqAuditingOptions()
        {
            IsEnabled = true;
            IsEnabledForAnonymousUsers = true;
            HideErrors = true;

            Contributors = new List<AuditLogContributor>();

            IgnoredTypes = new List<Type>
            {
                typeof(Stream),
                typeof(Expression)
            };

            EntityHistorySelectors = new EntityHistorySelectorList();
        }
    }
}
