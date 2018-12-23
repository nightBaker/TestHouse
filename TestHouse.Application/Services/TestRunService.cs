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
        /// <returns></returns>
        public async Task<TestRun> AddTestRun(long projectId, string name, string description, HashSet<long> testCasesIds)
        {

            var project = await _dbContext.Projects.FirstOrDefaultAsync()
                            ?? throw new ArgumentException("Project with specified id is not found", nameof(projectId));

            var testCases = await _dbContext.TestCases
                                    .Include(tc=>tc.Steps)
                                    .Where(tc => testCasesIds.Contains(tc.Id))
                                    .ToListAsync();

            var testRun = new TestRun(name, description, project);
            var testRunCases = testCases.Select(tc => 
                    new TestCaseRun( tc, 
                        tc.Steps.Select(s => new StepRun(s)).ToList(),
                        testRun));

            testRun.TestCases.AddRange(testRunCases);

            await _dbContext.SaveChangesAsync();

            return testRun;
        }

    }
}
