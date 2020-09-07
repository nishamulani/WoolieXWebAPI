using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.Models.Resource
{
    public class ShopperHistory
    {
        public int CustomerId {get;set;}

        public IEnumerable<Product> Products { get; set; }
    }
}
