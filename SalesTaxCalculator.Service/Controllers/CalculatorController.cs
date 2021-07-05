using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesTaxCalculator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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


        // GET api/<CalculatorController>/county/price
        [HttpGet("{countyName}/{price}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string countyName, decimal price)
        {
            try
            {
                var salesTax = await calculator.CalculateSalesTax(countyName, price);
                return Ok(salesTax);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error for request: [{nameof(CalculatorController)}/{nameof(Get)}/{countyName}/{price}]");
                return BadRequest();
            }
        }

    }
}
