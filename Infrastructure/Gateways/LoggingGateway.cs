using Contracts;
using Contracts.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Gateways
{
    public class LoggingGateway : ILoggingGateway
    {
        private readonly ILogger _logger;

        public LoggingGateway(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<Option<IEnumerable<LogRecord>>> GetLogsFromFooService()
        {
            FooService fooService = new FooService(_logger);

            var logResult = await fooService.GenerateMockLogs();

            return Option<IEnumerable<LogRecord>>.ToOption(logResult?.Value);

        }
    }
}
