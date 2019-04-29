using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.Web.Models.TestRun
{
    public class TestRunModel
    {
        /// <summary>
        /// Project id
        /// </summary>
        [Required]
        public long ProjectId { get; internal set; }

        /// <summary>
        /// Test run name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; internal set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Test cases ids for including into test run
        /// </summary>
        public HashSet<long> TestCasesIds { get; internal set; }
    }
}
