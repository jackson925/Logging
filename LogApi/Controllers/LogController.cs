using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Log;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ILoggingService _loggingService;

        public LogController(ILogger logger, ILoggingService loggingService)
        {
            _logger = logger;
            _loggingService = loggingService;
        }
        

        [HttpGet]
        [Route("")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            var logs = await _loggingService.Get();

            return new JsonResult(logs);
        }
    }
}