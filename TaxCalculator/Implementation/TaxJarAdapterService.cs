using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaxCalculator.Interface;
using TaxCalculator.Models;

namespace TaxCalculator.Implementation
{
    public class TaxJarAdapterService : ITaxJarAdapter
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
        /// Bind the configuration and intialize API variables
        /// </summary>
        /// <param name="configuration"></param>
        public TaxJarAdapterService(IConfiguration configuration)
        {
            _configuration = configuration;
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
            }
            catch (Exception ex)
            {
                // log exception
            }
            return null;
        }
    }
}
