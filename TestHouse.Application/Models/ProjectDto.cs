using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Models
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

    public static class ProjectAggregateExtensions
    {
        public static IEnumerable<ProjectDto> ToProjectsDto(this IEnumerable<ProjectAggregate> projectAggregates)
        {
            foreach (var item in projectAggregates)
            {
                yield return item.ToProjectDto();
            }
        }

        public static ProjectDto ToProjectDto(this ProjectAggregate item)
        {
            return new ProjectDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }
    }
}
