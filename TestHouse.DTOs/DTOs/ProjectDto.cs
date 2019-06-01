using System;
using System.Collections.Generic;

namespace TestHouse.DTOs.DTOs
{
    public class ProjectDto
    {
        /// <summary>
        /// Project id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreatedAt { get; set; }

    }

    public class ProjectAggregateDto : ProjectDto
    {
        public SuitDto RootSuit { get; set; }
        public IEnumerable<TestRunDto> TestRuns { get; set; }
    }


}
