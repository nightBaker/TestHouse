using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Extensions;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

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
        public async Task<TestRunDto> AddTestRunAsync(long projectId, string name, string description, HashSet<long> testCasesIds)
        {
            var project = await _repository.GetAsync(projectId);

            var testRun = project.AddTestRun(name, description, testCasesIds);

            await _repository.SaveAsync();

            return testRun.ToTestRunDto();
        }
        
        /// <summary>
        /// Add test cases to test run
        /// </summary>
        /// <param name="testCasesIds">Test cases ids</param>
        /// <param name="testRunId">Test run id</param>
        /// <returns>Test run with added test cases</returns>
        public async Task<IEnumerable<TestRunCaseDto>> AddTestCases(long projectId, long testRunId, HashSet<long> testCasesIds)
        {
            var project = await _repository.GetAsync(projectId, testRunId)
                        ?? throw new ArgumentException("Project with specified id is not found", nameof(projectId));


            var testRunCases = project.AddTestCasesToRun(testCasesIds, testRunId);

            await _repository.SaveAsync();

            return testRunCases.ToTestRunCasesDtos();
        }               
    }
}
