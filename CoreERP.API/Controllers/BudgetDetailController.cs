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
    public class BudgetDetailController : Controller
    {
       
        private readonly IBudgetDetailRepository _BudgetDetailRepository;

        public BudgetDetailController(IBudgetDetailRepository budgetDetailRepository)
        {
            _BudgetDetailRepository = budgetDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBudgetDetails()
        {
            return Ok(await _BudgetDetailRepository.GetAllBudgetDetails());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetDetails(int id)
        {
            return Ok(await _BudgetDetailRepository.GetBudgetDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudgetDetail([FromBody] BudgetDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _BudgetDetailRepository.InsertBudgetDetail(budgetDetails);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBudgetDetail([FromBody] BudgetDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (budgetDetails.precio.ToString()  == null)
            {
                ModelState.AddModelError("BudgetDetail", "BudgetDetail price shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _BudgetDetailRepository.UpdateBudgetDetail(budgetDetails);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetDetail(int id)
        {
            if (id == 0)
                return BadRequest();

            await _BudgetDetailRepository.DeleteBudgetDetail(id);

            return NoContent(); //success
        }

    }
}
