using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFinalCheckProject.Models.DTOs
{
    public class FavouriteDTO
    {
        public int UserId { get; set; }
        public List<Movie> Favourites { get; set; }
        public int FavouriteCount { get; set; }
    }
}
