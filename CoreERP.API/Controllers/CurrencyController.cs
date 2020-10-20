using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
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

    }
}
