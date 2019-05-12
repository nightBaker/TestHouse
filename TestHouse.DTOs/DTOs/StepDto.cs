using System;
using System.Collections.Generic;
using System.Text;

namespace TestHouse.DTOs.DTOs
{
    public class StepDto
    {
        /// <summary>
        /// Step id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Step order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Step description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Step expected result
        /// </summary>
        public string ExpectedResult { get; set; }
    }
}
