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
    public class TestCaseController : ControllerBase
    {
        private TestCaseService _testCaseService;

        public TestCaseController(TestCaseService testCaseService)
        {
            _testCaseService = testCaseService;
        }

        /// <summary>
        /// Add new test case to the project
        /// </summary>
        /// <param name="model">Test case model</param>
        /// <returns>Created test case</returns>
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TestCaseDto>> Post([FromBody] TestCaseModel model)
        {
            if (ModelState.IsValid)
            {
                var testCase = await _testCaseService.AddTestCaseAsync(model.Name,
                                                                model.Description,
                                                                model.ExpectedResult,
                                                                model.ProjectId,
                                                                model.SuitId,
                                                                model.Steps
                                                                ?.Select(s=> new Domain.Models.Step(s.Order,s.Description,s.ExpectedResult))
                                                                .ToList());

                return Created("",testCase);

            }

            return BadRequest();
        }                
    }
}