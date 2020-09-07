using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.Models.Resource
{
    public class Product
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price{ get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}
