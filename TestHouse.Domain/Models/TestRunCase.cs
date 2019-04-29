using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case run - extended test case for run
    /// </summary>
    public class TestRunCase
    {
        //for ef core
        private TestRunCase() { }

        public TestRunCase(TestCase testCase, List<StepRun> steps)
        {
            TestCase = testCase ?? throw new ArgumentNullException("Test case is not specified", nameof(testCase));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
            Status = TestCaseStatus.None;            
        }

        /// <summary>
        /// Test run case id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Test case
        /// </summary>
        public TestCase TestCase { get; private set; }

        /// <summary>
        /// Status
        /// </summary>
        public TestCaseStatus Status { get; internal set; }

        /// <summary>
        /// Steps
        /// </summary>
        public List<StepRun> Steps { get; private set; }
        
    }
}
