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
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;


namespace WooliesX.Resources
{
    public class ProductService
    {

        private static string Resource_Products_API_URL = "/api/resource/products";


        private TelemetryClient telemetryClient = new TelemetryClient();

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



        public IEnumerable<Product> GetProducts()
        {
            try
            {

                var data = string.Format("?token={0}", ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.Token"]);
                var json = WooliesXProductConnector.MakeRequest(data);
                telemetryClient.TrackTrace($"JSON from Product API: {json}", SeverityLevel.Information);
                var productDetails = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
            

                return productDetails;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackTrace($"Error in retrieving products:{ex.Message})", SeverityLevel.Error);
                return null;
            }
           
        }

        public IEnumerable<Product> GetSortedProducts(string SortOption)
        {
            var products = GetProducts();
            switch (SortOption)
            {
                case "Low":
                    return products.OrderBy(x => x.Price);
                case "High":
                    return products.OrderByDescending(x => x.Price);
                case "Ascending":
                    return products.OrderBy(x => x.Name);
                case "Descending":
                    return products.OrderByDescending(x => x.Name);

                case "Recommended":
                    {
                        List<Product> popularProducts = GetPopularProducts(products);
                        if (popularProducts != null)
                        {
                            var sortedProducts = popularProducts.OrderByDescending(x => x.Quantity).Distinct();
                            return sortedProducts;
                        }
                        return products;
                    }
            }

            return products;
        }

        private List<Product> GetPopularProducts(IEnumerable<Product> products)
        {
            var shopperHistoryService = new ShopperHistoryService();
            try
            {
                var shopperHistoryDetails = shopperHistoryService.GetShopperHistoryDetails();

                var productList = shopperHistoryDetails.SelectMany(x => x.Products).GroupBy(s => s.Name)
                   .Select(
                       g => new Product
                       {

                           Quantity = g.Sum(s => s.Quantity),
                           Name = g.First().Name,
                           Price = g.First().Price
                       });

                //form a group join to have a left join so that if the shopper didnt shop the items
                //that's actually avaialble gets included as well
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
            catch (Exception ex)
            {
                telemetryClient.TrackTrace($"Error in retrieving popular products:{ex.Message})", SeverityLevel.Error);
                return null;
            }

         
        }



    }
}
