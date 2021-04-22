using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        public bool IsOldCustomer { get; set; }
        
        [Required(ErrorMessage = "Phone cannot be empty")]
        public string Phone { get; set; }
        

        public IEnumerable<Bill> Bills { get; set; }
    }
}
