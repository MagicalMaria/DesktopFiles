using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerManagementRepository<Customer> _repository;

        public CustomerController(ICustomerManagementRepository<Customer> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                List<Customer> customers = _repository.GetAllData().ToList();
                return Ok(customers);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                Customer customer = _repository.GetById(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
            }
            catch
            {
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                _repository.Add(customer);
                return Ok();
            }
            catch
            {
                return BadRequest("Couldn't update");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Customer customer)
        {
            if(customer==null)
            {
                return BadRequest("Customer object is null");
            }
            try
            {
                _repository.Update(id, customer);
            }
            catch
            {
                return BadRequest("Customer details not found");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest("Couldn't delete...");
            }
        }
    }
}
