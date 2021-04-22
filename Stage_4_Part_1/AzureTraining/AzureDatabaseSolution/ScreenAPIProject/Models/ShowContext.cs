using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenAPIProject.Models
{
    public class ShowContext:DbContext
    {
        public ShowContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Show> Shows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>().HasData(new Show
            {
                ScreenName="Kite",
                Capacity=30
            });
        }
    }
}
