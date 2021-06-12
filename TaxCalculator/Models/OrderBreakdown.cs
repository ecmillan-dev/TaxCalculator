﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class OrderBreakdown
    {
        public float taxable_amount { get; set; }
        public float tax_collectable { get; set; }
        public float combined_tax_rate { get; set; }
        public float state_taxable_amount { get; set; }
        public float state_tax_rate { get; set; }
        public float state_tax_collectable { get; set; }
        public float county_taxable_amount { get; set; }
        public float county_tax_rate { get; set; }
        public float county_tax_collectable { get; set; }
        public float city_taxable_amount { get; set; }
        public float city_tax_rate { get; set; }
        public float city_tax_collectable { get; set; }
        public float special_district_taxable_amount { get; set; }
        public float special_tax_rate { get; set; }
        public float special_district_tax_collectable { get; set; }
        public OrderBreakdownLineItem[] line_items { get; set; }
        
    }
}
