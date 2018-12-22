using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case run - extended test case for run
    /// </summary>
    public class TestCaseRun
    {
        public TestCaseRun(TestCase testCase, List<StepRun> steps, TestRun testRun)
        {
            TestCase = testCase ?? throw new ArgumentException("Test case is not specified", "testCase");            
            Steps = steps;
            Status = TestCaseStatus.None;
            TestRun = testRun ?? throw new ArgumentException("Test run is not specified", "testRun");
        }

        /// <summary>
        /// Test case
        /// </summary>
        public TestCase TestCase { get; private set; }

        /// <summary>
        /// Status
        /// </summary>
        public TestCaseStatus Status { get; private set; }

        /// <summary>
        /// Steps
        /// </summary>
        public List<StepRun> Steps { get; private set; }

        /// <summary>
        /// Parent test run
        /// </summary>
        public TestRun TestRun { get; private set; }
    }
}
