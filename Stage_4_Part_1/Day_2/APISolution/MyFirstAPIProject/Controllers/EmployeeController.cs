using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee(){ ID=101,Name="Ramu",Age=21},
            new Employee(){ ID=102,Name="Somu",Age=31},
            new Employee(){ ID=103,Name="Bimu",Age=29}
        };

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        //Very bad practice
        //[HttpGet]
        //[Route("GetAnother")]
        //public IEnumerable<string> Check()
        //{
        //     return new List<string>() { "Abc","Xyz" };
        //}
        
        [HttpGet]
        [Route("GetEmployee/{id}")]
        public Employee XYZ(int id)
        {
            return employees.Find(x => x.ID == id);
        }

        [HttpPut]
        [Route("{id}")]
        public Employee Put(int id,Employee employee)
        {
            var emp = employees.FindIndex(x => x.ID == id);
            employees[emp]=employee;
            return employees[emp];
        }

        [HttpPost]
        public void Post(Employee employee)
        {
            employees.Add(employee);
        }
    }
}
