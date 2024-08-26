using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Helpers;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepository;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthRepository _authRepository;

        public AuthController(UserManager<IdentityUser> userManager, IAuthRepository authRepository)
        {
            _userManager = userManager;
            _authRepository = authRepository;
        }





        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
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
                            return Ok("User registered successfully! Please login.");
                        }
                    }
                }

                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {

                throw;
            } 
        }


        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.UserName);
                if (user is not null)
                {
                    var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                    if (checkPasswordResult)
                    {
                        //Get Roles for this User
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles is not null)
                        {
                            //Create Token
                            var loginResponseDto = await _authRepository.CreateJwtToken(user, roles.ToList());

                            return Ok(loginResponseDto);
                        }
                    }
                }
                return BadRequest("UserName or Password incorrect");
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        //GET: /api/Auth/GetUsers
        [HttpGet]
        [Route("GetUsers")]
        [ValidateModelAttribute]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
