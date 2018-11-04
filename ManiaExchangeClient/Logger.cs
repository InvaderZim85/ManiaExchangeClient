using System;
using System.Runtime.CompilerServices;
using NLog;

namespace ManiaExchangeClient
{
    public static class Logger
    {
        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <param name="message">The message</param>
        public static void Info(string message)
        {
            var log = new LogEventInfo
            {
                Level = LogLevel.Info,
                Message = message
            };

            WriteLog(log);
        }
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message</param>
        public static void Debug(string message)
        {
            // Log only when the program is in the debug mode
            WriteLog(new LogEventInfo
            {
                Level = LogLevel.Debug,
                Message = message
            });
        }
        /// <summary>
        /// Loags a warn message
        /// </summary>
        /// <param name="message">The message</param>
        public static void Warn(string message)
        {
            WriteLog(new LogEventInfo
            {
                Level = LogLevel.Warn,
                Message = message
            });
        }
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message</param>
        public static void Error(string message)
        {
            WriteLog(new LogEventInfo
            {
                Level = LogLevel.Error,
                Message = message
            });
        }
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="ex">The exception</param>
        /// <param name="callerMemberName">The caller member name (filled automatically)</param>
        /// <param name="callerFilePath">The caller file path (filled automatically)</param>
        /// <param name="callerLineNumber">The caller line number (filled automatically)</param>
        public static void Error(string message, Exception ex, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logEvent = new LogEventInfo
            {
                Level = LogLevel.Error,
                Message = $"{message}" +
                          "\r\n\r\nAdditional info:" +
                          $"\r\n\t- Member name: {callerMemberName}" +
                          $"\r\n\t- Filepath..: {callerFilePath}" +
                          $"\r\n\t- Line number: {callerLineNumber}\r\n",
                Exception = ex,
            };

            logEvent.Properties.Add("callerpath", callerFilePath);
            logEvent.Properties.Add("callermember", callerMemberName);
            logEvent.Properties.Add("callerline", callerLineNumber);

            WriteLog(logEvent);
        }

        /// <summary>
        /// Writes the log
        /// </summary>
        /// <param name="log">The log data</param>
        private static void WriteLog(LogEventInfo log)
        {
            var logger = LogManager.GetLogger("*");
            log.Properties.Add("user", Environment.UserName.Replace(" ", ""));
            logger.Log(log);
        }
    }
}
