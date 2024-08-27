using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Helpers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using System.Net;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class RegionController : ControllerBase
    {

        private readonly IRegionRepository _regionRepository;

        public RegionController(NZWalksDbContext context, IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        // Get All Regions
        // GET: https://localhost:portnumber/api/region
        [HttpGet]
        //[Authorize(Roles = Roles.Reader)]
        public async Task<IActionResult> GetAll()
        {
            
            var regions = await _regionRepository.GetAll();
            return Ok(regions);
            
        }

        // Get Region by ID
        // GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.Reader)]
        public async Task<IActionResult> GetById(int id) 
        {
            var region = await _regionRepository.GetById(id);

            if (region is null)
                return NotFound();

            return Ok(region);
        }


        // POST: https://localhost:portnumber/api/region
        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Create([FromBody] RegionDto regionDto)
        {

            await _regionRepository.Create(regionDto);

            return Ok();

        }


        // PUT: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RegionDto regionDto)
        {

            if (regionDto.Id != id)
                return BadRequest("Model Id & Parameter Id is not same");

            var region = await _regionRepository.GetById(id);

            if (region is null)
                return NotFound();

            await _regionRepository.Update(regionDto);


            return Ok();
        }


        // DELETE: https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Delete(int id)
        {

            var region = await _regionRepository.GetById(id);

            if (region is null)
                return NotFound();

            await _regionRepository.Delete(region);

            return Ok();
        }
    }
}
