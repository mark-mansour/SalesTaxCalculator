using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxCalculator.Shared.Model
{
    public class SalesPrice
    {
        public string CountyName { get; set; }
        public string Price { get; set; }
        public string TaxRate { get; set; }
        public string TotalPrice { get; set; }
    }
}
