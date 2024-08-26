using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<IdentityUser> _userManager;


        public AuthRepository(IConfiguration configuration, UserManager<IdentityUser> userManager) 
        {
            _configuration = configuration;
            _userManager = userManager;

        }



        public async Task<IdentityResult?> Register(RegisterDto registerDto)
        {
            var identityuser = new IdentityUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityuser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                // Add Roles to this user
                if (registerDto.Roles is not null && registerDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityuser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return identityResult;
                    }
                }
            }

            return null;

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



        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    Roles = roles.ToList()
                };

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        public async Task<UserDto?> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                Roles = roles.ToList()
            };

            return userDto;
        }


        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user is not null)
                await _userManager.DeleteAsync(user);
           
        }
    }
}
