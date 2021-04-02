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
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationRepository _ConfigurationRepository;

        public ConfigurationController(IConfigurationRepository configurationRepository)
        {
            _ConfigurationRepository = configurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConfigurations()
        {
            return Ok(await _ConfigurationRepository.GetAllConfigurations());
        }

        [HttpGet("{param}")]
        public async Task<IActionResult> GetConfigurationDetails(string param)
        {
            return Ok(await _ConfigurationRepository.GetConfigurationDetails(param));
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfiguration([FromBody] Configuration configuration)
        {
            if (configuration == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _ConfigurationRepository.InsertConfiguration(configuration);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateConfiguration([FromBody] Configuration configuration)
        {
            if (configuration == null)
                return BadRequest();

            if (configuration.parametro.Trim() == string.Empty)
            {
                ModelState.AddModelError("Configuration", "Parameter name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ConfigurationRepository.UpdateConfiguration(configuration);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            if (id == 0)
                return BadRequest();

            await _ConfigurationRepository.DeleteConfiguration(id);

            return NoContent(); //success
        }
    }
}
