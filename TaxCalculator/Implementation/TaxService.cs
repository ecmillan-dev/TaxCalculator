using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Models;

namespace TaxCalculator.Implementation
{
    public class TaxService : ITaxService
    {
        #region private members and constructor
        /// <summary>
        /// An instance of a Tax Calculator
        /// </summary>
        private readonly ITaxCalculator _taxCalculator;
        /// <summary>
        /// Logging via NLog
        /// </summary>
        private readonly ILogger<TaxService> _logger;

        public TaxService(ITaxCalculator taxCalculator, ILogger<TaxService> logger)
        {
            _taxCalculator = taxCalculator;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Retrieves the tax rate model for a zip code location (only accepts 5 digit or 9 digit zip codes)
        /// </summary>
        /// <param name="zip">the zip code (5 or 9 digits)</param>
        /// <returns>The tax rates (in the Tax Jar rate model) for that location</returns>
        public async Task<GeneralTaxRate> GetTaxRateForLocation(string zip)
        {
            try
            {
                return await _taxCalculator.GetTaxRateForLocation(zip);
            }
            catch (Exception ex)
            {
                // log exception
                _logger.LogError("General Exception in TaxService.GetTaxRateForLocation - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the taxes for a specific order from the Tax Jar API
        /// </summary>
        /// <param name="order">the posted order model with related search information</param>
        /// <returns>The order tax information</returns>
        public async Task<OrderTaxInformation> CalculateTaxesForOrder(OrderInformation order)
        {
            try
            {
                return await _taxCalculator.CalculateTaxesForOrder(order);
            }
            catch (Exception ex)
            {
                // log exception
                _logger.LogError("General Exception in TaxService.CalculateTaxesForOrder - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);
            }
        }
    }
}
