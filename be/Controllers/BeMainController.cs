using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeMainController : ControllerBase
    {
        private readonly ILogger<BeMainController> _logger;

        public BeMainController(ILogger<BeMainController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromHeader(Name = "X-CorrelationId")] Guid correlationId)
        {
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                _logger.LogTrace("Getting string data");
                _logger.LogDebug("Processing request");
                var result = new[] { "one", "two", "three" };
                _logger.LogDebug("Data obtained: {result}", result);
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            throw new NotImplementedException("This method is not implemented in Backend");
        }
    }
}
