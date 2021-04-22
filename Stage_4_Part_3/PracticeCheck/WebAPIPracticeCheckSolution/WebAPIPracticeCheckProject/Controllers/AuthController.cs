using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIPracticeCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SymmetricSecurityKey _Key;

        public AuthController(IConfiguration configuration)
        {
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
        }
        private string GenerateJsonWebToken(int UserID,string UserRole)
        {
            var credentials = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,UserRole),
                new Claim("UserID",UserID.ToString())
            };

            var token = new JwtSecurityToken(
                    issuer: "MySystem",
                    audience: "MyUsers",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string UserRole; 
            if(id==1)
            {
                UserRole = "Admin";
            }
            else if(id==-1)
            {
                UserRole = string.Empty;
            }
            else
            {
                UserRole = "Customer";
            }

            return Content(GenerateJsonWebToken(id, UserRole));
        }
    }
}
