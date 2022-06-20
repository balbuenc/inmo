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
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _QuoteRepository;

        public QuoteController(IQuoteRepository accountTypeRepository)
        {
            _QuoteRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotes()
        {
            return Ok(await _QuoteRepository.GetAllQuotes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteDetails(int id)
        {
            return Ok(await _QuoteRepository.GetQuoteDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] Quote quote)
        {
            if (quote == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _QuoteRepository.InsertQuote(quote);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuote([FromBody] Quote quote)
        {
            if (quote == null)
                return BadRequest();

            if (quote.nombre_para_documento.Trim() == string.Empty)
            {
                ModelState.AddModelError("Quote", "Quote name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _QuoteRepository.UpdateQuote(quote);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            if (id == 0)
                return BadRequest();

            await _QuoteRepository.DeleteQuote(id);

            return NoContent(); //success
        }
    }
}
