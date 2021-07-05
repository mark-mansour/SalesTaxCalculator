using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SalesTaxCalculator.Data.Test
{
    [TestClass]
    public class CountyTaxRepositoryTest
    {
        private static IConfigurationRoot config;
        private ICountyTaxRepository countyTaxRepository;

        [TestInitialize]
        public void ConnectToDB()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();

            countyTaxRepository = new CountyTaxRepository(config.GetConnectionString("DefaultConnection"));
        }


        [TestMethod]
        public void Fetch_Existing_CountyTax_ByCountyName()
        {
            var durhamCountyTax = countyTaxRepository.FindByCountyName("Durham County");

            Assert.IsNotNull(durhamCountyTax);
            Assert.AreEqual("Durham County", durhamCountyTax.CountyName);
            Assert.IsTrue(durhamCountyTax.TaxRate >= 0m);
        }



        [TestMethod]
        public void Fetch_NonExisting_CountyTax_ByCountyName()
        {
            var nonExistingCountyTax = countyTaxRepository.FindByCountyName("Non Existing County");

            Assert.IsNull(nonExistingCountyTax);
        }
    }
}
