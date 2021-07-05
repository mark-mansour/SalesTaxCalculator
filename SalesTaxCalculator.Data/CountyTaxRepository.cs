using Dapper;
using SalesTaxCalculator.Shared.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SalesTaxCalculator.Data
{
    public class CountyTaxRepository : ICountyTaxRepository
    {
        private readonly IDbConnection dbConnection;

        public CountyTaxRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            this.dbConnection = new SqlConnection(connectionString);
        }

        public CountyTax FindByCountyName(string countyName)
        {
            return this.dbConnection.Query<CountyTax>("SELECT * FROM COUNTYTAX WHERE COUNTYNAME = @COUNTYNAME", new { countyName }).SingleOrDefault();
        }
    }
}
