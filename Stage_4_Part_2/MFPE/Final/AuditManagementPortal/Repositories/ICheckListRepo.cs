using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;

namespace AuditManagementPortal.Repositories
{
    public interface ICheckListRepo
    {
        public List<CheckListQuestion> GetQuestions(string auditType, string token);
    }
}
