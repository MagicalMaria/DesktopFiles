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
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public string CustomerAPIUri { get; }
        public string ProductAPIUri { get; }
        public static IEnumerable<Product> ProductsList;

        // public List<User> Users = new List<User>();

        public CustomerController(IConfiguration config)
        {
            _config = config;
            CustomerAPIUri = _config.GetValue<string>("CustomerMicroServiceAPIUri");
            ProductAPIUri = _config.GetValue<string>("ProductMicroServiceAPIUri");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(ProductAPIUri);
                if (response.IsSuccessStatusCode)
                {
                    ProductsList = response.Content.ReadAsAsync<List<Product>>().Result.Where(c => c.IsAvailable == true);
                    return Ok(ProductsList);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCustomer([FromBody] EditUserDTO editUserDTO)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(editUserDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(CustomerAPIUri, content);
                if (response.IsSuccessStatusCode)
                {
                    var rawResult = response.Content.ReadAsAsync<User>();
                    User result = rawResult.Result;
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request - " + response);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProvideFeedback(Feedback feedback)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(CustomerAPIUri, content);
                if(response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request - " + response);
                }
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
