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

            if(trolly.Product.Name == trolly.Quantities.Name && trolly.Product.Name == trolly.Special.Quantities.Name)
            {
               if ( trolly.Quantities.Quantity >= trolly.Special.Quantities.Quantity )
                {
                    var specialUnit = trolly.Quantities.Quantity / trolly.Special.Quantities.Quantity;
                    var individualUnits = trolly.Quantities.Quantity % trolly.Special.Quantities.Quantity;
                    trollyTotal = (specialUnit * trolly.Special.Total) + (individualUnits * trolly.Product.Price);

                }
               else
                {
                    trollyTotal = trolly.Quantities.Quantity * trolly.Product.Price;
                }
                
            }
            else
            {
                //error
            }
           
            return trollyTotal;
        }
    }
}
