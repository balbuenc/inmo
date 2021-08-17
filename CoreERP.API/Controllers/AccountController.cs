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
    public class AccountController : Controller
    {
        private readonly IAccountRepository _AccountRepository;

        public AccountController(IAccountRepository accountTypeRepository)
        {
            _AccountRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _AccountRepository.GetAllAccounts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountDetails(int id)
        {
            return Ok(await _AccountRepository.GetAccountDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (account == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _AccountRepository.InsertAccount(account);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] Account accountType)
        {
            if (accountType == null)
                return BadRequest();

            if (accountType.denominacion.Trim() == string.Empty)
            {
                ModelState.AddModelError("Account", "Account name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _AccountRepository.UpdateAccount(accountType);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (id == 0)
                return BadRequest();

            await _AccountRepository.DeleteAccount(id);

            return NoContent(); //success
        }
    }
}
