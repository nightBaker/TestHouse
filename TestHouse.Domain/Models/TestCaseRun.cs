using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case run
    /// </summary>
    public class TestCaseRun : TestCase
    {
        public TestCaseRun(string name, string description, string expectedResult, Suit suit)
            : base(name, description, expectedResult, suit)
        {
        }

        /// <summary>
        /// Status
        /// </summary>
        public TestCaseStatus Status { get; private set; }
                
    }
}
