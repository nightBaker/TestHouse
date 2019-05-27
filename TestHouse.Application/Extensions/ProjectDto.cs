using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Extensions
{
    

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
                Description = item.Description,
                CreatedAt = item.CreatedAt
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
            dto.RootSuit.TestCases = item.RootSuit.TestCases.ToTestCasesDto();

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
                    suitDto.TestCases = project.Suits.First(s => s.Id == suitDto.Id).TestCases.ToTestCasesDto();
                }

                return suitDtos;
            }

            return null;
        }
    }
}
