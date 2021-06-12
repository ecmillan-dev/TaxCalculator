﻿using Microsoft.AspNetCore.Http;
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
    public class TaxJarAdapterController : ControllerBase
    {
        #region members/constructor
        /// <summary>
        /// Service for the Tax Jar API methods
        /// </summary>
        private ITaxJarAdapter _taxJarService;

        /// <summary>
        /// Constructor to bind the tax jar service
        /// </summary>
        /// <param name="taxJarService"></param>
        public TaxJarAdapterController (ITaxJarAdapter taxJarService)
        {
            _taxJarService = taxJarService;
        }
        #endregion

        /// <summary>
        /// Retrieves the tax rate model for a zip code location (only accepts 5 digit or 9 digit zip codes)
        /// </summary>
        /// <param name="zip">the zip code (5 or 9 digits)</param>
        /// <returns>The tax rates (in the Tax Jar rate model) for that location</returns>
        [HttpGet("GetTaxRateForLocation/{zip}")]
        public async Task<ActionResult<GeneralTaxRate>> GetTaxRateForLocation(string zip)
        {
            try
            {
                // validate the zip code
                if (Regex.IsMatch(zip, @"^[0-9]{5}(?:-[0-9]{4})?$"))
                {
                    return Ok(await _taxJarService.GetTaxRateForLocation(zip));
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
        /// Retrieves the taxes for a specific order from the Tax Jar API
        /// </summary>
        /// <param name="order">the posted order model with related search information</param>
        /// <returns>The order tax information</returns>
        [HttpPost("CalculateTaxesForOrder")]
        public async Task<ActionResult<OrderTaxInformation>> CalculateTaxesForOrder([FromBody] OrderInformation order)
        {
            try
            {
                return Ok(await _taxJarService.CalculateTaxesForOrder(order));
            }

            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex);
                return result;
            }

        }
    }
}
