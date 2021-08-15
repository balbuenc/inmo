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
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeRepository _AccountTypeRepository;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository)
        {
            _AccountTypeRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountTypes()
        {
            return Ok(await _AccountTypeRepository.GetAllAccountTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountTypeDetails(int id)
        {
            return Ok(await _AccountTypeRepository.GetAccountTypeDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountType([FromBody] AccountType accountType)
        {
            if (accountType == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _AccountTypeRepository.InsertAccountType(accountType);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountType([FromBody] AccountType accountType)
        {
            if (accountType == null)
                return BadRequest();

            if (accountType.tipo_cuenta.Trim() == string.Empty)
            {
                ModelState.AddModelError("AccountType", "AccountType name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _AccountTypeRepository.UpdateAccountType(accountType);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            if (id == 0)
                return BadRequest();

            await _AccountTypeRepository.DeleteAccountType(id);

            return NoContent(); //success
        }
    }
}
