using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public IEnumerable<int> Products { get; set; }
    }
}
