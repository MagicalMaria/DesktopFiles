using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSiteWebAPIProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
