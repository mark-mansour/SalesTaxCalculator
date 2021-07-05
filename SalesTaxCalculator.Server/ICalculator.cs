using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Service
{
    public interface ICalculator
    {
        Task<(decimal, decimal)> CalculateSalesTax(string countyName, decimal price);
    }
}
