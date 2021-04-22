using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIFinalCheckProject.Models;

namespace WebAPIFinalCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousUserController : ControllerBase
    {
        private readonly MovieManagementContext _context;

        public AnonymousUserController(MovieManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Movie>>> Get()
        {
            try
            {
                List<Movie> movies = await _context.Movies.Where(m => m.IsActive == true && m.DateOfLaunch < DateTime.Now).ToListAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
