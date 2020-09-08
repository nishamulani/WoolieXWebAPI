using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Models.Resource;

namespace WooliesX.Resources
{
    public class TrollyService
    {
        public decimal CalculateTrollyTotal(Trolly trolly)
        {

            decimal trollyTotal = 0;
            foreach (var product in trolly.Products)
            {
                var quantity = trolly.Quantities.FirstOrDefault(q => q.Name == product.Name);
                var special = trolly.Specials.FirstOrDefault(s => s.Quantities.Name == product.Name);

                if (quantity != null && special != null)
                {

                    if (quantity.Quantity >= special.Quantities.Quantity)
                    {
                        var specialUnit = quantity.Quantity / special.Quantities.Quantity;
                        var individualUnits = quantity.Quantity % special.Quantities.Quantity;
                        trollyTotal = trollyTotal + ((specialUnit * special.Total) + (individualUnits * product.Price));

                    }

                }
                if(quantity != null && special == null)
                {
                    trollyTotal = trollyTotal + (product.Price * quantity.Quantity);
                }
            }
            return trollyTotal;
        }
    }
}
