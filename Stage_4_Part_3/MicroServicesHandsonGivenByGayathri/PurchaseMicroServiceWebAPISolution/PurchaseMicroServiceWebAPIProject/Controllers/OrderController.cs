using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseMicroServiceWebAPIProject.Models;
using PurchaseMicroServiceWebAPIProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroServiceWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public static List<Order> Orders = new List<Order>();
        public static List<OrderedProductsList> ListOfOrderedProducts = new List<OrderedProductsList>();
        private static int OrderCount = 0;

        [HttpPost]
        public IActionResult PlaceOrder(OrderDTO orderDTO)
        {
            try
            {
                orderDTO.Id = ++OrderCount;

                Orders.Add(new Order
                {
                    Id = orderDTO.Id,
                    UserId = orderDTO.UserId,
                    OrderDate = orderDTO.OrderDate,
                    OrderAmount = orderDTO.OrderAmount
                });

                foreach (int ProdId in orderDTO.Products)
                {
                    ListOfOrderedProducts.Add(new OrderedProductsList
                    {
                        OrderId = orderDTO.Id,
                        ProductId = ProdId
                    });
                }

                foreach(var id in orderDTO.Products)
                {
                    var item = CartController.CartList.SingleOrDefault(c => c.UserId == orderDTO.UserId && c.ProductId == id);
                    CartController.CartList.Remove(item);
                }

                return Ok("New order has been placed.");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpDelete("{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                Order orderItem = Orders.Find(o => o.Id == orderId);
                Orders.Remove(orderItem);

                var orderedProducts = ListOfOrderedProducts.Where(o => o.OrderId == orderId).ToList();

                foreach(var item in orderedProducts)
                {
                    ListOfOrderedProducts.Remove(item);
                }

                return Ok("Your Order was cancelled successfully.");
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
