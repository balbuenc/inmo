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
    public class BankController : Controller
    {
        private readonly IBankRepository _BankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _BankRepository = bankRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            return Ok(await _BankRepository.GetAllBanks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankDetails(int id)
        {
            return Ok(await _BankRepository.GetBankDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] Bank bank)
        {
            if (bank == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _BankRepository.InsertBank(bank);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBank([FromBody] Bank bank)
        {
            if (bank == null)
                return BadRequest();

            if (bank.banco.Trim() == string.Empty)
            {
                ModelState.AddModelError("Bank", "Bank name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _BankRepository.UpdateBank(bank);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (id == 0)
                return BadRequest();

            await _BankRepository.DeleteBank(id);

            return NoContent(); //success
        }
    }
}
