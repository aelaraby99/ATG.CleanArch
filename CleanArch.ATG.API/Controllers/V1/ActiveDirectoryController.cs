using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;

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
        public async Task<IActionResult> GetUserGroups( string searchedUser )
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var user = UserPrincipal.FindByIdentity(context , IdentityType.SamAccountName , searchedUser);

                if (user != null)
                {
                    var groups = user.GetGroups();
                    var groupNames = new List<string>();

                    foreach (var group in groups)
                    {
                        groupNames.Add(group.Name);
                    }

                    var result = new
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
                        user.Description ,
                        GroupNames = groupNames
                    };

                    return Ok(result);
                }
                else
                {
                    return NotFound($"User '{searchedUser}' not found.");
                }
            }
        }
    }
}
