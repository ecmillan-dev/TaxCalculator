using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxServiceController : ControllerBase
    {
        #region members/constructor
        /// <summary>
        /// Service for the Tax Jar API methods
        /// </summary>
        private ITaxService _taxService;

        /// <summary>
        /// Constructor to bind the tax jar service
        /// </summary>
        /// <param name="taxJarService"></param>
        public TaxServiceController (ITaxService taxService)
        {
            _taxService = taxService;
        }
        #endregion

        /// <summary>
        /// Retrieves the tax rate model for a zip code location (only accepts 5 digit or 9 digit zip codes)
        /// </summary>
        /// <param name="zip">the zip code (5 or 9 digits)</param>
        /// <returns>The tax rates  for that location</returns>
        [HttpGet("GetTaxRateForLocation/{zip}")]
        public async Task<ActionResult<GeneralTaxRate>> GetTaxRateForLocation(string zip)
        {
            try
            {
                // validate the zip code
                if (Regex.IsMatch(zip, @"^[0-9]{5}(?:-[0-9]{4})?$"))
                {
                    return Ok(await _taxService.GetTaxRateForLocation(zip));
                }
                else
                {
                    // return error message specifying zip code requirements
                    var result = StatusCode(StatusCodes.Status500InternalServerError, "Invalid zip code format - must be 5 digits or 9 digits");
                    return result;
                }
            }

            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex);
                return result;
            }
            
        }

        /// <summary>
        /// Retrieves the taxes for a specific order from a Tax Calculator
        /// </summary>
        /// <param name="order">the posted order model with related search information</param>
        /// <returns>The order tax information</returns>
        [HttpPost("CalculateTaxesForOrder")]
        public async Task<ActionResult<float>> CalculateTaxesForOrder([FromBody] OrderInformation order)
        {
            try
            {
                return Ok(await _taxService.CalculateTaxesForOrder(order));
            }

            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex);
                return result;
            }

        }
    }
}
