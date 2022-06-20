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
    public class MessageController : Controller
    {
        
        private readonly IMessageRepository _MessageRepository;

        public MessageController(IMessageRepository accountTypeRepository)
        {
            _MessageRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            return Ok(await _MessageRepository.GetAllMessages());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageDetails(int id)
        {
            return Ok(await _MessageRepository.GetMessageDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            if (message == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _MessageRepository.InsertMessage(message);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] Message message)
        {
            if (message == null)
                return BadRequest();

            if (message.titulo.Trim() == string.Empty)
            {
                ModelState.AddModelError("Message", "Message tile shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _MessageRepository.UpdateMessage(message);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (id == 0)
                return BadRequest();

            await _MessageRepository.DeleteMessage(id);

            return NoContent(); //success
        }
    }
}
