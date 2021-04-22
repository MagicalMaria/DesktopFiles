using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPracticeCheckProject.Models
{
    public class MenuItemOperation:DbContext
    {
        public MenuItemOperation(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                            .HasData(new MenuItem
                            {
                                Id = 1,
                                Name = "Poori",
                                Price = 20,
                                isActive = true,
                                DateOfLaunch = DateTime.ParseExact("20/03/2021", "dd/mm/yyyy", null),
                                Category = ItemCategory.MainCourse,
                                isFreeDelivery = true
                            });
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
