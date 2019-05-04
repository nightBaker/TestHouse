using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{   
    public static class TestCaseExtensions
    {
        public static IEnumerable<TestCaseDto> ToTestCasesDto(this IEnumerable<TestCase> testCases)
        {
            foreach (var item in testCases)
            {
                yield return item.ToTestCaseDto();
            }
        }

        public static TestCaseDto ToTestCaseDto(this TestCase item)
        {
            return new TestCaseDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedAt = item.CreatedAt,
                ExpectedResult = item.ExpectedResult,
                Order = item.Order,
                Steps = item.Steps,
            };
        }
    }
}
