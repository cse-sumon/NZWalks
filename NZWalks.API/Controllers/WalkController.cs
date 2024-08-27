using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Helpers;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = Roles.Reader)]
        public async Task<ActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {

            var walk = await _walkRepository.GetAll(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(walk);

        }


        // GET: api/walk
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.Reader)]
        public async Task<ActionResult> GetById(int id)
        {
            var walk = await _walkRepository.GetById(id);

            if (walk is null)
            {
                return NotFound();
            }

            return Ok(walk);

        }



        // POST: api/walk
        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ActionResult> Create([FromBody] WalkDto walkDto)
        {

            await _walkRepository.Create(walkDto);

            return Ok();

        }


        // PUT: api/walk
        [HttpPut]
        [Route("{id}")]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ActionResult> Update(int id, [FromBody] WalkDto walkDto)
        {

            if (walkDto.Id != id)
                return BadRequest("Model Id & Parameter Id is not same");


            var walk = await _walkRepository.GetById(id);

            if (walk is null)
                return NotFound();


            await _walkRepository.Update(walkDto);

            return Ok();

        }


        // DELETE: api/walk
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ActionResult> Delete(int id)
        {

            var walk = await _walkRepository.GetById(id);

            if (walk is null)
                return NotFound();


            await _walkRepository.Delete(walk);

            return Ok();

        }

    }
}
