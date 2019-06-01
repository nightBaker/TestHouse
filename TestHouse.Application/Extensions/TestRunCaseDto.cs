using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{
   
    public static class TestRunCaseExtensions
    {
        public static IEnumerable<TestRunCaseDto> ToTestRunCasesDtos(this IEnumerable<TestRunCase> testRuns)
        {
            foreach (var item in testRuns)
            {
                yield return item.ToTestRunCaseDto();
            }
        }

        public static TestRunCaseDto ToTestRunCaseDto(this TestRunCase item)
        {
            return new TestRunCaseDto()
            {
                Id = item.Id,
                TestCase = item.TestCase.ToTestCaseDto(),
                Status = item.Status,
                Steps = item.Steps.ToStepsRunDtos()
            };
        }
    }
}
