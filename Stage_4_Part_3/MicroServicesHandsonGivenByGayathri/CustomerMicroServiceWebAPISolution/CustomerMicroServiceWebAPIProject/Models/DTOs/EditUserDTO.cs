using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroServiceWebAPIProject.Models.DTOs
{
    public class EditUserDTO
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
    }
}
