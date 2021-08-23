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
    public class AccountDetailsController : Controller
    {
        private readonly IAccountDetailRepository _AccountDetailRepository;

        public AccountDetailsController(IAccountDetailRepository accountDetailRepository)
        {
            _AccountDetailRepository = accountDetailRepository;
        }

        //[HttpGet("{accountID}")]
        //public async Task<IActionResult> GetAllAccountDetails(int accountID)
        //{
        //    return Ok(await _AccountDetailRepository.GetAllAccountDetails(accountID));
        //}

        [HttpGet("{AccountID}")]
        public async Task<IActionResult> GetAccountDetailDetails(int accountID)
        {
            return Ok(await _AccountDetailRepository.GetAllAccountDetails(accountID));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountDetail([FromBody] AccountDetails accountDetail)
        {
            if (accountDetail == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _AccountDetailRepository.InsertAccountDetail(accountDetail);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountDetail([FromBody] AccountDetails accountDetail)
        {
            if (accountDetail == null)
                return BadRequest();

            if (accountDetail.nro_cuenta.Trim() == string.Empty)
            {
                ModelState.AddModelError("AccountDetail", "Account number shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _AccountDetailRepository.UpdateAccountDetail(accountDetail);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDetail(int id)
        {
            if (id == 0)
                return BadRequest();

            await _AccountDetailRepository.DeleteAccountDetail(id);

            return NoContent(); //success
        }
    }
}
