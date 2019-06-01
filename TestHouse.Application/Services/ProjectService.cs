using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Extensions;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="name">Name of the project</param>
        /// <param name="description">Description of the project</param>
        /// <returns>Created project dto</returns>
        public async Task<ProjectDto> AddProjectAsync(string name, string description)
        {
            var project = new ProjectAggregate(name, description);

            _projectRepository.Add(project);
            await _projectRepository.SaveAsync();

            return project.ToProjectDto();
        }

        /// <summary>
        /// Get all existing projects
        /// </summary>
        /// <returns>List of projects dto</returns>
        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projectAggregates = await _projectRepository.GetAllAsync();
            return projectAggregates.ToProjectsDto();
        }

        /// <summary>
        /// Get project aggregate
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns></returns>
        public async Task<ProjectAggregateDto> GetAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            return project?.ToProjectAggregateDto();
        }

        /// <summary>
        /// Update project info
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <returns></returns>
        public async Task UpdateProject(long id, string name, string description)
        {
            var project = await _projectRepository.GetAsync(id);
            project.UpdateInfo(name, description);

            await _projectRepository.SaveAsync();
        }
    }
}
