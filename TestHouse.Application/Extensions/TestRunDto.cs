using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{    
    public static class TestRunExtensions
    {
        public static IEnumerable<TestRunDto> ToTestRunDtos(this IEnumerable<TestRun> testRuns)
        {
            foreach (var item in testRuns)
            {
                yield return item.ToTestRunDto();
            }
        }

        public static TestRunDto ToTestRunDto(this TestRun item)
        {
            return new TestRunDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                TestCases = item.TestCases?.ToTestRunCasesDtos()
            };
        }
    }
}
