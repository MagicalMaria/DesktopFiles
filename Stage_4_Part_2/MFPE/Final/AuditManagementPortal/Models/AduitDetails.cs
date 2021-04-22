using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class AuditDetails
    {
        [Display(Name = "Audit Type")]
        [Required]
        public string Type { get; set; }

        public Questions Questions { get; set; }    //Navigation Property

        public DateTime AuditDate { get; set; }
    }
}
