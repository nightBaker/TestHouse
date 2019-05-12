using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Extensions
{
    public class StepRunDto
    {
        /// <summary>
        /// Step run id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Step
        /// </summary>
        public Step Step { get; internal set; }

        /// <summary>
        /// Status
        /// </summary>
        public StepRunStatus Status { get; internal set; }
    }

    public static class RunStepExtensions
    {
        public static IEnumerable<StepRunDto> ToStepsRunDtos(this IEnumerable<StepRun> steps)
        {
            foreach (var item in steps)
            {
                yield return item.ToStepRunDto();
            }
        }

        public static StepRunDto ToStepRunDto(this StepRun item)
        {
            return new StepRunDto()
            {
                Id = item.Id,                
                Status = item.Status,
                Step = item.Step
            };
        }
    }
}
