using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.DTOs.Models
{
    public class StepModel
    {
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
