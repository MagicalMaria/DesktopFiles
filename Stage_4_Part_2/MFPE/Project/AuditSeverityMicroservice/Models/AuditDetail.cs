using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class AuditDetail
    {
        public string Type { get; set; }
        public DateTime AuditDate { get; set; }
        public Questions Questions { get; set; }
    }
}
