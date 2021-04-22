using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPracticeCheckProject.Models
{
    public class CartViewModel
    {
        public int UserId { get; set; }
        public IList<MenuItem> CartItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
