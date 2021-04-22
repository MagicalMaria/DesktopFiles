using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class AuditRequest
    {
        [Display(Name = "Project Name")]
        [Required]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Project Manager")]
        public string ProjectManagerName { get; set; }

        [Required]
        [Display(Name = "Application Owner")]
        public string ApplicationOwnerName { get; set; }

        public AuditDetails AuditDetails { get; set; }
    }
}
