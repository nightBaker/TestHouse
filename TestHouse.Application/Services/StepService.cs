using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Domain.Models;

namespace TestHouse.Application.Services
{
    public class StepService
    {
        private readonly IProjectRepository _projectRepository;

        public StepService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        /// <summary>
        /// Add new steps to existing test case
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="testCaseId">Test case id</param>
        /// <param name="step">Step to add</param>
        /// <returns></returns>
        public async Task AddStep(long projectId, long testCaseId, Step step)
        {
            if (step == null) throw new ArgumentNullException("Step is not specified", nameof(step));

            var project = await _projectRepository.GetAsync(projectId) 
                    ?? throw new ArgumentException("Project is not found with specified id", nameof(projectId)) ;

            project.AddStep(testCaseId, step);            
        }
    }
}
