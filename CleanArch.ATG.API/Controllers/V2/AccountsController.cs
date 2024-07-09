using Asp.Versioning;
using CleanArch.ATG.Application.DTOs;
using CleanArch.ATG.Application.Interfaces.JWT;
using CleanArch.ATG.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArch.ATG.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    //[Authorize]
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
        [AllowAnonymous]
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginRequestDTO request )
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser == null)
                return NotFound("User Not Exist");
            var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(existingUser , request.Password , false);
            if (!isPasswordValid.Succeeded)
                return BadRequest("Invalid");
            var result = await _signInManager.PasswordSignInAsync(existingUser , request.Password , false , false);
            if (!result.Succeeded)
                return BadRequest("Invalid credentials.");

            var userRoles = await _userManager.GetRolesAsync(existingUser); //should be based on the group not userRoles
            var userPermissions = new List<string>();
            //var userPermissions = new HashSet<string>();
            foreach (var role in userRoles)
            {
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleEntity);
                    userPermissions.AddRange(roleClaims.Where(c => c.Type == "Permission").Select(c => c.Value));
                    //foreach (var claim in roleClaims.Where(c => c.Type == "Permission"))
                    //{
                    //    userPermissions.Add(claim.Value);
                    //}
                }
            }
            var distinctPermissions = userPermissions.Distinct().ToList();
            var token = _jwtTokenservice.GenerateToken(existingUser , userRoles , distinctPermissions);
            return Ok(new { Token = token , Permissions = distinctPermissions });
        }


        #region Roles
        [HttpGet("AllRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return Ok(roles);
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

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole( string RoleName )
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null)
                return NotFound("Role Not Exist");
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return BadRequest();
            return Ok();
        }

        [HttpPost("AddPermissionToRole")]
        public async Task<IActionResult> AddPermissionToRole( string permission , string roleName )
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole == null)
                return NotFound("Role Not Exist");
            var permissionClaims = await _roleManager.GetClaimsAsync(existingRole);
            if (permissionClaims.Any(x => x.Value == permission))
                return BadRequest("Permission Already Exist");
            var result = await _roleManager.AddClaimAsync(existingRole , new Claim("Permission" , permission));
            return Ok();
        }
        [HttpDelete("DeletePermissionFromRole")]
        public async Task<IActionResult> DeletePermissionFromRole( string permission , string roleName )
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole == null)
                return NotFound("Role Not Exist");
            var permissionClaims = await _roleManager.GetClaimsAsync(existingRole);
            if (permissionClaims.Any(x => x.Value == permission))
                await _roleManager.RemoveClaimAsync(existingRole , new Claim("Permission" , permission));
            else
                return BadRequest("Permission Not Exist");

            return Ok();
        }
        [HttpGet("GetRolePermissions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRolePermissions( string roleName )
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole == null)
                return NotFound("Role Not Exist");
            var permissions = await _roleManager.GetClaimsAsync(existingRole);
            return Ok(permissions);
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
        [HttpDelete("RemoveRoleFrom")]
        public async Task<IActionResult> RemoveRoleFrom( string roleName , string userEmail )
        {
            var existingUser = await _userManager.FindByEmailAsync(userEmail);
            if (existingUser == null)
                return NotFound("User Not Exist");
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound("Role Not Exist");
            var isInRole = await _userManager.IsInRoleAsync(existingUser , role.Name);
            if (!isInRole)
                return BadRequest("User Not In Role");
            var res = await _userManager.RemoveFromRoleAsync(existingUser , role.Name);
            if (!res.Succeeded)
                return BadRequest();
            return Ok();
        }

        [HttpGet("PrivateData")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PrivateData()
        {
            return Ok("Admin Authorized");
        }
        [HttpGet("TestData")]
        [Authorize(Roles = "Admin,TestRole" , Policy = "")]
        public async Task<IActionResult> TestData()
        {
            return Ok("Test/Admin Authorized");
        }
        [HttpGet("UserData")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UserData()
        {
            return Ok("User/Admin Authorized");
        }

        #endregion


        #region User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles( string userEmail )
        {
            var existingUser = await _userManager.FindByEmailAsync(userEmail);
            if (existingUser == null)
                return NotFound("User Not Exist");
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (userRoles.Count == 0)
                return NotFound("User Not In Any Role");
            return Ok(userRoles);
        }
        #endregion
    }
}
