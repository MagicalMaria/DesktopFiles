using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleShowAPI.Models
{
    public class ShowContext:DbContext
    {
        public ShowContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ScheduleShow> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleShow>().HasData(new ScheduleShow
            {
                ShowId=1,
                ScreenName = "Kite",
                MovieID = 101,
                Time = "15:00"
            });
        }
    }
}
