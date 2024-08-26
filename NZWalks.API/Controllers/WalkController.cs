using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;

        public WalkController(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }


        // GET: api/walk
        // GET: api/walk?filterOn=Name&filterQuiry=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var walk = await _walkRepository.GetAll(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

                return Ok(walk);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // GET: api/walk
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var walk = await _walkRepository.GetById(id);

                if (walk is null)
                {
                    return NotFound();
                }

                return Ok(walk);
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        // POST: api/walk
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<ActionResult> Create([FromBody] WalkDto walkDto)
        {
            try
            {
                await _walkRepository.Create(walkDto);

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // PUT: api/walk
        [HttpPut]
        [Route("{id}")]
        [ValidateModelAttribute]
        public async Task<ActionResult> Update(int id, [FromBody] WalkDto walkDto)
        {
            try
            {
                if (walkDto.Id != id)
                    return BadRequest("Model Id & Parameter Id is not same");


                var walk = await _walkRepository.GetById(id);

                if (walk is null)
                    return NotFound();


                await _walkRepository.Update(walkDto);

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // DELETE: api/walk
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var walk = await _walkRepository.GetById(id);

                if (walk is null)
                    return NotFound();


                await _walkRepository.Delete(walk);

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
