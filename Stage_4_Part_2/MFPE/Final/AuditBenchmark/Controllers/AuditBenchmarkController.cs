using AuditBenchmarkMicroservice.Models;
using AuditBenchmarkMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AuditBenchmarkController : ControllerBase
    {
        private readonly log4net.ILog _log4net;
        private readonly IAuditBenchmarkService objService;
        public AuditBenchmarkController(IAuditBenchmarkService _objService)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkController));
            objService = _objService;
        }
      
        [HttpGet]
        public IActionResult Get()
        {
            _log4net.Info(" Http GET request " + nameof(AuditBenchmarkController));
            List<AuditBenchmark> listOfService = new List<AuditBenchmark>();
          
            try
            {
                listOfService = objService.GetBenchmarksList();
                return Ok(listOfService);
            }
            catch (Exception e)
            {
                _log4net.Error(" Exception here" + e.Message + " " + nameof(AuditBenchmarkController));
                return StatusCode(500);
            }
        }
    }
}
