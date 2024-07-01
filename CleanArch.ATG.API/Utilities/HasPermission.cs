using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace CleanArch.ATG.API.Utilities
{
    [AttributeUsage(AttributeTargets.All)]
    public class HasPermission : Attribute, IAuthorizationFilter
    {
        public string Name { get; }
        public HasPermission( string name )
        {
            Name = name;
        }
        public void OnAuthorization( AuthorizationFilterContext context )
        {
            if (context != null)
            {
                var user = context.HttpContext.User;
                if (user.IsInRole("SuperAdmin"))
                    return;
                //var hasPermission = user.Claims.Any(c => c.Type == "Permission" && c.Value == Name);
                var hasPermission = user.HasClaim("Permission" , Name);
                if (!hasPermission)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
