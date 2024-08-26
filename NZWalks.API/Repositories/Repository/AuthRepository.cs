using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }



        public async Task<LoginResponseDto> CreateJwtToken(IdentityUser identityUser, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));

            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials:credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new LoginResponseDto
            {
                JwtToken = jwtToken,
                UserName = identityUser.UserName,
                UserEmail = identityUser.Email,
                UserRole = roles
            };

            return response;

        }
    }
}
