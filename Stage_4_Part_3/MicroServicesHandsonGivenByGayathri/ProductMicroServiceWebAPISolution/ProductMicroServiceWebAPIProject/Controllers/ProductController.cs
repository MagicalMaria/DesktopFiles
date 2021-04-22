using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMicroServiceWebAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroServiceWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static List<Product> Products = new List<Product>();

        [HttpGet]
        public ActionResult<IList<Product>> GetProducts()
        {
            try 
            {
                return Products;
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                Product product = Products.SingleOrDefault(p => p.Id == id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                Products.Add(product);
                return Ok("New Product is added.");
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        [HttpPut]
        public ActionResult<Product> EditProduct(Product product)
        {
            try
            {
                int index = Products.FindIndex(p => p.Id == product.Id);
                Products[index] = product;

                return Ok(Products[index]);
            }
            catch (Exception e)
            {
                return BadRequest("Bad Request - " + e.Message);
            }
        }

        //[HttpPut]
        //public IActionResult ChangePrice(int Id, double Price)
        //{
        //    try
        //    {
        //        int index = Products.FindIndex(p => p.Id == Id);

        //        Products[index].UnitPrice = Price;
        //        return Ok(Products[index]);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Bad Request - " + e.Message);
        //    }
        //}

        //[HttpPut]
        //public IActionResult ChangeQuantity(int Id, int Quantity)
        //{
        //    try
        //    {
        //        int index = Products.FindIndex(p => p.Id == Id);

        //        Products[index].Quantity += Quantity;
        //        return Ok(Products[index]);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Bad Request - " + e.Message);
        //    }
        //}

    }
}
