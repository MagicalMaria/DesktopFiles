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
    public class PurchaseController : ControllerBase
    {
        public string PurchaseAPIUri { get; }

        private readonly IConfiguration _config;

        public PurchaseController(IConfiguration config)
        {
            PurchaseAPIUri = config.GetValue<string>("PurchaseMicroServiceAPIUri");
            _config = config;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetCartDetails(int UserId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(PurchaseAPIUri + "api/Cart/" + UserId);
                if (response.IsSuccessStatusCode)
                {
                    var CartItems = response.Content.ReadAsAsync<List<Cart>>().Result;
                    return Ok(CartItems);
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
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(PurchaseAPIUri + "api/Cart", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
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

        [HttpDelete("{UserId}/{ProductId}")]
        public async Task<IActionResult> RemoveFromCart(int UserId, int ProductId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(PurchaseAPIUri + "api/Cart/" + UserId + "/" + ProductId);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
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

        [HttpPost("PlaceOrder/{UserId}")]
        public async Task<IActionResult> PlaceOrder(int UserId)
        {
            try
            {
                var CartDetails = this.GetCartDetails(UserId);
                var res = (OkObjectResult)CartDetails.Result;
                List<Cart> op=(List<Cart>)res.Value;
                var ProductsList = op.Select(c => c.ProductId);

                CustomerController custCtrlr = new CustomerController(_config);
                var result=(OkObjectResult)custCtrlr.GetAllProducts().Result.Result;
                var AvailableProducts = (IEnumerable<Product>) result.Value;
                var total = 0.00;

                foreach (int id in ProductsList)
                {
                    var num1 = op.SingleOrDefault(c => c.ProductId == id).Quantitity;
                    var num2 = AvailableProducts.SingleOrDefault(c => c.Id == id).UnitPrice;
                    total += num1 * num2;
                }

                var order = new OrderDTO
                {
                    UserId = UserId,
                    OrderDate = DateTime.Now,
                    OrderAmount = total,
                    Products = ProductsList
                };

                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(PurchaseAPIUri + "api/Order", content);
                if (response.IsSuccessStatusCode)
                {
                    var resultData = response.Content.ReadAsStringAsync().Result;
                    return Ok(resultData);
                }
                else
                {
                    return BadRequest("Bad Request - " + response);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Exception - " + e.Message);
            }
        }

        [HttpDelete("CancelOrder/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(PurchaseAPIUri + "api/Order/" + orderId);
                if(response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
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
