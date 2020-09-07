using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Connector;
using WooliesX.Interface;
using WooliesX.Models.Resource;

namespace WooliesX.Resources
{
    public class ProductService
    {

        private static string Resource_Products_API_URL = "/api/resource/products";




        private IJsonConnector _wooliesXProductConnector;
        public IJsonConnector WooliesXProductConnector
        {
            get
            {
                if (_wooliesXProductConnector == null)
                {
                    return _wooliesXProductConnector = new JsonConnector(string.Format("{0}{1}",
                        ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.URL"], Resource_Products_API_URL),
                        HttpVerb.Get, "", "application/json");

                }
                return _wooliesXProductConnector;
            }
        }

        private ShopperHistoryService ShopperHistoryService = new ShopperHistoryService();

        public IEnumerable<Product> GetSortedProducts(string SortOption)
        {
            try
            {
                var products = GetProducts();
                switch (SortOption)
                {
                    case "Low":
                        {

                            products = products.OrderBy(x => x.Price);
                            return products;
                        }
                    case "High":

                        {
                            products = products.OrderByDescending(x => x.Price);
                            return products;
                        }

                    case "Ascending":
                        {
                            products = products.OrderBy(x => x.Name);
                            return products;
                        }
                    case "Descending":
                        {
                            products = products.OrderByDescending(x => x.Name);
                            return products;
                        }


                    case "Recommended":

                        {
                            // add null Check 
                            List<Product> popularProducts = GetPopularProducts(products);

                            var sortedProducts = popularProducts.OrderByDescending(x => x.Quantity).Distinct();

                            return sortedProducts;
                        }

                }
                return products;
            }
            catch (Exception ex)
            {
                // add better messaging
                throw ex;
            }
        }

        private List<Product> GetPopularProducts(IEnumerable<Product> products)
        {
            var shopperHistoryDetails = ShopperHistoryService.GetShopperHistoryDetails();

            var productList = shopperHistoryDetails.SelectMany(x => x.Products).GroupBy(s => s.Name)
               .Select(
                   g => new Product
                   {

                       Quantity = g.Sum(s => s.Quantity),
                       Name = g.First().Name,
                       Price = g.First().Price
                   });

            List<Product> mergedList = products
             .GroupJoin(
                 productList, left => left.Name, right => right.Name,
                 (x, y) => new { Left = x, Right = y }
             )
             .SelectMany(
                 x => x.Right.DefaultIfEmpty(),
                 (x, y) => new Product
                 {
                     Name = x.Left.Name,
                     Price = x.Left.Price,
                     Quantity = y == null ? x.Left.Quantity : y.Quantity

                 }
             ).ToList();
            return mergedList;
        }

        public IEnumerable<Product> GetProducts()
        {

            var data = string.Format("?token={0}", ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.Token"]);
            var json = WooliesXProductConnector.MakeRequest(data);
      
            var productDetails = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

            return productDetails;
        }

    }
}
