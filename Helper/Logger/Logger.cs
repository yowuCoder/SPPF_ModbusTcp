using Serilog.Events;
using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppMCTcp.Helper
{
    internal class Logger
    {
        private static ILogger _logger;

        // Initialize the logger
        public static void Initialize()
        {
            _logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // Log an information message
        public static void LogInformation(string message)
        {
            _logger.Information(message);
        }

        // Log a warning message
        public static void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        // Log an error message
        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        // Log an exception
        public static void LogException(Exception ex)
        {
            _logger.Error(ex, "An error occurred");
        }

        // Set the minimum log level
       
    }
}
