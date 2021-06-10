using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bffe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var correlationId = Guid.NewGuid();
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                _logger.LogTrace("Getting data from: {service}", "be");

                var result = await "http://be/bemain"
                    .WithHeader("X-CorrelationId", correlationId)
                    .GetJsonAsync<IEnumerable<string>>();

                _logger.LogDebug("Obtained data: {data}", string.Join(", ", result));
                return result;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            _logger.LogTrace("Posting");

            try
            {
                var result = await "http://be/bemain".PostAsync();
                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not post");
                return StatusCode(500);
            }

        }
    }
}
