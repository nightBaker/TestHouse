using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestHouse.Domain.Tests")]
namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case 
    /// </summary>
    public class TestCase
    {

        //for ef
        private TestCase() { }

        public TestCase(string name, string description, string expectedResult, Suit suit, int order)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");
            if (string.IsNullOrEmpty(expectedResult))
                throw new ArgumentException("Expected result is not specified", "expectedResult");

            Name = name;
            Description = description;
            Order = order;
            CreatedAt = DateTime.UtcNow;
            ExpectedResult = expectedResult;
            Steps = new List<Step>();

            Suit = suit ?? throw new ArgumentException("Test case must belogs to suit", "suit");

        }

        /// <summary>
        /// Test case id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Test case name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Test case description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Test suite (Parent category)
        /// </summary>
        public Suit Suit { get; private set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Test case expected result
        /// </summary>
        public string ExpectedResult { get; private set; }

        /// <summary>
        /// Order in a suit
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Test case steps
        /// </summary>
        public List<Step> Steps { get; private set; }

        /// <summary>
        /// Add step
        /// </summary>
        /// <param name="step"></param>
        internal void AddStep(Step step)
        {
            Steps.Add(step);
        }

        /// <summary>
        /// Add steps
        /// </summary>
        /// <param name="steps"></param>
        internal void AddSteps(IEnumerable<Step> steps)
        {
            Steps.AddRange(steps);
        }

    }
}
