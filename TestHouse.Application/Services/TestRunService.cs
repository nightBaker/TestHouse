using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Services
{
    public class TestRunService
    {
        private readonly IProjectRepository _repository;

        public TestRunService(IProjectRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Add new run
        /// </summary>
        /// <param name="projectId">Parent project id</param>
        /// <param name="name">Run name</param>
        /// <param name="description">Run description</param>
        /// <param name="testCasesIds">Test cases which is included in test run</param>
        /// <returns>New test run</returns>
        public async Task<TestRun> AddTestRunAsync(long projectId, string name, string description, HashSet<long> testCasesIds)
        {
            var project = await _repository.GetAsync(projectId);

            var testRun = project.AddTestRun(name, description, testCasesIds);

            await _repository.SaveAsync();

            return testRun;
        }
        
        /// <summary>
        /// Add test cases to test run
        /// </summary>
        /// <param name="testCasesIds">Test cases ids</param>
        /// <param name="testRunId">Test run id</param>
        /// <returns>Test run with added test cases</returns>
        public async Task AddTestCases(HashSet<long> testCasesIds, long testRunId)
        {
            //var testRun = await _dbContext.TestRuns.Include(tr => tr.TestCases)
            //                                .Where(tr => tr.Id == testRunId)
            //                                .SingleOrDefaultAsync()
            //            ?? throw new ArgumentException("Test run with specified id is not found", nameof(testRunId));


            //var testRunCases = await _getTestRunCases(testCasesIds, testRun);
            //if (!testRunCases.Any())
            //{
            //    throw new ArgumentException("Test cases with provided ids are not found", nameof(testCasesIds));
            //}

            //testRun.TestCases.AddRange(testRunCases);

            //await _dbContext.SaveChangesAsync();

            //return testRun;
        }        

        private async Task _getTestRunCases(HashSet<long> testCasesIds, TestRun testRun)
        {
            //var testCases = await _dbContext.TestCases
            //                                    .Include(tc => tc.Steps)
            //                                    .Where(tc => testCasesIds.Contains(tc.Id))
            //                                    .ToListAsync();

            //var testRunCases = testCases.Select(tc =>
            //        new TestRunCase(tc,
            //            tc.Steps.Select(s => new StepRun(s)).ToList()));
            //return testRunCases;
        }

    }
}
