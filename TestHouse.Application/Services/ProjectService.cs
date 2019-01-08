using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Domain.Models;


namespace TestHouse.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectAggregate> AddProject(string name, string description)
        {
            var project = new ProjectAggregate(name, description);

            _projectRepository.Add(project);
            await _projectRepository.SaveAsync();

            return project;
        }
    }
}
