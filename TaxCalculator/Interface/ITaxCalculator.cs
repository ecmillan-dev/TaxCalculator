using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Interface
{
    /// <summary>
    /// Interface for a Tax Calculator
    /// </summary>
    public interface ITaxCalculator
    {
        /// <summary>
        /// Retrieves the tax rate model for a zip code location (only accepts 5 digit or 9 digit zip codes)
        /// </summary>
        /// <param name="zip">the zip code (5 or 9 digits)</param>
        /// <returns>The tax rates for that location</returns>
        Task<GeneralTaxRate> GetTaxRateForLocation(string zip);

        /// <summary>
        /// Retrieves the taxes for a specific order 
        /// </summary>
        /// <param name="order">the posted order model with related search information</param>
        /// <returns>The order tax information</returns>
        Task<float> CalculateTaxesForOrder(OrderInformation order);
    }
}
