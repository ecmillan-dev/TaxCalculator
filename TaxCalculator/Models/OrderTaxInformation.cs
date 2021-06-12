using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    /// <summary>
    /// Contains the information for taxes for an order in the Tax Jar API
    /// </summary>
    public class OrderTaxInformation
    {
        public float order_total_amount { get; set; }
        public float shipping  { get; set; }
        public float taxable_amount { get; set; }
        public float amount_to_collect { get; set; }
        public float rate { get; set; }
        public bool has_nexus { get; set; }
        public bool freight_taxable { get; set; }
        public string tax_source { get; set; }
        public OrderJurisdiction jurisdictions { get; set; }
        public OrderBreakdown breakdown { get; set; }
    }
}
