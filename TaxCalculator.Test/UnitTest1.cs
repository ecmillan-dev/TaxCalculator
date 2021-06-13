using Moq;
using NUnit.Framework;
using System;
using TaxCalculator.Implementation;
using TaxCalculator.Interface;

namespace TaxCalculator.Test
{
    public class Tests
    {
        private TaxService taxService;
        [SetUp]
        public async void Setup()
        {
            var calc = new Mock<ITaxCalculator>();
            //calc.Setup(async c =>
            //{
            //    Models.GeneralTaxRate generalTaxRate = await c.GetTaxRateForLocation("35758");
            //    return generalTaxRate;
            //}).Returns(new System.Threading.Tasks.Task<Models.GeneralTaxRate>());
        }

        [Test]
        public void Test1()
        {
            string greeting = "Hello, get-testy.";
            Assert.That(greeting, Is.EqualTo("Hello, get-testy."));
            Assert.Pass();
            
        }

        [Test]
        public async void TestCalculateTaxesForOrder()
        {
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




  

            // expected result -  "amount_to_collect": 1.35,
        }
    }
}