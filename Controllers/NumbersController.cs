using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apigateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {
        private static readonly ConcurrentBag<int> _numbers = new ConcurrentBag<int>(Enumerable.Range(1,10));
        private readonly ILogger<NumbersController> _logger;

        public NumbersController(ILogger<NumbersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"{DateTime.Now} - returning numbers.");

            return Ok(
                new
                {
                    set = _numbers.AsEnumerable(),
                    time = DateTime.Now
                });
        }

        [HttpPost]
        public IActionResult Post([FromBody] int number)
        {
            _numbers.Add(number);

            _logger.LogInformation($"{DateTime.Now} - number {number} added to set.");

            return Accepted();
        }
    }
}
