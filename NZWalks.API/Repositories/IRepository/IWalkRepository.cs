using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IWalkRepository
    {

        Task<IEnumerable<WalkDto>> GetAll(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1);

        Task <WalkDto?> GetById(int id);

        Task Create(WalkDto walkDto);

        Task Update(WalkDto walkDto);

        Task Delete(WalkDto walkDto);
    }
}
