using AuditSeverityMicroservice.Models;
using AuditSeverityMicroservice.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectExecutionStatusController : ControllerBase
    {
        private readonly IProjectExecutionStatusService _service;

        public ProjectExecutionStatusController(IProjectExecutionStatusService service)
        {
            _service = service;
        }

        readonly ILog _log4net = LogManager.GetLogger(typeof(ProjectExecutionStatusController));

        [HttpPost]
        public IActionResult GetAuditResponse([FromBody]AuditRequest auditRequest)
        {
            _log4net.Info("Entered into the Post Method named as " + nameof(GetAuditResponse) + " of " + nameof(ProjectExecutionStatusController));

            if (auditRequest==null || auditRequest.AuditDetails.Type==null || (auditRequest.AuditDetails.Type != "Internal" && auditRequest.AuditDetails.Type != "SOX"))
            {
                return BadRequest("Invalid Audit Request");
            }

            try
            {
                //string token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
                AuditResponse auditResponse = _service.GetProjectExecutionStatusData(auditRequest/*, token*/);
                if(auditResponse==null)
                {
                    return StatusCode(500);
                }
                return Ok(auditResponse);
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured at " + nameof(ProjectExecutionStatusController) + " - " + nameof(GetAuditResponse) + " : " + e.Message);
                return StatusCode(500);
            }
        }
    }
}
