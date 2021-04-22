using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class SOXQuestions
    {
        [Display(Name = "Have all Change requests followed SDLC before PROD move?")]
        public bool Question1 { get; set; }

        [Display(Name = "Have all Change requests been approved by the application owner?")]
        public bool Question2 { get; set; }

        [Display(Name = "For a major change, was there a database backup taken before and after PROD move?")]
        public bool Question3 { get; set; }

        [Display(Name = "Has the application owner approval obtained while adding a user to the system?")]
        public bool Question4 { get; set; }

        [Display(Name = "Is data deletion from the system done with application owner approval?")]
        public bool Question5 { get; set; }
    }
}
