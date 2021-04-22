using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_2_Hands_on_1_SwaggerDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public bool Permanent { get; set; }
        
        //public Department Department { get; set; }
        //public List<Skill> Skills { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
