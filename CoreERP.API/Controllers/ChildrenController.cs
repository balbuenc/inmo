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
    public class ChildrenController : Controller
    {
        private readonly IChildrenRepository _ChildrenRepository;

        public ChildrenController(IChildrenRepository childrenRepository)
        {
            _ChildrenRepository = childrenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildrens()
        {
            return Ok(await _ChildrenRepository.GetAllChildrens());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChildrenDetails(int id)
        {
            return Ok(await _ChildrenRepository.GetChildrenDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChildren([FromBody] Children children)
        {
            if (children == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _ChildrenRepository.InsertChildren(children);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateChildren([FromBody] Children children)
        {
            if (children == null)
                return BadRequest();

            if (children.nombre.Trim() == string.Empty)
            {
                ModelState.AddModelError("Children", "Children name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ChildrenRepository.UpdateChildren(children);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChildren(int id)
        {
            if (id == 0)
                return BadRequest();

            await _ChildrenRepository.DeleteChildren(id);

            return NoContent(); //success
        }
    }
}
