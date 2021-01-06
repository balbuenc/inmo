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
    public class CivilStatusController : Controller
    {

        private readonly ICivilStatusRepository _CivilStatusRepository;

        public CivilStatusController(ICivilStatusRepository civilStatusRepository)
        {
            _CivilStatusRepository = civilStatusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            return Ok(await _CivilStatusRepository.GetAllCivilStatus());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCivilStatusDetails(int id)
        {
            return Ok(await _CivilStatusRepository.GetCivilStatusDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCivilStatus([FromBody] CivilStatus civilStatus)
        {
            if (civilStatus == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CivilStatusRepository.InsertCivilStatus(civilStatus);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCivilStatus([FromBody] CivilStatus civilStatus)
        {
            if (civilStatus == null)
                return BadRequest();

            if (civilStatus.estado_civil.Trim() == string.Empty)
            {
                ModelState.AddModelError("Cuty", "CivilStatus name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CivilStatusRepository.UpdateCivilStatus(civilStatus);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCivilStatus(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CivilStatusRepository.DeleteCivilStatus(id);

            return NoContent(); //success
        }
    }
}
