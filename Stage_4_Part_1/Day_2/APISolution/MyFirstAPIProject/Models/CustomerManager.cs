using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Models
{
    public class CustomerManager : ICustomerManagementRepository<Customer>
    {
        CustomerManagementContext _context;

        public CustomerManager(CustomerManagementContext context)
        {
            _context = context;
        }

        public void Add(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.FirstOrDefault(c => c.CustomerId == id));
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAllData()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void Update(int id, Customer entity)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            customer.Name = entity.Name;
            customer.IsOldCustomer = entity.IsOldCustomer;
            customer.Phone = entity.Phone;
            _context.SaveChanges();
        }
    }
}
