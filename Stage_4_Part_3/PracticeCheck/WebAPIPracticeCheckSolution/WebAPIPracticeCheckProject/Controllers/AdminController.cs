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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly MenuItemOperation _context;

        public AdminController(MenuItemOperation context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<MenuItem>> Get()
        {
            return await _context.MenuItems.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItem>> Put(int id,[FromBody]MenuItem menuItem)
        {
            MenuItem menu = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            menu.Name = menuItem.Name;
            menu.Price = menuItem.Price;
            menu.isActive = menuItem.isActive;
            menu.DateOfLaunch = menuItem.DateOfLaunch;
            menu.Category = menuItem.Category;
            menu.isFreeDelivery = menuItem.isFreeDelivery;
            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MenuItem menuItem)
        {
            try
            {
                await _context.MenuItems.AddAsync(menuItem);
                await _context.SaveChangesAsync();
                return Ok("New Menu Item was added...");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request... " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
                _context.MenuItems.Remove(item);
                await _context.SaveChangesAsync();

                return Ok("Menu item was deleted...");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request... " + e.Message);
            }
        }
    }
}
