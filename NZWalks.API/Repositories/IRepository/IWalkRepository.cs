using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IWalkRepository
    {

        Task<IEnumerable<WalkDto>> GetAll();

        Task <WalkDto?> GetById(int id);

        Task Create(WalkDto walkDto);

        Task Update(WalkDto walkDto);

        Task Delete(WalkDto walkDto);
    }
}
