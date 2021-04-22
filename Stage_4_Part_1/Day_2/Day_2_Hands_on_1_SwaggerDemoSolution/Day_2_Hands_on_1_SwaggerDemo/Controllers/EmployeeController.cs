using Day_2_Hands_on_1_SwaggerDemo.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles ="HR")]
    public class EmployeeController : ControllerBase
    {
        private List<Employee> employees;

        public EmployeeController()
        {
            employees = new List<Employee>
            {
                new Employee(){ Id=1,Name="Ramu",Salary=10000,Permanent=true,DateOfBirth=DateTime.ParseExact("20/06/1996","dd/mm/yyyy",null) },
                new Employee(){ Id=2,Name="Ragu",Salary=20000,Permanent=false,DateOfBirth=DateTime.ParseExact("20/06/1996","dd/mm/yyyy",null) },
                new Employee(){ Id=3,Name="Raju",Salary=30000,Permanent=true,DateOfBirth=DateTime.ParseExact("20/06/1996","dd/mm/yyyy",null) },
                new Employee(){ Id=4,Name="Raguvaran",Salary=40000,Permanent=false,DateOfBirth=DateTime.ParseExact("20/06/1996","dd/mm/yyyy",null) },
                new Employee(){ Id=5,Name="Raja",Salary=50000,Permanent=true,DateOfBirth=DateTime.ParseExact("20/06/1996","dd/mm/yyyy",null) }
            };
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return employees;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return GetStandardEmployeeList();
        }

        [HttpGet("{id}")]
       // [Route("{id}")]
        public Employee Get(int id)
        {
            return employees.SingleOrDefault(e=>e.Id==id);
        }

        [HttpPost]
        public Employee Post([FromBody]Employee employee)
        {
            employees.Add(employee);

            return employees.SingleOrDefault(e => e.Id == employee.Id);
        }

        [HttpPut("{id}")]
       // [Route("{id}")]
        public Employee Put(int id,[FromBody]Employee employee)
        {
            Employee emp = employees.SingleOrDefault(e => e.Id == id);
            emp.Name = employee.Name;
            emp.Salary = employee.Salary;
            emp.Permanent = employee.Permanent;
            emp.DateOfBirth = employee.DateOfBirth;

            return emp;
        }



    }
}
