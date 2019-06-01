using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Extensions;
using TestHouse.Application.Services;
using TestHouse.DTOs.DTOs;
using TestHouse.Web.Models.TestRun;

namespace TestHouse.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRunController : ControllerBase
    {
        private TestRunService _runService;

        public TestRunController(TestRunService runService)
        {
            _runService = runService;
        }
        /// <summary>
        /// Add new test run for project
        /// </summary>
        /// <param name="model">Test run model</param>
        /// <returns>Added test run</returns>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<TestRunDto>> Post([FromBody] TestRunModel model)
        {
            if (ModelState.IsValid)
            {
                var testRunDto = await _runService.AddTestRunAsync(model.ProjectId,
                                                    model.Name, model.Description,
                                                    model.TestCasesIds);

                return Created("", testRunDto);

            }

            return BadRequest();
        }
        
        /// <summary>
        /// Add test cases to existing project
        /// </summary>
        /// <param name="model">Test cases model</param>
        /// <returns>Added test cases</returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<IEnumerable<TestRunCaseDto>>> AddTestCases( [FromBody] TestCasesModel model)
        {
            if (ModelState.IsValid)
            {
                var testCases = await _runService.AddTestCases(
                            model.ProjectId, model.TestRunId, model.TestCasesIds);

                return Created("", testCases); 
            }

            return BadRequest();
        }
    }
}
