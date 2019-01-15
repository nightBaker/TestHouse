using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestHouse.Application.Models;
using TestHouse.Application.Services;
using TestHouse.Web.Models.Project;

namespace TestHouse.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="model">ProjectModel</param>
        /// <returns>Created project dto</returns>
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ProjectDto>> Post(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                return await _projectService.AddProjectAsync(model.Name, model.Description);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>Projects dto</returns>
        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> All()
        {
            return await _projectService.GetAllAsync();           
        }

        
    }
}