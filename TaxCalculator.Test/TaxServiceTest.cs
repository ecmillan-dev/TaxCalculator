using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Test
{
    public class TaxServiceTest : Interface.ITaxService
    {
        public Task<float> CalculateTaxesForOrder(OrderInformation order)
        {
            return Task.Factory.StartNew(() => (float)1.43); 
        }

        public Task<GeneralTaxRate> GetTaxRateForLocation(string zip)
        {
            throw new NotImplementedException();
        }
    }
}
