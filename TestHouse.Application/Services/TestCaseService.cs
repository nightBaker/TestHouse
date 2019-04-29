using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Models;
using TestHouse.Domain.Models;


namespace TestHouse.Application.Services
{
    public class TestCaseService
    {
        private readonly IProjectRepository _repository;

        public TestCaseService(IProjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Add new test case
        /// </summary>
        /// <param name="name">name of the test case</param>
        /// <param name="description">description of the test case</param>
        /// <param name="expectedResult">expected result of the test case</param>
        /// <param name="suitId">parent suit id</param>
        /// <returns>created test case</returns>
        public async Task<TestCaseDto> AddTestCaseAsync(string name, string description, string expectedResult,long projectId, long suitId, List<Step> steps)
        {
            var project = await _repository.GetAsync(projectId)
                ?? throw new ArgumentException("Project with specified id is not found", nameof(projectId));


            var testCase = project.AddTestCase(name, description, expectedResult, suitId, steps);

            await _repository.SaveAsync();

            return testCase.ToTestCaseDto();
        }        

    }
}
