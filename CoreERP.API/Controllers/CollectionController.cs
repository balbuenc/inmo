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
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository  _CollectionRepository;

        public CollectionController(ICollectionRepository areaRepository)
        {
            _CollectionRepository = areaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCollections()
        {
            return Ok(await _CollectionRepository.GetAllCollections());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollectionDetails(int id)
        {
            return Ok(await _CollectionRepository.GetCollectionDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] Collection area)
        {
            if (area == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CollectionRepository.InsertCollection(area);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCollection([FromBody] Collection collection)
        {
            if (collection == null)
                return BadRequest();

            if (collection.id_cuota == 0)
            {
                ModelState.AddModelError("Collection", "Collection quote id shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CollectionRepository.UpdateCollection(collection);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CollectionRepository.DeleteCollection(id);

            return NoContent(); //success
        }

    }
}
