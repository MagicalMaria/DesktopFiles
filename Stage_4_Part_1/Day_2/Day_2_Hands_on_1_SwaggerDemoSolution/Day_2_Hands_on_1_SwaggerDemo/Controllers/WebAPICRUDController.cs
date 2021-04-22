using Day_2_Hands_on_1_SwaggerDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_2_Hands_on_1_SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebAPICRUDController : ControllerBase
    {
        public static List<Employee> employees=new List<Employee>();
        

        public WebAPICRUDController()
        {
            
        }

        [HttpGet]
        public List<Employee> Get()
        {
            return employees;
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employees.SingleOrDefault(e=>e.Id==id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if(employees.Find(e=>e.Id==employee.Id)!=null)
            {
                return BadRequest("Employee with the Same ID is already available...");
            }
            employees.Add(employee);
            return Ok("Successful!!! Added New Employee...");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody]Employee employee)
        {
            Employee emp = employees.SingleOrDefault(e => e.Id == id);
            emp.Name = employee.Name;
            emp.Salary = employee.Salary;
            emp.Permanent = employee.Permanent;
            emp.DateOfBirth = employee.DateOfBirth;

            return Ok("Record Modified Successfully!!!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee emp = employees.SingleOrDefault(e => e.Id == id);
            if (employees.Remove(emp))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
