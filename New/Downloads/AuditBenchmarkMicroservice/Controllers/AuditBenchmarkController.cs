using AuditBenchmarkMicroservice.Models;
using AuditBenchmarkMicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditBenchmarkController : ControllerBase
    {
        private readonly IAuditBenchmarkService objService;
        public AuditBenchmarkController(IAuditBenchmarkService _objService)
        {
           
            objService = _objService;
        }
      
        [HttpGet]
        public IActionResult Get()
        {
            List<AuditBenchmark> listOfService = new List<AuditBenchmark>();
          
            try
            {
                listOfService = objService.GetBenchmarksList();
                return Ok(listOfService);
            }
            catch (Exception e)
            {
                Console.WriteLine( " Exception here" + e.Message);
                return StatusCode(500);
            }
        }
    }
}
