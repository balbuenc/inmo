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
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodRepository _PaymentMethodRepository;

        public PaymentMethodController(IPaymentMethodRepository methodRepository)
        {
            _PaymentMethodRepository = methodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            return Ok(await _PaymentMethodRepository.GetAllPaymentMethods());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodDetails(int id)
        {
            return Ok(await _PaymentMethodRepository.GetPaymentMethodDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] PaymentMethod method)
        {
            if (method == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PaymentMethodRepository.InsertPaymentMethod(method);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] PaymentMethod method)
        {
            if (method == null)
                return BadRequest();

            if (method.medio.Trim() == string.Empty)
            {
                ModelState.AddModelError("PaymentMethod", "PaymentMethod name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PaymentMethodRepository.UpdatePaymentMethod(method);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PaymentMethodRepository.DeletePaymentMethod(id);

            return NoContent(); //success
        }
    }
}
