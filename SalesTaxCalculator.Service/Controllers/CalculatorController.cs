using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesTaxCalculator.Service;
using SalesTaxCalculator.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesTaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator calculator;

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ICalculator calc, ILogger<CalculatorController> logger)
        {
            calculator = calc ?? throw new ArgumentNullException(nameof(calc));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Get()
        {
            var salesPrice = new SalesPrice
            {
                CountyName = "Please enter county name and other parameters in the URL. For example: https://localhost:44348/api/calculator/Vance%20County/100",
                Price = String.Format("{0:C}", 0m),
                TaxRate = $"{0m}%",
                TotalPrice = String.Format("{0:C}", 0m)
            };

            string jsonSalesPrice = JsonSerializer.Serialize(salesPrice);

            return jsonSalesPrice;
        }


        // GET api/<CalculatorController>/county/price
        [HttpGet("{countyName}/{price}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string countyName, decimal price)
        {
            try
            {
                var result = await calculator.CalculateSalesTax(countyName, price);
                var salesPrice = new SalesPrice
                {
                    CountyName = countyName,
                    Price = String.Format("{0:C}", price),
                    TaxRate = $"{result.Item1}%",
                    TotalPrice = String.Format("{0:C}", price + result.Item2)
                };

                string jsonSalesPrice = JsonSerializer.Serialize(salesPrice);

                return Ok(jsonSalesPrice);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error for request: [{nameof(CalculatorController)}/{nameof(Get)}/{countyName}/{price}]");
                return BadRequest();
            }
        }

    }
}
