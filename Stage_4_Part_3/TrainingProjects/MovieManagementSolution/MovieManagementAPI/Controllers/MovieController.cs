using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ShowContext _context;

        public MovieController(ShowContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            List<Movie> movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            Movie movie = await _context.Movies.SingleOrDefaultAsync(m=>m.Id==id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Post(Movie movie)
        {
            try
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return BadRequest("Exception..." + e.Message);
            }
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> Put(int id, Movie movie)
        {
            try
            {
                Movie mov = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
                if(mov!=null)
                {
                    mov.Name = movie.Name;
                    mov.Duration = movie.Duration;
                    mov.Remarks = movie.Remarks;
                    await _context.SaveChangesAsync();

                    return Ok(mov);
                }
                else
                {
                    return NotFound("Record not found...");
                }
            }
            catch(Exception e)
            {
                return BadRequest("Exception..." + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> Delete(int id)
        {
            try
            {
                Movie mov = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
                if (mov != null)
                {
                    _context.Remove(mov);
                    await _context.SaveChangesAsync();
                    return Ok(mov);
                }
                else
                {
                    return NotFound("Record not found...");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Exception..." + e.Message);
            }
        }
    }
}
