using SalesTaxCalculator.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Data
{
    public interface ICountyTaxRepository
    {
        Task<CountyTax> FindByCountyNameAsync(string countyName);
    }
}
