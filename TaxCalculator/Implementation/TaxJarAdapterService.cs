using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Models;

namespace TaxCalculator.Implementation
{
    /// <summary>
    /// A service implementation of the Tax Jar version of a Tax Calculator
    /// </summary>
    public class TaxJarAdapterService : ITaxCalculator
    {
        #region private members and constructor
        /// <summary>
        /// The configuration for app settings access
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Base URL for the API access to the Tax Jar API
        /// </summary>
        private string _apiBaseUrl { get; set; }

        /// <summary>
        /// Authentication token for the Tax Jar API
        /// </summary>
        private string _authToken { get; set; }

        /// <summary>
        /// Logging via NLog
        /// </summary>
        private readonly ILogger<TaxJarAdapterService> _logger;

        /// <summary>
        /// Bind the configuration and intialize API variables
        /// </summary>
        /// <param name="configuration"></param>
        public TaxJarAdapterService(IConfiguration configuration, ILogger<TaxJarAdapterService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            // set the api vars
            _apiBaseUrl = _configuration.GetSection("TaxJarApi").GetValue<string>("BaseUrl");
            _authToken = _configuration.GetSection("TaxJarApi").GetValue<string>("ApiKey");            
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
                // initialize an http client
                using (var httpClient = new HttpClient())
                {
                    // set address and authentication
                    httpClient.BaseAddress = new Uri(_apiBaseUrl);
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);

                    // make the request
                    using (var response = await httpClient.GetAsync("rates/" + zip))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogError("API Request Error status code - " + response.StatusCode);                            
                            throw new HttpRequestException("Request failed on status code returned: " + response.StatusCode);
                        }

                        // parse the return if successful
                        var sResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TaxRateApiReturn>(sResponse);
                        return result.Rate;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // log exception
                _logger.LogError("HttpRequestException in TaxJarAdapterService.GetTaxRateForLocation - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);
                
            }
            catch (Exception ex)
            {
                // log exception
                _logger.LogError("General Exception in TaxJarAdapterService.GetTaxRateForLocation - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the taxes for a specific order from the Tax Jar API
        /// </summary>
        /// <param name="order">the posted order model with related search information</param>
        /// <returns>The order tax information</returns>
        public async Task<float> CalculateTaxesForOrder(OrderInformation order)
        {
            //OrderTaxInfromationWrapper
            try
            {
                // initialize an http client
                using (var httpClient = new HttpClient())
                {
                    // set address and authentication
                    httpClient.BaseAddress = new Uri(_apiBaseUrl);
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authToken);

                    // serialize the order to add to the request
                    var orderJson = JsonConvert.SerializeObject(order);
                    var content = new StringContent(orderJson, Encoding.UTF8, "application/json"); 
                    // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                    // make the request
                    using (var response = await httpClient.PostAsync("taxes/", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogError("API Request Error status code - " + response.StatusCode);                            
                            throw new HttpRequestException("Request failed on status code returned: " + response.StatusCode);
                        }

                        // parse the return if successful
                        var sResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<OrderTaxInfromationWrapper>(sResponse);
                        return result.tax.amount_to_collect;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // log exception
                _logger.LogError("HttpRequestException in TaxJarAdapterService.CalculateTaxesForOrder - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);

            }
            catch (Exception ex)
            {
                // log exception
                _logger.LogError("General Exception in TaxJarAdapterService.CalculateTaxesForOrder - " + ex.Message);

                // return simple exception
                throw new Exception("The request encountered an exception (view NLog file for additional details) - " + ex.Message);
            }
        }
    }
}
