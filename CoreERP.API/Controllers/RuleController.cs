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
    public class RuleController : Controller
    {
        private readonly IRuleRepository _RuleRepository;

        public RuleController(IRuleRepository accountTypeRepository)
        {
            _RuleRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRules()
        {
            return Ok(await _RuleRepository.GetAllRules());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRuleDetails(int id)
        {
            return Ok(await _RuleRepository.GetRuleDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRule([FromBody] Rule account)
        {
            if (account == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _RuleRepository.InsertRule(account);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateRule([FromBody] Rule account)
        {
            if (account == null)
                return BadRequest();

            if (account.regla.Trim() == string.Empty)
            {
                ModelState.AddModelError("Rule", "Rule name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _RuleRepository.UpdateRule(account);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            if (id == 0)
                return BadRequest();

            await _RuleRepository.DeleteRule(id);

            return NoContent(); //success
        }

    }
}
