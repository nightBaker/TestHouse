using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Enums;
using TestHouse.Domain.Models;
using TestHouse.Persistence;

namespace TestHouse.Application.Services
{
    public class TestCaseRunService
    {
        private readonly TestHouseDbContext _dbContext;

        public TestCaseRunService(TestHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Change status for test run test case
        /// </summary>
        /// <param name="testRunCaseId">Test run case id</param>
        /// <param name="status">New status</param>
        /// <returns>Test run case with new status</returns>
        public async Task<TestRunCase> ChangeStatus(long testRunCaseId, TestCaseStatus status)
        {
            var testRunCase = await _dbContext.TestRunCases.Where(trc => trc.Id == testRunCaseId).SingleOrDefaultAsync();

            testRunCase.Status = status;

            return testRunCase;
        }
    }
}
