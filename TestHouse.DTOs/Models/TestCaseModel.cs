using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.DTOs.Models
{
    public class TestCaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Test suite id (Parent)
        /// </summary>
        public long SuitId { get; set; }

        /// <summary>
        /// Project id (Parent)
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// Test case expected result
        /// </summary>
        public string ExpectedResult { get; set; }

        /// <summary>
        /// Test case steps
        /// </summary>
        public List<StepModel> Steps { get; set; }
    }
}
