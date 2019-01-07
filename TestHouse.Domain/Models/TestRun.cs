using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    public class TestRun
    {
        //for ef
        private TestRun() { }

        public TestRun(string name, string description, List<TestRunCase> testCases)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is not specified", "name");            

            Name = name;
            Description = description;
            TestCases = testCases ?? throw new ArgumentNullException(nameof(testCases));           
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
        public List<TestRunCase> TestCases { get; private set; }        
    }
}
