using Dapper;
using SalesTaxCalculator.Shared.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CountyTax> FindByCountyNameAsync(string countyName)
        {
            return await this.dbConnection.QueryFirstOrDefaultAsync<CountyTax>("SELECT * FROM COUNTYTAX WHERE COUNTYNAME = @COUNTYNAME", new { countyName });
        }
    }
}
