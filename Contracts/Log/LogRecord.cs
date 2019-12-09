using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Log
{
    public class LogRecord
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public LogLevel Level { get; set; }
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Fatal
    }
}
