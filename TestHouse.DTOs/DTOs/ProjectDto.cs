using System;

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

    }

    public class ProjectAggregateDto : ProjectDto
    {
        public SuitDto RootSuit { get; set; }
    }


}
