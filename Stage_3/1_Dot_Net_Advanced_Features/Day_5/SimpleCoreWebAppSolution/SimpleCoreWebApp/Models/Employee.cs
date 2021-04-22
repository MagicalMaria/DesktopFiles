using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCoreWebApp.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public bool IsPermanent { get; set; }
    }
}
