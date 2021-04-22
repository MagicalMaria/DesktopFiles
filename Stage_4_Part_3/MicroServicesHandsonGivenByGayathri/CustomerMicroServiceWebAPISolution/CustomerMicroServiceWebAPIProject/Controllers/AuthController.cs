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

namespace CustomerMicroServiceWebAPIProject.Controllers
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

        private string GenerateJsonWebToken(string UserName, string Role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,Role),
                new Claim(ClaimTypes.Name,UserName)
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

        [HttpGet]
        public ActionResult<string> Get(string UserName)
        {
            try
            {
                if (UserName == "Admin")
                {
                    string tok = GenerateJsonWebToken(UserName, "Admin");
                    return Ok(tok);
                }
                else
                {
                    string tok = GenerateJsonWebToken(UserName, "Customer");
                    return Ok(tok);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
