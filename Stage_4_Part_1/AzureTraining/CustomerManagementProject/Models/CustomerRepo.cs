using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementProject.Models
{
    public class CustomerRepo : IRepo<Customer>
    {
        public List<Customer> Customers { get; set; }

        public CustomerRepo()
        {
            Customers = new List<Customer>()
            {
                new Customer
                {
                    Id=101,
                    Name="Ramu",
                    Age=21
                }
            };
        }

        public void AddItem(Customer Item)
        {
            Customers.Add(Item);
        }

        public IEnumerable<Customer> GetItems()
        {
            return Customers;
        }
    }
}
