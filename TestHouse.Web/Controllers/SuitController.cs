using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestHouse.Application.Models;
using TestHouse.Application.Services;
using TestHouse.Web.Models.Suit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestHouse.Web.Controllers
{
    [Route("api/[controller]")]
    public class SuitController : Controller
    {
        private SuitService _suitService;

        public SuitController(SuitService suitService)
        {
            _suitService = suitService;
        }

        /// <summary>
        /// Add new suit
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New suit id</returns>
        [HttpPost]
        public async Task<ActionResult<SuitDto>> Post([FromBody]SuitModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var suit = await _suitService.AddSuitAsync(model.Name, model.Description, model.ProjectId, model.ParentId);

            return Created("", suit);
        }

     
    }
}
