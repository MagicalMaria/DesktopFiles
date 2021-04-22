using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPIFinalCheckProject.Models;
using WebAPIFinalCheckProject.Models.DTOs;

namespace WebAPIFinalCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MovieManagementContext _context;

        public UserController(MovieManagementContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
        {
            try
            {
                if (userDTO.Password == userDTO.ConfirmPassword)
                {
                    using var hmac = new HMACSHA512();
                    var user = new User
                    {
                        UserName = userDTO.UserName,
                        FirstName = userDTO.FirstName,
                        LastName = userDTO.LastName,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                        PasswordSalt = hmac.Key
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    return Ok("New User is added...");
                }
                else
                {
                    return BadRequest("Password and Confirm Password must match.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpGet("{UserId}/{Password}")]
        public async Task<IActionResult> Get(int UserId,string Password)
        {
            try
            {
                User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == UserId);
                if(user!=null)
                {
                    using var hmac = new HMACSHA512(user.PasswordSalt);
                    byte[] checkPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    for(int i=0;i<checkPasswordHash.Length;i++)
                    {
                        if(checkPasswordHash[i]!=user.PasswordHash[i])
                        {
                            return Unauthorized("Invalid Password");
                        }
                    }
                    return Ok("Logged in successfully...");
                }
                else
                {
                    return Unauthorized("Invalid UserId");
                }
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
