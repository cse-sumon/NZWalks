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
using System.Data;

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
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            var identityResult = await _authRepository.Register(registerDto);

            if (identityResult != null && identityResult.Succeeded)
            {
                return Ok("User registered successfully! Please login.");
            }
            else
            {
                return BadRequest("Unable to registered! Something went wrong");
            }
        }


        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var loginResponseDto = await _authRepository.Login(loginDto);

            if (loginResponseDto is not null)
            {
                return Ok(loginResponseDto);
            }
            else
            {
                return BadRequest("UserName or Password incorrect");
            }
        }



        //GET: /api/Auth/GetUsers
        [HttpGet]
        [Route("GetUsers")]
        [ValidateModelAttribute]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _authRepository.GetAllUsers();
            return Ok(users);
        }


        //GET: /api/Auth/GetUserById/1
        [HttpGet]
        [Route("GetUserById/{id}")]
        [ValidateModelAttribute]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _authRepository.GetUserById(id);

            if (user is null)
                return NotFound();

            return Ok(user);
        }



        //GET: /api/Auth/DeleteUser
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [ValidateModelAttribute]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _authRepository.GetUserById(id);
            if (user is null)
                return NotFound();

            await _authRepository.Delete(id);
            return Ok();

        }



    }
}
