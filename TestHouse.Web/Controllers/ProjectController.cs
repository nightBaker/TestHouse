using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestHouse.Application.Extensions;
using TestHouse.Application.Services;
using TestHouse.DTOs.DTOs;
using TestHouse.DTOs.Models;

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
        [ProducesResponseType(201)]
        public async Task<ActionResult<ProjectDto>> Post(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                var projectDto = await _projectService.AddProjectAsync(model.Name, model.Description);

                return CreatedAtAction("Get", new { id = projectDto.Id }, projectDto);

            }

            return BadRequest();
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>Projects dto</returns>
        [HttpGet("all")]
        public async Task<IEnumerable<ProjectDto>> All()
        {
            return await _projectService.GetAllAsync();           
        }

        /// <summary>
        /// Get project
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Project dto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectAggregateDto>> Get(long id)
        {
            var project = await _projectService.GetAsync(id);

            if (project == null) return NotFound();

            return Ok(project);
        }

        /// <summary>
        /// Update project info
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="model">Project model</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ProjectDto>> Patch(long id, ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.UpdateProject(id,model.Name, model.Description);

                return NoContent();
            }

            return BadRequest();
        }

    }
}