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
    public class CurrencyController : Controller
    {

        private readonly ICurrencyRepository _currencyRepository;
        
        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurencies()
        {
            return Ok(await _currencyRepository.GetAllCurrencies());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyDetails(int id)
        {
            return Ok(await _currencyRepository.GetCurrencytDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] Currency currency)
        {
            if (currency == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _currencyRepository.InsertCurrency(currency);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrency([FromBody] Currency currency)
        {
            if (currency == null)
                return BadRequest();

            if (currency.moneda.Trim() == string.Empty)
            {
                ModelState.AddModelError("Name", "Currency name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _currencyRepository.UpdateCurrency(currency);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            if (id == 0)
                return BadRequest();

            await _currencyRepository.DeleteCurrency(id);

            return NoContent(); //success
        }



    }
}
