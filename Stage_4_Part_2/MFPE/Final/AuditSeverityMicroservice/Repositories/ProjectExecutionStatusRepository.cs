using AuditSeverityMicroservice.DBHelpers;
using AuditSeverityMicroservice.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Repositories
{
    public class ProjectExecutionStatusRepository : IProjectExecutionStatusRepository
    {
        readonly ILog _log4net = LogManager.GetLogger(typeof(ProjectExecutionStatusRepository));

        public List<AuditResponse> AuditResponseDetails()
        {
            try
            {
                _log4net.Info(" Http POST request from " + nameof(ProjectExecutionStatusRepository));
                List<AuditResponse> auditResponses = ProjectExecutionStatusDBHelper.AuditResponseDetails;
                return auditResponses;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message + " from " + nameof(ProjectExecutionStatusRepository));
                return null;
            }
        }
    }
}
