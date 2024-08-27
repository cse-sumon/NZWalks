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
            try
            {
                var regions = await _regionRepository.GetAll();

                return Ok(regions);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Get Region by ID
        // GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.Reader)]
        public async Task<IActionResult> GetById(int id) 
        {
            try
            {
                var region = await _regionRepository.GetById(id);

                if (region is null)
                    return NotFound();

                return Ok(region);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // POST: https://localhost:portnumber/api/region
        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Create([FromBody] RegionDto regionDto)
        {
            try
            {
                await _regionRepository.Create(regionDto);

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // PUT: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RegionDto regionDto)
        {
            try
            {
                if(regionDto.Id != id)
                    return BadRequest("Model Id & Parameter Id is not same");

                var region = await _regionRepository.GetById(id);

                if (region is null)
                    return NotFound();

                await _regionRepository.Update(regionDto);


                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // DELETE: https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Writer)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var region = await _regionRepository.GetById(id);

                if (region is null)
                    return NotFound();

                await _regionRepository.Delete(region);

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
