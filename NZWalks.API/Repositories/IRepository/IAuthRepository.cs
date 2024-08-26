using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto> CreateJwtToken(IdentityUser identityUser, List<string> roles);
    }
}
