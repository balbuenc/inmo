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
    public class BrandController : Controller
    {
        private readonly IBrandRepository _BrandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _BrandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            return Ok(await _BrandRepository.GetAllBrands());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandDetails(int id)
        {
            return Ok(await _BrandRepository.GetBrandDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            if (brand == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _BrandRepository.InsertBrand(brand);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] Brand brand)
        {
            if (brand == null)
                return BadRequest();

            if (brand.marca.Trim() == string.Empty)
            {
                ModelState.AddModelError("Brand", "Brand name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _BrandRepository.UpdateBrand(brand);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (id == 0)
                return BadRequest();

            await _BrandRepository.DeleteBrand(id);

            return NoContent(); //success
        }
    }
}
