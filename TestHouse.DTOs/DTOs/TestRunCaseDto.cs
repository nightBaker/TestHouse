using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.DTOs.DTOs
{
    public class TestRunCaseDto
    {
        /// <summary>
        /// Test run case id
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        /// Test case
        /// </summary>
        public TestCaseDto TestCase { get; internal set; }

        /// <summary>
        /// Status
        /// </summary>
        //public TestCaseStatus Status { get; internal set; }

        ///// <summary>
        ///// Steps
        ///// </summary>
        //public IEnumerable<StepRunDto> Steps { get; internal set; }
    }

    
}
