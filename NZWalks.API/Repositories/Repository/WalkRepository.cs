using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using System.Reflection.Metadata;

namespace NZWalks.API.Repositories.Repository
{
    public class WalkRepository : IWalkRepository
    {

        private readonly NZWalksDbContext _context;
        private readonly IMapper _mapper;

        public WalkRepository(NZWalksDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<IEnumerable<WalkDto>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1)
        {

            var walkDtos = (from walk in _context.Walks
                            join difficulty in _context.Difficulties
                            on walk.DifficultyId equals difficulty.Id
                            join region in _context.Regions
                            on walk.RegionId equals region.Id
                            select new WalkDto
                            {
                                Id = walk.Id,
                                Name = walk.Name,
                                Description = walk.Description,
                                LengthInKm = walk.LengthInKm,
                                WalkImageUrl = walk.WalkImageUrl,
                                DifficultyId = walk.DifficultyId,
                                RegionId = walk.RegionId,
                                DifficultyName = difficulty.Name,
                                RegionName = region.Name
                            })
                            .AsNoTracking();

            //filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                { 
                    walkDtos = walkDtos.Where(x=>x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkDtos = isAscending ? walkDtos.OrderBy(x => x.Name) : walkDtos.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walkDtos = isAscending ? walkDtos.OrderBy(x => x.LengthInKm) : walkDtos.OrderByDescending(x => x.LengthInKm);
                }
            }

            //paginate
            var skipResults = (pageNumber - 1) * pageSize;

            walkDtos = walkDtos.Skip(skipResults)
                               .Take(pageSize);


            return await walkDtos.ToListAsync();


           
        }

        public async Task<WalkDto?> GetById(int id)
        {
            var walkDto = await (from walk in _context.Walks
                                  where walk.Id == id
                                  join difficulty in _context.Difficulties
                                  on walk.DifficultyId equals difficulty.Id
                                  join region in _context.Regions
                                  on walk.RegionId equals region.Id
                                  select new WalkDto
                                  {
                                      Id = walk.Id,
                                      Name = walk.Name,
                                      Description = walk.Description,
                                      LengthInKm = walk.LengthInKm,
                                      WalkImageUrl = walk.WalkImageUrl,
                                      DifficultyId = walk.DifficultyId,
                                      RegionId = walk.RegionId,
                                      DifficultyName = difficulty.Name,
                                      RegionName = region.Name
                                  })
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();



            return walkDto;
        }

        public async Task Create(WalkDto walkDto)
        {
            var walk = _mapper.Map<Walk>(walkDto);
            await _context.AddRangeAsync(walk);
            await _context.SaveChangesAsync();

        }

        public async Task Update(WalkDto walkDto)
        {
            //var walk = new Walk()
            //{
            //    Name = walkDto.Name,
            //    Description= walkDto.Description,
            //    LengthInKm= walkDto.LengthInKm,
            //    WalkImageUrl = walkDto.WalkImageUrl,
            //    DifficultyId = walkDto.DifficultyId,
            //    RegionId= walkDto.RegionId,
            //};
            var walk = _mapper.Map<Walk>(walkDto);
            _context.Update(walk);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(WalkDto walkDto)
        {
            var walk = _mapper.Map<Walk>(walkDto);
            _context.Remove(walk);
            await _context.SaveChangesAsync();
        }


        
    }
}
