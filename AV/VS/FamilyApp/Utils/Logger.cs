using Microsoft.Extensions.Logging;
using System;

namespace FamilyApp.Utils
{
    public class Logger
    {
        private readonly ILogger _logger;

        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteInformation(string message)
        {
            _logger.LogInformation("Message:" + message);
        }

        public void WriteError(string message, Exception ex)
        {
            _logger.LogInformation("Message:" + message);
            _logger.LogInformation("Exception:" + ex.ToString());
        }
    }
}
