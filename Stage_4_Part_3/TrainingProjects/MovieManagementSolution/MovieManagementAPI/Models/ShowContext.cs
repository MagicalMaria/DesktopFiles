using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagementAPI.Models
{
    public class ShowContext:DbContext
    {
        public ShowContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 101,
                Name = "X-Men",
                Duration = 122.3f,
                Remarks = "Sci-Fi"
            });
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
