using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIFinalCheckProject.Models;
using WebAPIFinalCheckProject.Models.DTOs;

namespace WebAPIFinalCheckProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieManagementContext _context;

        public CustomerController(MovieManagementContext context)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Favourite>> Get(int id)
        {
            try
            {
                List<Movie> favouriteMovies = await _context.Favourites.Where(f => f.UserId == id).Select(f => f.Movie).ToListAsync();
                FavouriteDTO favouriteDTO = new FavouriteDTO
                {
                    UserId = id,
                    Favourites = favouriteMovies,
                    FavouriteCount = favouriteMovies.Count
                };

                return Ok(favouriteDTO);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Favourite favourite)
        {
            try
            {
                if(await _context.Favourites.AnyAsync(c=>c.UserId==favourite.UserId && c.MovieId==favourite.MovieId))
                {
                    return Content("This is already in your favourites.");
                }
                else
                {
                    await _context.Favourites.AddAsync(favourite);
                    await _context.SaveChangesAsync();
                    return Ok("Movie has been added to your favouries");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpDelete("{UserId}/{MovieId}")]
        public async Task<IActionResult> Delete(int UserId,int MovieId)
        {
            try
            {
                Favourite movie = await _context.Favourites.SingleOrDefaultAsync(f => f.UserId == UserId && f.MovieId == MovieId);
                _context.Favourites.Remove(movie);
                await _context.SaveChangesAsync();
                return Ok("Movie has been removed from your favourites");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }
    }
}
