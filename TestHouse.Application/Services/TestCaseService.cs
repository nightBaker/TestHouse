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
    public class TestCaseService
    {
        private readonly TestHouseDbContext _dbContext;

        public TestCaseService(TestHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add new test case
        /// </summary>
        /// <param name="name">name of the test case</param>
        /// <param name="description">description of the test case</param>
        /// <param name="expectedResult">expected result of the test case</param>
        /// <param name="suitId">parent suit id</param>
        /// <returns>created test case</returns>
        public async Task<TestCase> AddTestCase(string name, string description, string expectedResult, long suitId, List<Step> steps)
        {
            var suit = await _dbContext.Suits
                .Include(s=>s.TestCases)
                .FirstOrDefaultAsync(s=>s.Id == suitId)
                ?? throw new ArgumentException("Suit with specified id is not found", nameof(suitId));

            var order = suit.TestCases.Max(t => t.Order) + 1;
            var testCase = new TestCase(name, description, expectedResult, suit, order);

            if(steps != null) testCase.AddSteps(steps);

            _dbContext.TestCases.Add(testCase);

            return testCase;
        }        

    }
}
