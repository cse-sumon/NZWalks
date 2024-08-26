using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<RegionDto>> GetAll();

        Task<RegionDto?> GetById(int id);

        Task Create(RegionDto regionDto);

        Task Update(RegionDto regionDto);

        Task Delete(RegionDto regionDto);

    }
}
