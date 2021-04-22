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
    public class UserController : ControllerBase
    {
        private readonly MenuItemOperation _context;

        public UserController(MenuItemOperation context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO user)
        {
            try
            {
                if(user.Password==user.ConfirmPassword)
                {
                    await _context.Users.AddAsync(new User { UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password });
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest("Password and Confirm Password must match.");
                }
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request : " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<bool>> Get(int id,[FromBody]string password)
        {
            try
            {
                User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id && u.Password == password);
                if(user!=null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch(Exception e)
            {
                return BadRequest("Bad Request : " + e.Message);
            }
        }

    }
}
