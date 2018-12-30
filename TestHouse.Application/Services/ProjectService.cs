using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Models;
using TestHouse.Persistence;

namespace TestHouse.Application.Services
{
    public class ProjectService
    {
        private readonly TestHouseDbContext _projectDbContext;

        public ProjectService(TestHouseDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<ProjectAggregate> AddProject(string name, string description)
        {
            var project = new ProjectAggregate(name, description);

            _projectDbContext.Projects.Add(project);
            await _projectDbContext.SaveChangesAsync();

            return project;
        }
    }
}
