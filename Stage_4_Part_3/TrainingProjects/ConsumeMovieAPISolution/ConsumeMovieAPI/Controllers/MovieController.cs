using ConsumeMovieAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeMovieAPI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseUri;

        public MovieController(IConfiguration configuration)
        {
            _config = configuration;
            baseUri = _config.GetValue<string>("ShowWebAPIBaseUrl");
        }

        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            List<Movie> movies = null;
            HttpClient client = new HttpClient();
            string url = _config["ShowWebAPIBaseUrl"];
            string Uri = "http://localhost:60199/api/movie";
            var myResponse = await client.GetAsync(Uri);

            if(myResponse.StatusCode==HttpStatusCode.OK)
            {
                var moviesRaw = myResponse.Content.ReadAsAsync<List<Movie>>();
                movies = moviesRaw.Result;
                return View(movies);
            }
            return View();
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            HttpClient client = new HttpClient();
            string Uri = baseUri;
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                var myResponse = await client.PostAsync(Uri, content);
                if(myResponse.StatusCode==HttpStatusCode.OK)
                {
                    var rawResult = JsonConvert.SerializeObject(movie);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.Clear();
                ModelState.AddModelError("Error", "Error in creating a new movie");
                return View();
            }
            return View();
        }

        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient client = new HttpClient();
            string Uri = baseUri;
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                var myResponse = await client.GetAsync(Uri + "/" + id);
                if(myResponse.StatusCode==HttpStatusCode.OK)
                {
                    var rawResult = myResponse.Content.ReadAsAsync<Movie>();
                    Movie movie = rawResult.Result;
                    return View(movie);
                }
            }
            catch(Exception)
            {
                return View();
            }
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Movie movie)
        {
            HttpClient client = new HttpClient();
            string Uri = baseUri;
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                var myResponse = await client.PutAsync(Uri + "/" + movie.Id, content);
                if(myResponse.StatusCode==HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: MovieController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            string Uri = baseUri;
            try
            {
              //  StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                var myResponse = await client.GetAsync(Uri + "/" + id);
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    var rawResult = myResponse.Content.ReadAsAsync<Movie>();
                    Movie movie = rawResult.Result;
                    return View(movie);
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            HttpClient client = new HttpClient();
            string Uri = baseUri;
            try
            {
                var myResponse = await client.DeleteAsync(Uri + "/" + id);
                if(myResponse.StatusCode==HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}
