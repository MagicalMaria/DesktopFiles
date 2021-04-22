using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseMicroServiceWebAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroServiceWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public static List<Cart> CartList = new List<Cart>();

        [HttpGet("{UserId}")]
        public IActionResult GetCartDetails(int UserId)
        {
            try
            {
                var myCartDetails = CartList.Where(c => c.UserId == UserId);
                return Ok(myCartDetails);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddToCart(Cart cart)
        {
            try
            {
                if(CartList.Any(c=>c.UserId==cart.UserId && c.ProductId==cart.ProductId))
                {
                    CartList.SingleOrDefault(c => c.UserId == cart.UserId && c.ProductId == c.ProductId).Quantitity += cart.Quantitity;
                    return Ok("Quantity has been increased for the selected product in your cart.");
                }
                CartList.Add(cart);
                return Ok("New Item has been added to the cart.");
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpDelete("{UserId}/{ProductId}")]
        public IActionResult DeleteFromCart(int UserId,int ProductId)
        {
            try
            {
                Cart item = CartList.SingleOrDefault(c => c.UserId == UserId && c.ProductId == ProductId);
                CartList.Remove(item);
                return Ok("Item has been removed from your cart.");
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
