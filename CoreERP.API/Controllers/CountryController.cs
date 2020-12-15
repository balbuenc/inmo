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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _CountryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _CountryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _CountryRepository.GetAllCountries());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryDetails(int id)
        {
            return Ok(await _CountryRepository.GetCountryDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CountryRepository.InsertCountry(country);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest();

            if (country.pais.Trim() == string.Empty)
            {
                ModelState.AddModelError("Country", "Country name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CountryRepository.UpdateCountry(country);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CountryRepository.DeleteCountry(id);

            return NoContent(); //success
        }

    }
}
