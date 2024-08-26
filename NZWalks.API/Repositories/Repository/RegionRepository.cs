using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Models.DTO;
using AutoMapper;

namespace NZWalks.API.Repositories.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;
        private readonly IMapper _mapper;

        public RegionRepository(NZWalksDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<IEnumerable<RegionDto>> GetAll()
        {
           //var regions =  await _context.Regions
           //             .AsNoTracking()
           //             .Select(region=> new RegionDto
           //             {
           //                 Id = region.Id,
           //                 Code = region.Code,
           //                 Name = region.Name,
           //                 RegionImageUrl = region.RegionImageUrl
           //             })
           //             .ToListAsync();

            var regions = await _context.Regions
                            .AsNoTracking()
                            .ToListAsync();

            var regionsDto = _mapper.Map<IEnumerable<RegionDto>>(regions);
            return regionsDto;
        }


        public async Task<RegionDto?> GetById(int id)
        {
           
            //var region = await _context.Regions
            //            .AsNoTracking()
            //            .Select(region=> new RegionDto
            //            {
            //                Id = region.Id,
            //                Name = region.Name,
            //                Code = region.Code,
            //                RegionImageUrl = region.RegionImageUrl,
            //            })
            //            .FirstOrDefaultAsync(x => x.Id == id);

            var region = await _context.Regions
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id == id);

            var regionDto = _mapper.Map<RegionDto>(region);
            return regionDto;
        }


        public async Task Create(RegionDto regionDto)
        {
            //var region = new Region()
            //{
            //    Code = regionDto.Code,
            //    Name = regionDto.Name,
            //    RegionImageUrl = regionDto.RegionImageUrl,
            //};
            var region = _mapper.Map<Region>(regionDto);

            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
        }

    
        public async Task Update(RegionDto regionDto)
        {
            //var region = new Region()
            //{
            //    Id = regionDto.Id,
            //    Code = regionDto.Code,
            //    Name = regionDto.Name,
            //    RegionImageUrl = regionDto.RegionImageUrl,
            //};

            var region = _mapper.Map<Region>(regionDto);

            _context.Update(region);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(RegionDto regionDto)
        {
            //var region = new Region()
            //{
            //    Id = regionDto.Id,
            //    Code = regionDto.Code,
            //    Name = regionDto.Name,
            //    RegionImageUrl = regionDto.RegionImageUrl,
            //};

            var region = _mapper.Map<Region>(regionDto);

            _context.Remove(region);
            await _context.SaveChangesAsync();

        }

   

    }
}
