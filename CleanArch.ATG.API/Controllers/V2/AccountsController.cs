using Asp.Versioning;
using CleanArch.ATG.Application.DTO;
using CleanArch.ATG.Application.Interfaces.JWT;
using CleanArch.ATG.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.ATG.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly IJwtTokenService _jwtTokenservice;

        public AccountsController( UserManager<UserApplication> userManager , RoleManager<AppRole> roleManager , SignInManager<UserApplication> signInManager , IJwtTokenService jwtTokenservice )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenservice = jwtTokenservice;
        }
        [HttpPost]
        public async Task<IActionResult> Register( [FromBody] UserApplication request )
        {
            var user = new UserApplication
            {
                UserName = request.Email ,
                Email = request.Email ,
                Password = request.Password
            };
            var result = await _userManager.CreateAsync(user , request.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        //Create Role
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole( AppRole role )
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var newRole = new AppRole()
            {
                Name = role.Name
            };
            var result = await _roleManager.CreateAsync(newRole);
            if (!result.Succeeded)
                return BadRequest();
            return Ok();
        }
        [HttpPost("AddRoleTo")]
        public async Task<IActionResult> AddRoleTo( string roleName , string userEmail )
        {
            var existingUser = await _userManager.FindByEmailAsync(userEmail);
            if (existingUser == null)
                return NotFound("User Not Exist");
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound("Role Not Exist");


            var result = await _userManager.AddToRoleAsync(existingUser , role.Name);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login( LoginRequestDTO request )
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                return NotFound("User Not Exist");
            var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(existingUser , request.Password , false);
            if (!isPasswordValid.Succeeded)
                return BadRequest("Invalid");
            var result = await _signInManager.PasswordSignInAsync(existingUser, request.Password , false , false);
            if (!result.Succeeded)
                return BadRequest("Invalid credentials.");

            var token = _jwtTokenservice.GenerateToken(existingUser);
            return Ok(new {Token = token});
        }
        [HttpGet("Authorized")]
        [Authorize]
        public async Task<IActionResult> PrivateData()
        {
            return Ok("Authorized");
        }
    }
}
