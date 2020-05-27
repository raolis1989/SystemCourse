using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.SystemCourse.Contracts;
using Domain.SystemCourse.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Security.SystemCourse.TokenSecurity
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(User user, List<string> roles)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            if (roles != null)
            {
                foreach(var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol)); 
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890 a very long word"));
            var credentials= new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescription= new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(30),
                SigningCredentials= credentials
            };

            var tokenHandler = new  JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}