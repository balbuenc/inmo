using CoreERP.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientTypeController : Controller
    {
        private readonly IClientTypeRepository _ClientTypeRepository;

        public ClientTypeController(IClientTypeRepository clientTypeRepository)
        {
            _ClientTypeRepository = clientTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAreas()
        {
            return Ok(await _ClientTypeRepository.GetAllClientTypes());
        }
    }
}
