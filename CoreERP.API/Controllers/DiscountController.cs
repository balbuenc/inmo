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
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _DiscountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _DiscountRepository = discountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            return Ok(await _DiscountRepository.GetAllDiscounts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscounts(int id)
        {
            return Ok(await _DiscountRepository.GetDiscountDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Discount discount)
        {
            if (discount == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _DiscountRepository.InsertDiscount(discount);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Discount discount)
        {
            if (discount == null) 
                return BadRequest();

            if (discount.porcentaje.ToString() == null)
            {
                ModelState.AddModelError("Discount", "Discount % shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _DiscountRepository.UpdateDiscount(discount);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            if (id == 0)
                return BadRequest();

            await _DiscountRepository.DeleteDiscount(id);

            return NoContent(); //success
        }

    }
}
