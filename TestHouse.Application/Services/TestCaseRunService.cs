using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Services
{
    public class TestCaseRunService
    {
        private readonly IProjectRepository _repository;

        public TestCaseRunService(IProjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Change status for test run test case
        /// </summary>
        /// <param name="testRunCaseId">Test run case id</param>
        /// <param name="status">New status</param>
        /// <returns>Test run case with new status</returns>
        public async Task ChangeStatus(long testRunCaseId, TestCaseStatus status)
        {
            //var testRunCase = await _repository.TestRunCases.Where(trc => trc.Id == testRunCaseId).SingleOrDefaultAsync();

            //testRunCase.Status = status;

            //return testRunCase;
        }
    }
}
