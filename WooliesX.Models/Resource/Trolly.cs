using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.Models.Resource
{
    public class Trolly
    {
        public  IEnumerable<Product> Products { get; set; }
        public IEnumerable<Special> Specials { get; set; }
        public IEnumerable<Quantities> Quantities { get; set; }
    }

    public class Special
    {
        public decimal Total { get; set; }
        public Quantities Quantities { get; set; }
    }
    public class Quantities
    {
        public int Quantity { get; set; }
        public string Name { get; set; }

    }
}
