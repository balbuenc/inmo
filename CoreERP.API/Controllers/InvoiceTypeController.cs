using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceTypeController : ControllerBase
    {
        private readonly IInvoiceTypeRepository _InvoiceTypeRepository;

        public InvoiceTypeController(IInvoiceTypeRepository invoiceTypeRepository)
        {
            _InvoiceTypeRepository = invoiceTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoiceTypes()
        {
            return Ok(await _InvoiceTypeRepository.GetAllInvoiceTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceTypeDetails(int id)
        {
            return Ok(await _InvoiceTypeRepository.GetInvoiceTypeDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoiceType([FromBody] InvoiceType invoiceType)
        {
            if (invoiceType == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _InvoiceTypeRepository.InsertInvoiceType(invoiceType);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvoiceType([FromBody] InvoiceType invoiceType)
        {
            if (invoiceType == null)
                return BadRequest();

            if (invoiceType.condicion.Trim() == string.Empty)
            {
                ModelState.AddModelError("InvoiceType", "InvoiceType name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _InvoiceTypeRepository.UpdateInvoiceType(invoiceType);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceType(int id)
        {
            if (id == 0)
                return BadRequest();

            await _InvoiceTypeRepository.DeleteInvoiceType(id);

            return NoContent(); //success
        }
    }
}
