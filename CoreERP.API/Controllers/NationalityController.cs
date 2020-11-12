using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalityController : Controller
    {
        private readonly INationalityRepository _NationalityRepository;

        public NationalityController(INationalityRepository nationalityRepository)
        {
            _NationalityRepository = nationalityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNationalitys()
        {
            return Ok(await _NationalityRepository.GetAllNationalitys());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNationalityDetails(int id)
        {
            return Ok(await _NationalityRepository.GetNationalityDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNationality([FromBody] Nationality nationality)
        {
            if (nationality == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _NationalityRepository.InsertNationality(nationality);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateNationality([FromBody] Nationality nationality)
        {
            if (nationality == null)
                return BadRequest();

            if (nationality.nacionalidad.Trim() == string.Empty)
            {
                ModelState.AddModelError("Nationality", "Nationality name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _NationalityRepository.UpdateNationality(nationality);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationality(int id)
        {
            if (id == 0)
                return BadRequest();

            await _NationalityRepository.DeleteNationality(id);

            return NoContent(); //success
        }
    }
}
