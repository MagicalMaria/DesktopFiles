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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly string ProductAPIUri;
        public List<Product> ProductsList;

        public AdminController(IConfiguration config)
        {
            ProductAPIUri = config.GetValue<string>("ProductMicroServiceAPIUri");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(ProductAPIUri);
                if (response.IsSuccessStatusCode)
                {
                    ProductsList = response.Content.ReadAsAsync<List<Product>>().Result;
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

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ProductAPIUri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut("ChangeAvailability/{ProductId}/{status}")]
        public async Task<ActionResult<Product>> ChangeAvailability(int ProductId,bool status)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(ProductAPIUri + "/" + ProductId);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var rawResult = response.Content.ReadAsAsync<Product>();
                    Product prod = rawResult.Result;
                    prod.IsAvailable = status;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json");
                    var resp = await client.PutAsync(ProductAPIUri, content);
                    if(resp.IsSuccessStatusCode)
                    {
                        Product prodt = resp.Content.ReadAsAsync<Product>().Result;
                        return Ok(prodt);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut("ChangeQuantity/{ProductId}/{Quantity}")]
        public async Task<ActionResult<Product>> ChangeQuantity(int ProductId, int Quantity)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(ProductAPIUri + "/" + ProductId);
                if (response.IsSuccessStatusCode)
                {
                    Product prod = response.Content.ReadAsAsync<Product>().Result;
                    prod.Quantity += Quantity;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json");
                    var resp = await client.PutAsync(ProductAPIUri, content);
                    if (resp.IsSuccessStatusCode)
                    {
                        Product prodt = resp.Content.ReadAsAsync<Product>().Result;
                        return Ok(prodt);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut("ChangePrice/{ProductId}/{Price}")]
        public async Task<ActionResult<Product>> ChangePrice(int ProductId, double Price)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(ProductAPIUri + "/" + ProductId);
                if (response.IsSuccessStatusCode)
                {
                    Product prod = response.Content.ReadAsAsync<Product>().Result;
                    prod.UnitPrice = Price;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json");
                    var resp = await client.PutAsync(ProductAPIUri, content);
                    if (resp.IsSuccessStatusCode)
                    {
                        Product prodt = resp.Content.ReadAsAsync<Product>().Result;
                        return Ok(prodt);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
