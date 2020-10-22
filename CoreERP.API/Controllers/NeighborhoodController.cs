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
    public class NeighborhoodController : Controller
    {

        private readonly INeighborhoodRepository _INeighborhood;

        public NeighborhoodController(INeighborhoodRepository neighborhoodRepository)
        {
            _INeighborhood = neighborhoodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNeighborhoods()
        {
            return Ok(await _INeighborhood.GetAllNeighborhoods());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNeighborhoodDetails(int id)
        {
            return Ok(await _INeighborhood.GetNeighborhoodDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNeighborhood([FromBody] Neighborhood neighborhood)
        {
            if (neighborhood == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _INeighborhood.InsertNeighborhood(neighborhood);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateNeighborhood([FromBody] Neighborhood neighborhood)
        {
            if (neighborhood == null)
                return BadRequest();

            if (neighborhood.barrio.Trim() == string.Empty)
            {
                ModelState.AddModelError("Area", "Area name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _INeighborhood.UpdateNeighborhood(neighborhood);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNeighborhood(int id)
        {
            if (id == 0)
                return BadRequest();

            await _INeighborhood.DeleteNeighborhood(id);

            return NoContent(); //success
        }

    }
}
