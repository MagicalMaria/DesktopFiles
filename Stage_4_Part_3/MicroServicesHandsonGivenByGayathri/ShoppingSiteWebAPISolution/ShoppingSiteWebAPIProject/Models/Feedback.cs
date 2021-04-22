using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Models
{
    public class Feedback
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string FeedbackDescription { get; set; }
    }
}
