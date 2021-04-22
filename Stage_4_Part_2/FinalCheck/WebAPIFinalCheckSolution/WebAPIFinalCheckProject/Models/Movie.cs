using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFinalCheckProject.Models
{
    public enum Genre
    {
        ScienceFiction, SuperHero, Romance, Comedy, Adventure, Thriller
    }

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double BoxOffice { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfLaunch { get; set; }
        public Genre Genre { get; set; }
        public bool HasTeaser { get; set; }
    }
}
