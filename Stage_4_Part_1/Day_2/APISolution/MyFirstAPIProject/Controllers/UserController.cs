using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private CustomerManagementContext _context;
        private ITokenService _tokenService;

        public UserController(CustomerManagementContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(AppUserDTO userDTO)
        {
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = userDTO.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenUserDTO>> Login(AppUserDTO userDTO)
        {
            var myUser = await _context.Users.SingleOrDefaultAsync(usr => usr.Username == userDTO.UserName);
            if(myUser==null)
            {
                return Unauthorized("Username does not exist");
            }

            using var hmac = new HMACSHA512(myUser.PasswordSalt);
            var checkPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            for (int i = 0; i < checkPasswordHash.Length; i++)
            {
                if(checkPasswordHash[i] != myUser.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            var tokenUser = new TokenUserDTO
            {
                Username = myUser.Username,
                Token = _tokenService.CreateToken(myUser)
            };

            return tokenUser;
        }
    }
}
