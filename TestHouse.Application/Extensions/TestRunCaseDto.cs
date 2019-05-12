using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{
    public class TestRunCaseDto
    {
        /// <summary>
        /// Test run case id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Test case
        /// </summary>
        public TestCaseDto TestCase { get; internal set; }

        /// <summary>
        /// Status
        /// </summary>
        public TestCaseStatus Status { get; internal set; }

        /// <summary>
        /// Steps
        /// </summary>
        public IEnumerable<StepRunDto> Steps { get; internal set; }
    }

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
