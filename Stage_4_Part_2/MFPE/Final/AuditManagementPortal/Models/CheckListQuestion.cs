using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class CheckListQuestion
    {
        [Display(Name = "#")]
        public int QuestionNo { get; set; }
        public string Question { get; set; }
    }
}
