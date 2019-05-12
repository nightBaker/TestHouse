using System;
using System.Collections.Generic;
using System.Text;


namespace TestHouse.DTOs.DTOs
{
    public class TestRunDto
    {
        /// <summary>
        /// Test run id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Test run name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// List of test cases
        /// </summary>
        public IEnumerable<TestRunCaseDto> TestCases { get; internal set; }
    }

    
}
