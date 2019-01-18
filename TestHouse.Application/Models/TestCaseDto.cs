using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Models
{
    public class TestCaseDto
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string ExpectedResult { get; internal set; }
        public uint Order { get; internal set; }
        public List<Step> Steps { get; internal set; }
    }

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
