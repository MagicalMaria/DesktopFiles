using CustomerMicroServiceWebAPIProject.Models;
using CustomerMicroServiceWebAPIProject.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMicroServiceWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> Users = new List<User>();
        public static List<Feedback> Feedbacks = new List<Feedback>();
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserDTO userDTO)
        {
            try
            {
                if (userDTO.Password == userDTO.ConfirmPassword)
                {
                    using var hmac = new HMACSHA512();
                    var user = new User
                    {
                        Id = userDTO.Id,
                        UserName = userDTO.UserName,
                        Gender=userDTO.Gender,
                        DateOfBirth=userDTO.DateOfBirth,
                        Mobile=userDTO.Mobile,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                        PasswordSalt = hmac.Key
                    };
                    Users.Add(user);
                    return Ok("New User has been successfully registered.");
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

        [HttpGet("{UserName}/{Password}")]
        public IActionResult Login(string UserName, string Password)
        {
            try
            {
                var user = Users.SingleOrDefault(u => u.UserName == UserName);
                if (user != null)
                {
                    using var hmac = new HMACSHA512(user.PasswordSalt);
                    byte[] checkPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    for (int i = 0; i < checkPasswordHash.Length; i++)
                    {
                        if (checkPasswordHash[i] != user.PasswordHash[i])
                        {
                            return Unauthorized("Invalid Password.");
                        }
                    }
                    AuthController auth = new AuthController(_config);
                    var token = auth.Get(UserName).Result;
                    return token;
                }
                else
                {
                    return Unauthorized("Invalid UserName.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut]
        public IActionResult EditCustomer(EditUserDTO editUserDTO)
        {
            try
            {
                var index = Users.FindIndex(u => u.Id == editUserDTO.Id);
                Users[index].Gender = editUserDTO.Gender;
                Users[index].DateOfBirth = editUserDTO.DateOfBirth;
                Users[index].Mobile = editUserDTO.Mobile;
                return Ok(Users[index]);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPost]
        public IActionResult ProvideFeedback(Feedback feedback)
        {
            try
            {
                Feedbacks.Add(feedback);
                return Ok("Feedback is added.");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
