using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Project - parent of all suites
    /// </summary>
    public class ProjectAggregate
    {
        private List<TestRun> _testRuns;
        private List<Suit> _suits;

        /// <summary>
        /// Project id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Project creation date
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Root suit 
        /// </summary>
        public Suit RootSuit { get; private set; }

        /// <summary>
        /// Test runs
        /// </summary>
        public List<TestRun> TestRuns => _testRuns;

        /// <summary>
        /// Project suits
        /// </summary>
        public List<Suit> Suits => _suits;

        // for ef core
        private ProjectAggregate() { }

        public ProjectAggregate(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;

            RootSuit = new Suit("root", "root", 0);
            _testRuns = new List<TestRun>();
            _suits = new List<Suit>() { };
        }

        /// <summary>
        /// Add new suit to the project
        /// </summary>
        /// <param name="name">suit name</param>
        /// <param name="description">suit description</param>
        /// <param name="order">suit order</param>
        /// <param name="parentSuitId">parent suit id, if it is not specified then root suit</param>                        
        /// <returns>Added suit</returns>
        public Suit AddSuit(string name, string description, long? parentSuitId = null)
        {
            var parentSuit = (parentSuitId.HasValue
                            ? _suits.FirstOrDefault(s => s.Id == parentSuitId)
                            : RootSuit)
                            ?? RootSuit; // by default root
            var order = _suits.Any(s => s.Id == parentSuit.Id) 
                    ? _suits.Where(s => s.Id == parentSuit.Id).Max(s => s.Order) + 1
                    : 0;
            var suit = new Suit(name, description, order, parentSuit);
            _suits.Add(suit);

            return suit;
        }

        /// <summary>
        /// Add new test case to specified suit in the project
        /// </summary>
        /// <param name="name">test case name</param>
        /// <param name="description">test case description</param>
        /// <param name="expectedResult">test case expected result</param>
        /// <param name="suitId">test case suit id</param>
        /// <param name="steps">test case steps</param>
        public TestCase AddTestCase(string name, string description, string expectedResult, long suitId, List<Step> steps)
        {
            var suit = RootSuit.Id == suitId 
                    ? RootSuit 
                    : _suits.FirstOrDefault(s => s.Id == suitId) 
                    ?? RootSuit; // by default root

            var order = suit.TestCases.Any() ? suit.TestCases.Max(tc => tc.Order) + 1 : 0;
            var testCase = new TestCase(name, description, expectedResult, suit, order);

            if (steps != null)
            {
                foreach (var step in steps)
                {
                    testCase.AddStep(step);
                }
            }

            suit.TestCases.Add(testCase);

            return testCase;
        }

        /// <summary>
        /// Add step to test case
        /// </summary>
        /// <param name="testCaseId"></param>
        /// <param name="step"></param>
        public void AddStep(long testCaseId, Step step)
        {
            var testCase = _findTestCase(testCaseId);
            testCase.AddStep(step);
        }

        /// <summary>
        /// Add test run
        /// </summary>
        /// <param name="name">Name of the run</param>
        /// <param name="description">Description of the run</param>
        /// <param name="testCaseIds">Test cases to include into the run</param>
        public TestRun AddTestRun(string name, string description, HashSet<long> testCaseIds)
        {
            if (testCaseIds == null) throw new ArgumentNullException(nameof(testCaseIds));

            var testCases = _getAllTestCases(tc => testCaseIds.Contains(tc.Id));

            var testRunCases = testCases.Select(tc =>
                new TestRunCase(tc, tc.Steps.Select(s => new StepRun(s)).ToList()))
                    .ToList() ;

            var testRun = new TestRun(name, description, testRunCases);
            _testRuns.Add(testRun);

            return testRun;
        }

        /// <summary>
        /// Add test cases to test run
        /// </summary>
        /// <param name="testCaseIds">Test cases ids to add</param>
        /// <param name="testRunId">Test run id</param>
        public IEnumerable<TestRunCase> AddTestCasesToRun(HashSet<long> testCaseIds, long testRunId)
        {
            var testRun = _testRuns.FirstOrDefault(tr => tr.Id == testRunId)
                ?? throw new ArgumentException("Test run is not fount with specified id", nameof(testRunId));

            var testRunCases = _getAllTestCases(tc => testCaseIds.Contains(tc.Id))
                            .Except(testRun.TestCases.Select(trc => trc.TestCase)) //except already added
                            .Select(tc => new TestRunCase(tc, tc.Steps.Select(s => new StepRun(s)).ToList()));

            testRun.TestCases.AddRange(testRunCases);

            return testRunCases;
        }


        /// <summary>
        /// Look for test case in all suits
        /// </summary>
        /// <param name="id">Test case id</param>
        /// <returns></returns>
        private TestCase _findTestCase(long id)
        {
            foreach (var suit in _suits)
            {
                var testCase =suit.TestCases.FirstOrDefault(tc=>tc.Id == id);
                if (testCase != null) return testCase;
            }

            return null;
        }

        /// <summary>
        /// All test cases from all suits by predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>List of matches testCases</returns>
        private IEnumerable<TestCase> _getAllTestCases(Predicate<TestCase> predicate)
        {            
            var testCases = new List<TestCase>(RootSuit.TestCases.Where(t => predicate(t)));
            foreach(var suit in _suits)
            {
                testCases.AddRange(suit.TestCases.Where(t => predicate(t)));
            }
            return testCases.Where(t=> predicate(t));
        }
    }
}
