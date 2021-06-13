# TaxCalculator
A .NET Core API to handle several functionalities based on the TaxJar API by use of a wrapper service. The solution contains 2 projects, a web API and a test project

## TaxCalculator.API
TaxCalculator.API is a RESTful API documented using Swagger that allows a user to select from two exposed endpoints for tax calculations
* **GetTaxRateForLocation** - GET - takes in a zip code, returns the tax rates that apply to that location (country, state, county, city, etc)
* **CalculateTaxesForOrder** - POST - takes in a model containing to and from locations for an order as well as other related details and returns the tax total for that specific order

The TaxCalculator.API uses an injected instance of TaxService, which is a service to select between various TaxCalculator implementations defined with a common TaxCalculator interface. This project only contains a single TaxCalculator implementation, which is a wrapper for API methods contained within the Tax Jar API (additional details can be found here https://developers.taxjar.com/api/reference/#sales-tax-api).

Once more TaxCalculator implementations are added to the solution, there will need to be custom functionality to switch between various Tax Calculators based on which customer is using the Tax Service.

The implementations utilize NLog for exception logging, which can be easily configured to log to other locations (currently the logger is configured to log to a local text file).

## TaxCalculator.Test
TaxCalculator.Test is a Unit Test project using NUnit to unit test the functionality for both TaxService and TaxCalculator's calls for CalculateTaxesForOrder. They utilize a proven example of output from the documentation of the Tax Jar API endpoint for calculating taxes for an order and verify that the numbers are equal.
