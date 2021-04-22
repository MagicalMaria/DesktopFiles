using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeMovieAPI.Models
{
    public class Movie
    {
        [Required(ErrorMessage = "ID cannot be null")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be null")]
        public string Name { get; set; }
        public float Duration { get; set; }
        public string Remarks { get; set; }
    }
}
