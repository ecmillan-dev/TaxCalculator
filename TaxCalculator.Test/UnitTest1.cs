using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using TaxCalculator.Implementation;
using TaxCalculator.Interface;

namespace TaxCalculator.Test
{
    public class Tests
    {
        private ITaxService taxService;
        protected Mock<ITaxService> MockedTaxService => Mock.Get(taxService);

        private IServiceProvider ServicesProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<IMatchRepository, MatchRepositoryStub>();

            var serviceProvider = services.BuildServiceProvider();

            matchRepository = serviceProvider.GetService<IMatchRepository>();


            //calc.Setup(async c =>
            //{
            //    Models.GeneralTaxRate generalTaxRate = await c.GetTaxRateForLocation("35758");
            //    return generalTaxRate;
            //}).Returns(new System.Threading.Tasks.Task<Models.GeneralTaxRate>());
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITaxService>();
            // https://stackoverflow.com/a/67806752
        }

        [Test]
        public void Test1()
        {
            string greeting = "Hello, get-testy.";
            Assert.That(greeting, Is.EqualTo("Hello, get-testy."));
            
        }

        [Test]
        public void TestCalculateTaxesForOrder()
        {
            var calc = new Mock<ITaxCalculator>();
            calc.0
            
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

            var value = calc.Setup(c => c.CalculateTaxesForOrder(model).Result).Returns<float>(x => x);
     //       _mock.Setup(x => x.DoSomething(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
     //.Returns((string a, string b, string c) => string.Concat(a, b, c));
            Assert.AreEqual(value, (float)1.35);
            //Assert.That(model.from_country, Is.EqualTo("US"));

            // Assert.AreEqual("1725 Slough Avenue", customer.Street);

            // https://stackoverflow.com/questions/38605935/how-to-mock-a-service-with-moq-and-nunit-in-mvc-application-prevent-nulls
            // https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection
            // expected result -  "amount_to_collect": 1.35,
        }
    }
}