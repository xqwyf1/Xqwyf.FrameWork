using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Xqwyf.Application.Dtos
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class LimitedResultRequestDto : ILimitedResultRequest, IValidatableObject
    {
        /// <summary>
        ///默认的每页结果数量
        /// </summary>
        public static int DefaultMaxResultCount { get; set; } = 10;

        /// <summary>
        /// 允许设置的最大值
        /// </summary>
        public static int MaxMaxResultCount { get; set; } = 1000;

        /// <summary>
        /// Maximum result count should be returned.
        /// This is generally used to limit result count on paging.
        /// </summary>
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxResultCount > MaxMaxResultCount)
            {
                var localizer = validationContext.GetRequiredService<IStringLocalizer<XqDddApplicationContractsResource>>();

                yield return new ValidationResult(
                    localizer[
                        "MaxResultCountExceededExceptionMessage",
                        nameof(MaxResultCount),
                        MaxMaxResultCount,
                        typeof(LimitedResultRequestDto).FullName,
                        nameof(MaxMaxResultCount)
                    ],
                    new[] { nameof(MaxResultCount) });
            }
        }
    }
}
