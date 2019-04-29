using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Domain.Models;

namespace TestHouse.Web.Models.TestCase
{
    public class TestCaseModel
    {
        /// <summary>
        /// Test case name
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// Test case description
        /// </summary>
        public string Description { get;  set; }

        /// <summary>
        /// Test suite id (Parent)
        /// </summary>
        public long SuitId { get;  set; }

        /// <summary>
        /// Project id (Parent)
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// Test case expected result
        /// </summary>
        public string ExpectedResult { get;  set; }
        
        /// <summary>
        /// Test case steps
        /// </summary>
        public List<Step> Steps { get;  set; }
    }
}
