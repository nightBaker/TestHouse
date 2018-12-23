using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Models;
using TestHouse.Persistence;

namespace TestHouse.Application.Services
{
    public class StepService
    {
        private readonly TestHouseDbContext _dbContext;

        public StepService(TestHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add new steps to existing test case
        /// </summary>
        /// <param name="steps">Steps for adding</param>
        /// <param name="testCaseId">Test case id</param>
        /// <returns>Test case</returns>
        public async Task<TestCase> AddSteps(List<Step> steps, long testCaseId)
        {
            if (steps == null || !steps.Any()) throw new ArgumentException("Steps are not specified", nameof(steps));

            var testCase = await _dbContext.TestCases
                .Include(s => s.Steps)
                .FirstOrDefaultAsync(s => s.Id == testCaseId)
                ?? throw new ArgumentException("Test case with specified id is not found", nameof(testCaseId));

            testCase.Steps.AddRange(steps);

            return testCase;
        }
    }
}
