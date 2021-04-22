using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class AuditResponse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Audit Id: ")]
        public int AuditId { get; set; }

        [Display(Name = "Project Execution Status: ")]
        public string ProjectExecutionStatus { get; set; }

        [Display(Name = "Remedial Action Duration: ")]
        public string RemedialActionDuration { get; set; }
    }
}
