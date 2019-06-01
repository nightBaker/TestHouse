using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.DTOs.Models
{
    public class TestRunModel
    {
        /// <summary>
        /// Project id
        /// </summary>
        [Required]
        public int ProjectId { get;  set; }

        /// <summary>
        /// Test run name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get;  set; }

        /// <summary>
        /// Test run description
        /// </summary>
        public string Description { get;  set; }

        /// <summary>
        /// Test cases ids for including into test run
        /// </summary>
        public HashSet<long> TestCasesIds { get;  set; }
    }
}
