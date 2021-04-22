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
    public class AnonymousUserController : ControllerBase
    {
        private readonly MenuItemOperation _context;

        public AnonymousUserController(MenuItemOperation context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<MenuItem>> Get()
        {
            return await _context.MenuItems.ToListAsync();
        }
    }
}
