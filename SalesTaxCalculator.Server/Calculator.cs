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

        public async Task<decimal> CalculateSalesTax(string countyName, decimal price)
        {
            var countyTax = await countyTaxRepository.FindByCountyNameAsync(countyName);

            if (countyTax != null && countyTax.TaxRate >= 0m)
            {
                decimal tax = price * countyTax.TaxRate / 100;
                return tax;
            }
            else
            {
                throw new ApplicationException($"Tax Rate of county {countyName} is not configured properly in the database");
            }
        }
    }
}
