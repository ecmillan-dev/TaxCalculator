using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    /// <summary>
    /// Wrapper class for the tax rate class due to return format of the api
    /// </summary>
    public class TaxRateApiReturn
    {
        public GeneralTaxRate Rate { get; set; }
    }
}
