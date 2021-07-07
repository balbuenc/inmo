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
    public class PurchaseDetailController : Controller
    {

        private readonly IPurchaseDetailsRepository _PurchaseDetailsRepository;

        public PurchaseDetailController(IPurchaseDetailsRepository purchaseDetailsRepository)
        {
            _PurchaseDetailsRepository = purchaseDetailsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseDetailss()
        {
            return Ok(await _PurchaseDetailsRepository.GetAllPurchaseDetails());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseDetails(int id)
        {
            return Ok(await _PurchaseDetailsRepository.GetPurchaseDetails(id));
        }

       


        [HttpPost]
        public async Task<IActionResult> CreatePurchaseDetails([FromBody] PurchaseDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PurchaseDetailsRepository.InsertPurchaseDetail(budgetDetails);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePurchaseDetails([FromBody] PurchaseDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (budgetDetails.monto.ToString() == null)
            {
                ModelState.AddModelError("PurchaseDetails", "PurchaseDetails price shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PurchaseDetailsRepository.UpdatePurchaseDetail(budgetDetails);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseDetails(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PurchaseDetailsRepository.DeletePurchaseDetail(id);

            return NoContent(); //success
        }

    }
}
