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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            return Ok(await _ClientTypeRepository.GetClientTypeDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientType([FromBody] ClientType clientTYPE)
        {
            if (clientTYPE == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _ClientTypeRepository.InsertClientType(clientTYPE);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientType([FromBody] ClientType clientTYPE)
        {
            if (clientTYPE == null)
                return BadRequest();

            if (clientTYPE.tipo.Trim() == string.Empty)
            {
                ModelState.AddModelError("Name", "ClientType name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ClientTypeRepository.UpdateClientType(clientTYPE);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientType(int id)
        {
            if (id == 0)
                return BadRequest();

            await _ClientTypeRepository.DeleteClientType(id);

            return NoContent(); //success
        }
    
    }
}
