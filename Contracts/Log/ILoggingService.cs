using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Log
{
    public interface ILoggingService
    {
        Task<IEnumerable<LogRecord>> Get();
    }
}
