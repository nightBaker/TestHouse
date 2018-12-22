using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    public class TestRun
    {
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
        public List<TestCase> TestCases { get; private set; }
    }
}
