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
    public class CityController : Controller
    {
        private readonly ICityRepository _CityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _CityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            return Ok(await _CityRepository.GetAllCities());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityDetails(int id)
        {
            return Ok(await _CityRepository.GetCityDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (city == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CityRepository.InsertCity(city);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] City city)
        {
            if (city == null)
                return BadRequest();

            if (city.ciudad.Trim() == string.Empty)
            {
                ModelState.AddModelError("Cuty", "City name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CityRepository.UpdateCity(city);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CityRepository.DeleteCity(id);

            return NoContent(); //success
        }

    }
}
