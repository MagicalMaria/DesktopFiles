using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagementAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name cannot be null")]
        public string Name { get; set; }
        public float Duration { get; set; }
        public string Remarks { get; set; }
    }
}
