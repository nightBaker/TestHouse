using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHouse.Application.Models;
using TestHouse.Application.Services;
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

        [HttpPost]
        [ProducesResponseType(400)]
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
    }
}
