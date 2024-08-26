﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Create([FromBody] RegionDto regionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RegionDto regionDto)
        {
            try
            {
                if (!ModelState.IsValid || regionDto.Id != id)
                    return BadRequest(ModelState);

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
