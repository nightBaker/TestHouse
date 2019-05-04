using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Extensions
{
    public class TestRunDto
    {
        /// <summary>
        /// Test run id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Test run name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// List of test cases
        /// </summary>
        public IEnumerable<TestRunCaseDto> TestCases { get; internal set; }
    }

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
                TestCases = item.TestCases.ToTestRunCasesDtos()
            };
        }
    }
}
