using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ProjectAggregateDto: ProjectDto
    {
        public SuitDto RootSuit { get; set; }
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

        public static ProjectAggregateDto ToProjectAggregateDto(this ProjectAggregate item)
        {
            var dto = new ProjectAggregateDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                RootSuit = item.RootSuit.ToSuitDto(),
            };

            dto.RootSuit.Suits = _fillSuitsTree(dto.RootSuit.Id, item);

            return dto;
        }

        /// <summary>
        /// Recursive method for filling suits tree
        /// </summary>
        /// <param name="parentId">Parent id</param>
        /// <param name="project">Project aggregate</param>
        /// <returns>List of child suits</returns>
        private static IEnumerable<SuitDto> _fillSuitsTree(long parentId, ProjectAggregate project)
        {
            var suitDtos = project.Suits.Where(s => s.ParentSuit.Id == parentId).ToSuitsDto().ToList();
            if (suitDtos.Any())
            {
                foreach (var suitDto in suitDtos)
                {
                    suitDto.Suits = _fillSuitsTree(suitDto.Id, project);                    
                }

                return suitDtos;
            }

            return null;
        }
    }
}
