using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginController : Controller
    {
        private readonly IOriginRepository _OriginRepository;

        public OriginController(IOriginRepository originRepository)
        {
            _OriginRepository = originRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrigins()
        {
            return Ok(await _OriginRepository.GetAllOrigins());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOriginDetails(int id)
        {
            return Ok(await _OriginRepository.GetOriginDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrigin([FromBody] Origin origin)
        {
            if (origin == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _OriginRepository.InsertOrigin(origin);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrigin([FromBody] Origin origin)
        {
            if (origin == null)
                return BadRequest();

            if (origin.origen.Trim() == string.Empty)
            {
                ModelState.AddModelError("Origin", "Origin name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _OriginRepository.UpdateOrigin(origin);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrigin(int id)
        {
            if (id == 0)
                return BadRequest();

            await _OriginRepository.DeleteOrigin(id);

            return NoContent(); //success
        }

    }
}
