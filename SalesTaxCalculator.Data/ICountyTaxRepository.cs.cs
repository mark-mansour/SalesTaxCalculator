using SalesTaxCalculator.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxCalculator.Data
{
    public interface ICountyTaxRepository
    {
        CountyTax FindByCountyName(string countyName);
    }
}
