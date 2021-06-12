using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    /// <summary>
    /// General Tax rate structure for the Tax Jar API tax rate model
    /// </summary>
    public class GeneralTaxRate
    {
        public string zip { get; set; }
        public string country { get; set; }
        public float country_rate { get; set; }
        public string county { get; set; }
        public float county_rate { get; set; }
        public string state { get; set; }
        public float state_rate { get; set; }
        public string city { get; set; }
        public float city_rate { get; set; }
        public float combined_district_rate { get; set; }
        public float combined_rate { get; set; }
        public bool freight_taxable { get; set; }
    }
}
