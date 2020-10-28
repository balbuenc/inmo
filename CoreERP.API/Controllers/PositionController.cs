using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly IPositionRepository _PositioRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _PositioRepository = positionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            return Ok(await _PositioRepository.GetAllPositions());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionDetails(int id)
        {
            return Ok(await _PositioRepository.GetPositionDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] Position position)
        {
            if (position == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PositioRepository.InsertPosition(position);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePosition([FromBody] Position position)
        {
            if (position == null)
                return BadRequest();

            if (position.cargo.Trim() == string.Empty)
            {
                ModelState.AddModelError("Position", "Position name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PositioRepository.UpdatePosition(position);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PositioRepository.DeletePosition(id);

            return NoContent(); //success
        }
    }
}
