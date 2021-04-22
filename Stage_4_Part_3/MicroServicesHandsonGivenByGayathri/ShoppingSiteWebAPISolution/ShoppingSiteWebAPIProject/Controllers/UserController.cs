using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingSiteWebAPIProject.Models;
using ShoppingSiteWebAPIProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        //private readonly IConfiguration _config;

        public string CustomerAPIUri { get; }

        public List<User> Users = new List<User>();

        public UserController(IConfiguration config)
        {
            CustomerAPIUri = config.GetValue<string>("CustomerMicroServiceAPIUri");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(CustomerAPIUri+"/Register", content);
                if(response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request - " + response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpGet("{UserName}/{Password}")]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(CustomerAPIUri + "/" + UserName + "/" + Password);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request - " + response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

    }
}
