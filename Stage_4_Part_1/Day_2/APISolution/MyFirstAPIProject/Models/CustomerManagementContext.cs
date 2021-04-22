using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Models
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 1,
                Name = "Ramu",
                IsOldCustomer = false,
                Phone = "9876543210"
            });
        }
    }
}
