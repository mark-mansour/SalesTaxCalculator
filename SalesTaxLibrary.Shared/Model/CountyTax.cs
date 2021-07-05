using System;

namespace SalesTaxCalculator.Shared.Model
{
    public class CountyTax
    {
        public Guid CountyTaxKey { get; set; }
        public string CountyName { get; set; }
        public decimal TaxRate { get; set; }
    }
}
