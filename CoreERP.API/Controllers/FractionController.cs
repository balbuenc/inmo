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
    public class FractionController : Controller
    {
        private readonly IFractionRepository _FractionRepository;

        public FractionController(IFractionRepository accountTypeRepository)
        {
            _FractionRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFractions()
        {
            return Ok(await _FractionRepository.GetAllFractions());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFractionDetails(int id)
        {
            return Ok(await _FractionRepository.GetFractionDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFraction([FromBody] Fraction fraction)
        {
            if (fraction == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _FractionRepository.InsertFraction(fraction);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateFraction([FromBody] Fraction fraction)
        {
            if (fraction == null)
                return BadRequest();

            if (fraction.fraccion == 0)
            {
                ModelState.AddModelError("Fraction", "Fraction name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _FractionRepository.UpdateFraction(fraction);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFraction(int id)
        {
            if (id == 0)
                return BadRequest();

            await _FractionRepository.DeleteFraction(id);

            return NoContent(); //success
        }
    }
}
