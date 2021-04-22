using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class InternalQuestions
    {
        [Display(Name = "Have all Change requests followed SDLC before PROD move?")]
        public bool Question1 { get; set; }

        [Display(Name = "Have all Change requests been approved by the application owner?")]
        public bool Question2 { get; set; }

        [Display(Name = "Are all artifacts like CR document, Unit test cases available?")]
        public bool Question3 { get; set; }

        [Display(Name = "Is the SIT and UAT sign-off available?")]
        public bool Question4 { get; set; }

        [Display(Name = "Is data deletion from the system done with application owner approval?")]
        public bool Question5 { get; set; }
    }
}
