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
            calc.Setup(async c =>
            {
                Models.GeneralTaxRate generalTaxRate = await c.GetTaxRateForLocation("35758");
                return generalTaxRate;
            }).Returns(new System.Threading.Tasks.Task<Models.GeneralTaxRate>());
        }

        [Test]
        public void Test1()
        {
            string greeting = "Hello, get-testy.";
            Assert.That(greeting, Is.EqualTo("Hello, get-testy."));
            Assert.Pass();
            
        }
    }
}