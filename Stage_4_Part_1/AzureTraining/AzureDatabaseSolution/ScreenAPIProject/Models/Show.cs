using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenAPIProject.Models
{
    public class Show
    {
        [Key]
        public string ScreenName { get; set; }
        public int Capacity { get; set; }
    }
}
