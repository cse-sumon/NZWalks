using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var walk = await _walkRepository.GetAll();

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
        public async Task<ActionResult> Create([FromBody] WalkDto walkDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
        public async Task<ActionResult> Update(int id, [FromBody] WalkDto walkDto)
        {
            try
            {
                if (!ModelState.IsValid || walkDto.Id != id)
                {
                    return BadRequest(ModelState);
                }

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
