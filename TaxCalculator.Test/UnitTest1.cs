using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using TaxCalculator.Implementation;
using TaxCalculator.Interface;

namespace TaxCalculator.Test
{

    public class Tests
    {
        private ITaxCalculator taxCalculator;
        private IConfiguration configuration;
        private ITaxService taxService;
        private Controllers.TaxServiceController taxServiceController;
        protected Mock<ITaxService> MockedTaxService => Mock.Get(taxService);

        private IServiceProvider ServicesProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            // initialize config (we can just ignore the logger for now)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
            
            // bind services
            taxCalculator = new TaxJarAdapterService(configuration, null);
            taxService = new Implementation.TaxService(taxCalculator, null);

            
            // Use DI to get instances of IMatchService            

        }
       


        [Test]
        public async Task TestCalculateTaxesForOrderService()
        {
            // var taxService2 = ServicesProvider.GetService<ITaxService>();

            var model = new Models.OrderInformation()
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                to_city = "Los Angeles",
                to_street = "1335 E 103rd St",
                amount = 15,
                shipping = (float)1.5,
                nexus_addresses = new Models.NexusAddress[1],
                line_items = new Models.LineItem[1],
            };

            // init the arrays
            model.nexus_addresses[0] = new Models.NexusAddress()
            {
                id = "Main Location",
                country = "US",
                zip = "92093",
                state = "CA",
                city = "La Jolla",
                street = "9500 Gilman Drive",
            };
            model.line_items[0] = new Models.LineItem()
            {
                id = "1",
                quantity = 1,
                product_tax_code = "20010",
                unit_price = 15,
                discount = 0,
            };

            // hardcoded from last successful test with that input data set
            float expectedResult = (float)1.43;
            var result = await taxService.CalculateTaxesForOrder(model);
            Assert.AreEqual(expectedResult, result);
            
        }

        [Test]
        public async Task TestCalculateTaxesForOrderCalculator()
        {
            // var taxService2 = ServicesProvider.GetService<ITaxService>();

            var model = new Models.OrderInformation()
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                to_city = "Los Angeles",
                to_street = "1335 E 103rd St",
                amount = 15,
                shipping = (float)1.5,
                nexus_addresses = new Models.NexusAddress[1],
                line_items = new Models.LineItem[1],
            };

            // init the arrays
            model.nexus_addresses[0] = new Models.NexusAddress()
            {
                id = "Main Location",
                country = "US",
                zip = "92093",
                state = "CA",
                city = "La Jolla",
                street = "9500 Gilman Drive",
            };
            model.line_items[0] = new Models.LineItem()
            {
                id = "1",
                quantity = 1,
                product_tax_code = "20010",
                unit_price = 15,
                discount = 0,
            };

            // hardcoded from last successful test with that input data set
            float expectedResult = (float)1.43;
            var result = await taxCalculator.CalculateTaxesForOrder(model);
            Assert.AreEqual(expectedResult, result);

        }
    }
}