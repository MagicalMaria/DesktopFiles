using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScheduleShowAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleShowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowMovieController : ControllerBase
    {
        // GET: api/<ShowMovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64095");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("api/Movies");
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IEnumerable<MovieDTO>>().Result;
                return result.ToList();
            }
            return BadRequest("Sorry could not access the API");
        }

        // GET api/<ShowMovieController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShowMovieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movieDTO)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64095");
            StringContent content = new StringContent(JsonConvert.SerializeObject(movieDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Movies",content);
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<MovieDTO>().Result;
                return Ok(result);
            }
            return BadRequest("Sorry could not access the API");
        }

        // PUT api/<ShowMovieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShowMovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
