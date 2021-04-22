using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;

namespace AuditManagementPortal.Repositories
{
    public interface ISeverityRepo
    {
        public AuditResponse GetInternalResponse(string token, InternalQuestions questions);
        public AuditResponse GetSOXResponse(string token, SOXQuestions questions);
    }
}
