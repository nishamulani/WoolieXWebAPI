
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WooliesX.Models;
using WooliesX.Models.Resource;
using WooliesX.Resources;
using WooliesX.Users;

namespace WooliesX.WebAPI.Controllers
{
    public class ProductsController :  ApiController
    {
        
        public ProductService ProductService = new ProductService();

        [HttpGet]
        [ActionName("products")]
        // GET: Product
        public IEnumerable<Product> Get()
        {
            var products = ProductService.GetProducts();
            return products;

        }

        [HttpGet]
        [ActionName("sort")]
        public IEnumerable<Product> Sort(string sortOption)
        {
            var products = ProductService.GetSortedProducts(sortOption);
            return products;

        }
    }
}