using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly MovieManagementContext _context;

        public AdminController(MovieManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            try
            {
                return Ok(await _context.Movies.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {
            try
            {
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return Ok("New Movie added...");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> Put(int id,Movie movie)
        {
            try
            {
                Movie mov = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
                if(mov!=null)
                {
                    mov.Title = movie.Title;
                    mov.BoxOffice = movie.BoxOffice;
                    mov.IsActive = movie.IsActive;
                    mov.DateOfLaunch = movie.DateOfLaunch;
                    mov.HasTeaser = movie.HasTeaser;

                    await _context.SaveChangesAsync();
                    return Ok(mov);
                }
                else
                {
                    return BadRequest("Invalid Movie Id");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Movie movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
                if(movie!=null)
                {
                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();

                    return Ok("Movie was deleted...");
                }
                else
                {
                    return BadRequest("Invalid Movie Id");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

    }
}
