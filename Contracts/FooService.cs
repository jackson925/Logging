using Contracts.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class FooService
    {
        private readonly ILogger _logger;
        private bool GetRandom => new Random().Next(0, 10) % 2 == 0;
        private Action LogRestarting => new Action(() => _logger.Information("{@Service} restarting", nameof(FooService)));
        private Action LogStarted => new Action(() => _logger.Information("{@Service} running", nameof(FooService)));


        public FooService(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<Option<IEnumerable<LogRecord>>> GenerateMockLogs()
        {
            return await Task.Run(() =>
            {
                if (GetRandom)
                {
                    return null;
                }

                var logs = new List<LogRecord>();

                try
                {
                    for (int i = 0; i < new Random().Next(0, 10); i++)
                    {
                        // Stimulate 10% error rate. 
                        if (new Random().Next(0, 100) < 10)
                        {
                            MockIOException();
                        } else
                        {
                            logs.Add(GetRandomLogRecord());
                        }
                    }
                }

                catch(Exception ex)
                {
                    _logger.Error(ex, "An error occured in {@Service} retrieving logs", nameof(FooService));
                    MockServiceRestart();
                }

                return !logs.Any()
                    ? Option<IEnumerable<LogRecord>>.ToErrorOption(null)
                    : Option<IEnumerable<LogRecord>>.ToSuccessOption(logs);
            });
        }


        private LogRecord GetRandomLogRecord()
        {
            return new LogRecord
            {
                Time = DateTime.Now,
                Message = GetRandomMessage(GetRandom),
                Level = GetRandomLevel(GetRandom)
            };
        }

        private LogLevel GetRandomLevel(bool error)
        {
            return error
                ? LogLevel.Error
                : LogLevel.Info;
        }

        private string GetRandomMessage(bool error)
        {
            return error
                ? "An error occured in Foo Service"
                : "All is well in Foo Serviceland";
        }

        private void MockServiceRestart()
        {
            LogRestarting();
            Task.Delay(2000);
            LogStarted();
            return;
        }

        private void MockIOException()
        {
            try
            {
                using (FileStream stream = File.Open(@"\logs", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException exception)
            {
                _logger.Error(exception, "An error occurered attempting to read logs from file");
            }

            MockServiceRestart();
        }


    }
}
