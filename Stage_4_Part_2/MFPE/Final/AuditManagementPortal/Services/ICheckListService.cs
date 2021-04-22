using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;

namespace AuditManagementPortal.Services
{
    public interface ICheckListService
    {
        public List<CheckListQuestion> GetQuestions(string auditType, string token);
    }
}
