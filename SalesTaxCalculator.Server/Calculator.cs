using Microsoft.Extensions.Configuration;
using SalesTaxCalculator.Data;
using System;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Service
{
    public class Calculator : ICalculator
    {
        private readonly ICountyTaxRepository countyTaxRepository;


        public Calculator(IConfiguration configuration)
        {
            countyTaxRepository = new CountyTaxRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public Calculator(ICountyTaxRepository repository)
        {
            countyTaxRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// This method calculates the sales tax and returns 2 decimal values (The first value is for the tax rate and the second one is for the sales tax value)
        /// </summary>
        /// <param name="countyName"></param>
        /// <param name="price"></param>
        /// <returns>(taxRate, salesTaxValue)</returns>
        public async Task<(decimal, decimal)> CalculateSalesTax(string countyName, decimal price)
        {
            var countyTax = await countyTaxRepository.FindByCountyNameAsync(countyName);

            if (countyTax != null && countyTax.TaxRate >= 0m)
            {
                decimal taxValue = price * countyTax.TaxRate / 100m;
                return (countyTax.TaxRate, taxValue);
            }
            else
            {
                throw new ApplicationException($"Tax Rate of county {countyName} is not configured properly in the database");
            }
        }
    }
}
