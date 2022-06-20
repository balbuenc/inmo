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
    public class SendMessageLogController : Controller
    {
        private readonly ISendMessageLogRepository _SendMessageLogRepository;

        public SendMessageLogController(ISendMessageLogRepository accountTypeRepository)
        {
            _SendMessageLogRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSendMessageLogs()
        {
            return Ok(await _SendMessageLogRepository.GetAllSendMessageLogs());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSendMessageLogDetails(int id)
        {
            return Ok(await _SendMessageLogRepository.GetSendMessageLogDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSendMessageLog([FromBody] SendMessageLog message_log)
        {
            if (message_log == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _SendMessageLogRepository.InsertSendMessageLog(message_log);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateSendMessageLog([FromBody] SendMessageLog message_log)
        {
            if (message_log == null)
                return BadRequest();

            if (message_log.texto.Trim() == string.Empty)
            {
                ModelState.AddModelError("SendMessageLog", "SendMessageLog name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _SendMessageLogRepository.UpdateSendMessageLog(message_log);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSendMessageLog(int id)
        {
            if (id == 0)
                return BadRequest();

            await _SendMessageLogRepository.DeleteSendMessageLog(id);

            return NoContent(); //success
        }
    }
}
