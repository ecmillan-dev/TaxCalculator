using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    /// <summary>
    /// wrapper for the OrderTaxInformation class
    /// </summary>
    public class OrderTaxInfromationWrapper
    {
        public OrderTaxInformation tax { get; set; }
    }
}
