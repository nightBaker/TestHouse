using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case 
    /// </summary>
    public class TestCase
    {
        private List<Step> _steps;

        public TestCase(string name, string description, string expectedResult, Suit suit, uint order)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");
            if (string.IsNullOrEmpty(expectedResult))
                throw new ArgumentException("Expected result is not specified", "expectedResult");

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            ExpectedResult = expectedResult;
            _steps = new List<Step>();

            Suit = suit ?? throw new ArgumentException("Test case must belogs to suit", "suit");

        }

        /// <summary>
        /// Test case id
        /// </summary>
        public long Id { get; private set; }

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
        public uint Order { get; private set; }

        /// <summary>
        /// Test case steps
        /// </summary>
        public IReadOnlyCollection<Step> Steps => _steps;

        public void AddStep(Step step)
        {
            _steps.Add(step);
        }

    }
}
