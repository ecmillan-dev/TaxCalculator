using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    /// <summary>
    /// The search model to post for an order for calculating taxes for an order 
    /// </summary>
    public class OrderInformation
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public float amount { get; set; }
        public float shipping { get; set; }
        public string customer_id { get; set; }
        public string extemption_type { get; set; }
        public NexusAddress[] nexus_addresses { get; set; }
        public LineItem[] line_items { get; set; }        
    }
}
