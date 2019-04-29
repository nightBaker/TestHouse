using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.Domain.Models
{
    /// <summary>
    /// Test case step
    /// </summary>
    public class Step
    {
        //for ef
        private Step() { }

        public Step(int order, string description, string expectedResult)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentException("Description is not specified", "description");

            Order = order;
            Description = description;
            ExpectedResult = expectedResult;
        }

        /// <summary>
        /// Step id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Step order
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Step description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Step expected result
        /// </summary>
        public string ExpectedResult { get; private set; }
    }
}
