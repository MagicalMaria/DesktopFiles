using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIPracticeCheckProject.Models;

namespace WebAPIPracticeCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly MenuItemOperation _context;

        public CustomerController(MenuItemOperation context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuItem>>> Get()
        {
            try
            {
                List<MenuItem> menuList = await _context.MenuItems.Where(m => m.isActive == true && m.DateOfLaunch < DateTime.Now).ToListAsync();
                return Ok(menuList);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request... " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartViewModel>> Get(int id)
        {
            try
            {
                List<MenuItem> cartItems = await _context.Carts.Where(c => c.UserId == id).Select(c => c.menuItem).ToListAsync();
                CartViewModel cartViewModel = new CartViewModel
                {
                    UserId = id,
                    CartItems = cartItems,
                    TotalPrice = cartItems.Sum(m => m.Price)
                };

                return Ok(cartViewModel);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request : " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cart cartItem)
        {
            try
            {
                await _context.Carts.AddAsync(cartItem);
                await _context.SaveChangesAsync();
                return Ok("Item added to your cart");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request : " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, Cart cartItem)
        {
            try
            {
                Cart item = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == cartItem.UserId && c.Id == id);
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
                return Ok("Item removed from your cart");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request : " + e.Message);
            }
        }
    }
}
