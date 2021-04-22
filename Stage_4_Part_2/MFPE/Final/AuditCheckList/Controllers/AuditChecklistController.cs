using AuditCheckList.Models;
using AuditCheckList.ServiceProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditChecklistController : ControllerBase
    {
        private readonly IChecklistProvider checklistProviderobj;
        readonly log4net.ILog _log4net;


        //Constructor
        public AuditChecklistController(IChecklistProvider _checklistProviderobj)
        {
            checklistProviderobj = _checklistProviderobj;
            _log4net = log4net.LogManager.GetLogger(typeof(AuditChecklistController));

        }

        // GET: api/AuditChecklist
        [HttpGet("{auditType}")]
        public IActionResult GetQuestions(string auditType)
        {
            _log4net.Info("AuditChecklistController Http GET request called" + nameof(AuditChecklistController));

            //Chech for input String 
            if (string.IsNullOrEmpty(auditType))
                return BadRequest("No Input");

            //Validate the input provided 
            if ((auditType != "Internal") && (auditType != "SOX"))
                return Ok("Wrong Input");

            try
            {
                List<Questions> list = checklistProviderobj.QuestionsProvider(auditType);
                return Ok(list);
            }
            catch (Exception e) 
            {
                _log4net.Error("Exception " + e.Message + nameof(AuditChecklistController));
                return BadRequest();
            }
        }
    }
}
