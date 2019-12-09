using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Log;
using Serilog;

namespace Application
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;
        private readonly ILoggingGateway _loggingGateway;

        public LoggingService(ILogger logger, ILoggingGateway loggingGateway)
        {
            _logger = logger;
            _loggingGateway = loggingGateway;
        }
        public async Task<IEnumerable<LogRecord>> Get()
        {

            var logs = new List<LogRecord>();

            var time = new Stopwatch();

            time.Start();

            while(time.ElapsedMilliseconds < 1000)
            {
                var newLogs = await _loggingGateway.GetLogsFromFooService();

                if(!newLogs.HasValue || newLogs.IsError)
                {
                    _logger.Warning($"service returned empty log batch");
                } 
                else
                {
                    _logger.Information("service returned {@logCount} logs", newLogs.Value.ToList().Count);
                    logs.AddRange(newLogs.Value);
                }

            }

            time.Stop();

            _logger.Information("Found {@Count} logs. Logs: {@Logs}", logs.Count, logs.Select(l => l));

            return logs;
        }
    }
}
