using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPracticeCheckProject.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User UserDetails { get; set; }
        public int MenuItemID { get; set; }
        public MenuItem menuItem { get; set; }
    }
}
