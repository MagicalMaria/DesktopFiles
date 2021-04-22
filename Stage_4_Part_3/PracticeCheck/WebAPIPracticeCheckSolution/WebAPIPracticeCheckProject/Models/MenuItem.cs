using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPracticeCheckProject.Models
{
    public enum ItemCategory
    {
        Starter, MainCourse, Drinks, Dessert
    }
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool isActive { get; set; }
        public DateTime DateOfLaunch { get; set; }
        public ItemCategory Category { get; set; }
        public bool isFreeDelivery { get; set; }
    }
}
