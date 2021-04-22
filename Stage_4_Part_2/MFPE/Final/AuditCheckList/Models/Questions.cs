using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Models
{
    public class Questions
    {
        [Key]
        public int QuestionNo { get; set; }       //Primary key
        public string Question { get; set; }

    }
}
