using Asp.Versioning;
using CleanArch.ATG.API.Utilities;
using CleanArch.ATG.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.ATG.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ActiveDirectoryController : ControllerBase
    {
        string ipAddress = @"10.0.20.98";
        string domainName = @"NSA.local";
        string userName = @"NSA\administrator";
        string password = "P@ssw0rd";
        private readonly IConfiguration _configuration;

        public ActiveDirectoryController( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            ///            [
            ///  "Administrator",
            ///  "Guest",
            ///  "krbtgt",
            ///  "test11",
            ///  "testuser",
            ///  "t2user"
            ///]
            List<string> userList = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain , ipAddress , userName , password))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        if (result is UserPrincipal userPrincipal)
                        {
                            userList.Add(userPrincipal.SamAccountName);
                        }
                    }
                }
                return Ok(userList);
            }
        }

        [HttpGet("AllSecurityGroups")]
        public async Task<IActionResult> GetAllSecurityGroups()
        {
            List<string> groupList = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain , ipAddress , userName , password))
            {
                using (var searcher = new PrincipalSearcher(new GroupPrincipal(context) { IsSecurityGroup = true }))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        if (result is GroupPrincipal groupPrincipal)
                        {
                            groupList.Add(groupPrincipal.Name);
                        }
                    }
                }
                return Ok(groupList);
            }
        }

        [HttpGet("SecurityGroup")]
        public async Task<IActionResult> GetSecurityGroup( string groupName )
        {
            using (var context = new PrincipalContext(ContextType.Domain , ipAddress , userName , password))
            {
                var group = GroupPrincipal.FindByIdentity(context , IdentityType.Name , groupName);
                if (group != null)
                {
                    var result = new
                    {
                        group.Name ,
                        group.Description ,
                        group.DisplayName ,
                        group.DistinguishedName ,
                        group.SamAccountName ,
                        group.GroupScope ,
                        group.IsSecurityGroup
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Group '{groupName}' not found.");
                }
            }
        }

        [HttpGet("SecurityGroupMembers")]
        public async Task<IActionResult> GetSecurityGroupMembers( string groupName )
        {
            using (var context = new PrincipalContext(ContextType.Domain , ipAddress , userName , password))
            {
                var group = GroupPrincipal.FindByIdentity(context , IdentityType.Name , groupName);
                if (group != null)
                {
                    var members = new List<string>();
                    foreach (var member in group.GetMembers())
                    {
                        members.Add(member.SamAccountName);
                    }

                    var result = new
                    {
                        group.Name ,
                        group.Description ,
                        group.DisplayName ,
                        group.DistinguishedName ,
                        group.SamAccountName ,
                        group.GroupScope ,
                        group.IsSecurityGroup ,
                        Members = members
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Group '{groupName}' not found.");
                }
            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetUser( string searchedUser )
        {
            using (var context = new PrincipalContext(ContextType.Domain , ipAddress , userName , password))
            {
                var user = UserPrincipal.FindByIdentity(context , IdentityType.SamAccountName , searchedUser);

                if (user != null)
                {
                    return Ok(new
                    {
                        user.Name ,
                        user.SamAccountName ,
                        user.EmailAddress ,
                        user.Enabled ,
                        user.LastLogon ,
                        user.LastPasswordSet ,
                        user.PasswordNeverExpires ,
                        user.UserCannotChangePassword ,
                        user.UserPrincipalName ,
                        user.VoiceTelephoneNumber ,
                        user.GivenName ,
                        user.Surname ,
                        user.DisplayName ,
                        user.DistinguishedName ,
                        user.Guid ,
                        user.HomeDirectory ,
                        user.HomeDrive ,
                        user.MiddleName ,
                        user.SmartcardLogonRequired ,
                        user.StructuralObjectClass ,
                        user.ContextType ,
                        user.Description
                    });
                }
                else
                {
                    return NotFound($"User '{userName}' not found.");
                }

            }
        }

        [HttpGet("UserGroups")]
        [Authorize]
        public async Task<IActionResult> GetUserGroups()
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var searchedUser = User?.Identity?.Name;
                var user = UserPrincipal.FindByIdentity(context , IdentityType.SamAccountName , searchedUser);

                if (user != null)
                {
                    var groups = user.GetGroups();
                    var groupNames = new List<string>();

                    foreach (var group in groups)
                    {
                        groupNames.Add(group.Name);
                    }

                    var token = GenerateToken(user);
                    var result = new
                    {
                        user.Name ,
                        user.SamAccountName ,
                        user.EmailAddress ,
                        user.DisplayName ,
                        user.Enabled ,
                        user.LastLogon ,
                        //user.LastPasswordSet ,
                        //user.PasswordNeverExpires ,
                        //user.UserCannotChangePassword ,
                        //user.UserPrincipalName ,
                        //user.VoiceTelephoneNumber ,
                        //user.GivenName ,
                        //user.Surname ,
                        //user.DistinguishedName ,
                        //user.Guid ,
                        //user.HomeDirectory ,
                        //user.HomeDrive ,
                        //user.MiddleName ,
                        //user.SmartcardLogonRequired ,
                        //user.StructuralObjectClass ,
                        //user.ContextType ,
                        //user.Description ,
                        GroupNames = groupNames
                    };

                    return Ok(new { Token = token , UserDtails = result });
                }
                else
                {
                    return NotFound($"User '{searchedUser}' not found.");
                }
            }
        }

        private string GenerateToken( UserPrincipal user )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration ["Jwt:Key"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration ["JWT:Key"]));
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Name) ,
                new Claim(ClaimTypes.Email , user.EmailAddress??""),
                new Claim("Permission" , "Read")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration ["Jwt:ValidAudience"] ,
                Issuer = _configuration ["Jwt:ValidIssuer"] ,
                Subject = new ClaimsIdentity(claimList) ,
                Expires = DateTime.UtcNow.AddHours(1) ,
                SigningCredentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("TestAdmin")]
        [HasPermission("Admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Test()
        {
            return Ok("User is authenticated./Admin");
        }
        [HttpGet("TestRead")]
        [HasPermission("Read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TestRead()
        {
            return Ok("User is authenticated./Read");
        }
    }
}
