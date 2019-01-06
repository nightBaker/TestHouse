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
    public class TestRunService
    {
        private readonly TestHouseDbContext _dbContext;

        public TestRunService(TestHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Add new run
        /// </summary>
        /// <param name="projectId">Parent project id</param>
        /// <param name="name">Run name</param>
        /// <param name="description">Run description</param>
        /// <param name="testCasesIds">Test cases which is included in test run</param>
        /// <returns>New test run</returns>
        public async Task<TestRun> AddTestRun(long projectId, string name, string description, HashSet<long> testCasesIds)
        {

            var project = await _dbContext.Projects.FirstOrDefaultAsync()
                            ?? throw new ArgumentException("Project with specified id is not found", nameof(projectId));

            var testRun = new TestRun(name, description, new List<TestRunCase>());
            var testRunCases = await _getTestRunCases(testCasesIds, testRun);

            testRun.TestCases.AddRange(testRunCases);

            _dbContext.TestRuns.Add(testRun);
            await _dbContext.SaveChangesAsync();

            return testRun;
        }
        
        /// <summary>
        /// Add test cases to test run
        /// </summary>
        /// <param name="testCasesIds">Test cases ids</param>
        /// <param name="testRunId">Test run id</param>
        /// <returns>Test run with added test cases</returns>
        public async Task<TestRun> AddTestCases(HashSet<long> testCasesIds, long testRunId)
        {
            var testRun = await _dbContext.TestRuns.Include(tr => tr.TestCases)
                                            .Where(tr => tr.Id == testRunId)
                                            .SingleOrDefaultAsync()
                        ?? throw new ArgumentException("Test run with specified id is not found", nameof(testRunId));


            var testRunCases = await _getTestRunCases(testCasesIds, testRun);
            if (!testRunCases.Any())
            {
                throw new ArgumentException("Test cases with provided ids are not found", nameof(testCasesIds));
            }

            testRun.TestCases.AddRange(testRunCases);

            await _dbContext.SaveChangesAsync();

            return testRun;
        }        

        private async Task<IEnumerable<TestRunCase>> _getTestRunCases(HashSet<long> testCasesIds, TestRun testRun)
        {
            var testCases = await _dbContext.TestCases
                                                .Include(tc => tc.Steps)
                                                .Where(tc => testCasesIds.Contains(tc.Id))
                                                .ToListAsync();

            var testRunCases = testCases.Select(tc =>
                    new TestRunCase(tc,
                        tc.Steps.Select(s => new StepRun(s)).ToList()));
            return testRunCases;
        }

    }
}
