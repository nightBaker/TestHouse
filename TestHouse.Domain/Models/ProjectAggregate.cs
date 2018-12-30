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
        public IReadOnlyCollection<TestRun> TestRuns => _testRuns;

        /// <summary>
        /// Project suits
        /// </summary>
        public IReadOnlyCollection<Suit> Suits => _suits;

        public ProjectAggregate(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;

            RootSuit = new Suit("root", "root", 0);
            _testRuns = new List<TestRun>();
            _suits = new List<Suit>();
        }

        /// <summary>
        /// Add new suit to the project
        /// </summary>
        /// <param name="name">suit name</param>
        /// <param name="description">suit description</param>
        /// <param name="order">suit order</param>
        /// <param name="parentSuitId">parent suit id, if it is not specified then root suit</param>
        public void AddSuit(string name, string description, long? parentSuitId = null)
        {
            var parentSuit = parentSuitId.HasValue
                            ? _suits.FirstOrDefault(s => s.Id == parentSuitId)
                            : RootSuit
                            ?? throw new ArgumentException("Project does not have suit with specified parentSuitId", nameof(parentSuitId));
            var order = _suits.Any(s => s.Id == parentSuit.Id) 
                    ? _suits.Where(s => s.Id == parentSuit.Id).Max(s => s.Order) + 1
                    : 0;
            var suit = new Suit(name, description, order, parentSuit);
            _suits.Add(suit);           
        }

        /// <summary>
        /// Add new test case to specified suit in the project
        /// </summary>
        /// <param name="name">test case name</param>
        /// <param name="description">test case description</param>
        /// <param name="expectedResult">test case expected result</param>
        /// <param name="suitId">test case suit id</param>
        /// <param name="steps">test case steps</param>
        public void AddTestCase(string name, string description, string expectedResult, long suitId, List<Step> steps)
        {
            var suit = _suits.FirstOrDefault(s => s.Id == suitId) ?? throw new ArgumentException("Suit is not found with specified suitId", nameof(suitId));
            var order = suit.TestCases.Any() ? suit.TestCases.Max(tc => tc.Order) + 1 : 0;
            var testCase = new TestCase(name, description, expectedResult, suit, order);

            foreach(var step in steps)
            {
                testCase.AddStep(step);
            }

            suit.TestCases.Add(testCase);
        }
    }
}
