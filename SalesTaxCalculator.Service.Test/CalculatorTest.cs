using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalesTaxCalculator.Data;
using SalesTaxCalculator.Shared.Model;
using System;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Service.Test
{
    [TestClass]
    public class CalculatorTest
    {
        private ICountyTaxRepository countyTaxRepoMock;

        [TestInitialize]
        public void Initialize()
        {
            countyTaxRepoMock = Mock.Of<ICountyTaxRepository>();
            Mock.Get(countyTaxRepoMock)
                .Setup(x => x.FindByCountyNameAsync(It.IsAny<string>()))
                .Returns((string x) =>
                        Task.Run(() =>
                        {
                            if (string.Equals(x, "Non Existing County"))
                            {
                                return null;
                            }
                            else
                            {
                                return new CountyTax
                                {
                                    CountyTaxKey = Guid.NewGuid(),
                                    CountyName = x,
                                    TaxRate = 6.75m
                                };
                            }
                        }
                    ));

        }

        [TestMethod]
        [DataRow("Durham County", "1")]
        [DataRow("Durham County", "0")]
        [DataRow("Durham County", "100")]
        [DataRow("Durham County", "0.7")]
        public async Task CalculateSalesTax_ExistingCounty(string countyName, string stringPrice)
        {
            decimal decimalPrice = Decimal.Parse(stringPrice);
            Calculator calc = new Calculator(countyTaxRepoMock);

            var result = await calc.CalculateSalesTax(countyName, decimalPrice).ConfigureAwait(false);
            Assert.AreEqual(6.75m, result.Item1);   // first value is the tax rate
            Assert.AreEqual(decimalPrice * 0.0675m, result.Item2);  // second value is the sales tax based on the price and the tax rate
        }


        [TestMethod]
        public void CalculateSalesTax_NonExistingCounty()
        {
            Calculator calc = new Calculator(countyTaxRepoMock);

            Assert.ThrowsExceptionAsync<ApplicationException>(() => calc.CalculateSalesTax("Non Existing County", 100m));
        }
    }
}
