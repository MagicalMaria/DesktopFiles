using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class AuditRequest
    {
        public string ProjectName { get; set; }
        public string ProjectManagerName { get; set; }
        public string ApplicationOwnerName { get; set; }
        public AuditDetail AuditDetails { get; set; }
    }
}
