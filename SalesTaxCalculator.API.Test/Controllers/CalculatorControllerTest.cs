using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxCalculator.API.Controllers;
using SalesTaxCalculator.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Threading.Tasks;
using System.Text.Json;
using SalesTaxCalculator.Shared.Model;
using System.Collections;

namespace SalesTaxCalculator.API.Controllers.Test
{
    [TestClass()]
    public class CalculatorControllerTest
    {
        private ICalculator calculatorMock;
        private ILogger<CalculatorController> logMock;


        [TestInitialize]
        public void Initialize()
        {
            calculatorMock = Mock.Of<ICalculator>();
            Mock.Get(calculatorMock)
                .Setup(x => x.CalculateSalesTax(It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns((string countyName, decimal price) => Task.Run(() => (6.75m, price * 6.75m / 100)));

            logMock = Mock.Of<ILogger<CalculatorController>>();
        }

        [TestMethod()]
        public async Task GetTest()
        {
            string inputCountyName = "Durham County";
            decimal price = 100m;
            decimal taxRate = 6.75m;

            CalculatorController calculator = new CalculatorController(calculatorMock, logMock);
            var result = await calculator.Get(inputCountyName, price);

            Assert.IsNotNull(result);
            var stringResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value.ToString();
            Assert.IsFalse(string.IsNullOrWhiteSpace(stringResult));
            SalesPrice salesPrice = JsonSerializer.Deserialize<SalesPrice>(stringResult);
            Assert.IsNotNull(salesPrice);
            Assert.AreEqual(inputCountyName, salesPrice.CountyName);
            Assert.AreEqual(String.Format("{0:C}", price), salesPrice.Price);
            Assert.AreEqual($"{taxRate}%", salesPrice.TaxRate);
            Assert.AreEqual(String.Format("{0:C}", price + (price * taxRate / 100m)), salesPrice.TotalPrice);
        }
    }
}