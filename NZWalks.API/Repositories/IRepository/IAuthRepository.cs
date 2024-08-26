using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<IdentityResult?> Register(RegisterDto registerDto);

        Task<LoginResponseDto> CreateJwtToken(IdentityUser identityUser, List<string> roles);

        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserDto?> GetUserById(string id);

        Task Delete(string id);
    }
}
