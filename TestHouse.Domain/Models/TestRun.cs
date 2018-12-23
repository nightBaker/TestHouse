using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    public class TestRun
    {
        public TestRun(string name, string description, List<TestCaseRun> testCases)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");

            Name = name;
            Description = description;
            TestCases = testCases ?? new List<TestCaseRun>();
        }

        /// <summary>
        /// Test run id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Test run name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// List of test cases
        /// </summary>
        public List<TestCaseRun> TestCases { get; private set; }
    }
}
