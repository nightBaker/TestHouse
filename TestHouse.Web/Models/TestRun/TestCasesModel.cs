using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.Web.Models.TestRun
{
    public class TestCasesModel
    {
        /// <summary>
        /// Project id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// Test run id
        /// </summary>
        public long TestRunId { get; set; }

        /// <summary>
        /// Test case for adding
        /// </summary>
        public HashSet<long> TestCasesIds { get; set; }
    }
}
