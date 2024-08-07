﻿using CleanArch.ATG.Application.Interfaces.JWT;
using CleanArch.ATG.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure.JWT
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService( IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public string GenerateToken( UserApplication user , IEnumerable<string> roles , IEnumerable<string> permissions )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration ["Jwt:Key"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration ["JWT:Key"]));
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()) ,
                new Claim(ClaimTypes.Name , user.UserName) 
            };
            claimList.AddRange(roles.Distinct().Select(roles => new Claim(ClaimTypes.Role , roles)));
            claimList.AddRange(permissions.Distinct().Select(permission => new Claim("Permission" , permission)));
            //foreach (var role in roles)
            //{
            //    claimList.Add(new Claim(ClaimTypes.Role , role));
            //}
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

    }
}
