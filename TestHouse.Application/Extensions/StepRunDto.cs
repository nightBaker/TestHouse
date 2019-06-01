using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{
    
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
