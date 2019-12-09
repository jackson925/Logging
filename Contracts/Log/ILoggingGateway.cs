using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Log
{
    public interface ILoggingGateway
    {
        Task<Option<IEnumerable<LogRecord>>> GetLogsFromFooService();
    }
}
