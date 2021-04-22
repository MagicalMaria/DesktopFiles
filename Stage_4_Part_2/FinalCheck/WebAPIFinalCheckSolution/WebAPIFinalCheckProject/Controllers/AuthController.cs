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
using WebAPIFinalCheckProject.Models;

namespace WebAPIFinalCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        private string GenerateJsonWebToken(int UserId, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,Role),
                new Claim("UserId",UserId.ToString())
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"])), SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                    issuer: "MySystem",
                    audience: "MyUsers",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("{UserId}")]
        public IActionResult Get(int UserId)
        {
            try
            {
                string UserRole=string.Empty;
                if (UserId == 1)
                {
                    UserRole = "Admin";
                }
                else
                {
                    UserRole = "Customer";
                }

                return Content(GenerateJsonWebToken(UserId, UserRole));
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
