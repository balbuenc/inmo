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
    public class AreaController : Controller
    {

        private readonly IAreaRepository _AreaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            _AreaRepository = areaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAreas()
        {
            return Ok(await _AreaRepository.GetAllAreas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaDetails(int id)
        {
            return Ok(await _AreaRepository.GetAreaDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArea([FromBody] Area area)
        {
            if (area == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _AreaRepository.InsertArea(area);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateArea([FromBody] Area area)
        {
            if (area == null)
                return BadRequest();

            if (area.area.Trim() == string.Empty)
            {
                ModelState.AddModelError("Area", "Area name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _AreaRepository.UpdateArea(area);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (id == 0)
                return BadRequest();

            await _AreaRepository.DeleteArea(id);

            return NoContent(); //success
        }

    }
}
