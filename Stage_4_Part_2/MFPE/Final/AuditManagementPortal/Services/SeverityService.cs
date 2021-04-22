using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using AuditManagementPortal.Repositories;
using Microsoft.Extensions.Configuration;

namespace AuditManagementPortal.Services
{
    public class SeverityService : ISeverityService
    {
        private IConfiguration _configuration;
        private SeverityRepo _severityRepo;

        public SeverityService(IConfiguration configuration)
        {
            _configuration = configuration;
            _severityRepo = new SeverityRepo(_configuration);
        }
        public AuditResponse GetInternalResponse(string token, InternalQuestions questions)
        {
            try
            {
                var auditResponse = _severityRepo.GetInternalResponse(token, questions);
                return auditResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public AuditResponse GetSOXResponse(string token, SOXQuestions questions)
        {
            try
            {
                var auditResponse = _severityRepo.GetSOXResponse(token, questions);
                return auditResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
