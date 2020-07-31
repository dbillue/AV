using FamilyDemoAPIv2.Models;
using Microsoft.Extensions.Logging;
using System;

namespace FamilyDemoAPIv2.Helpers
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

        public void WriteInformation(string message, AddPersonDTO personToReturn)
        {
            _logger.LogInformation("Message:" + message);
            _logger.LogInformation("Person:" +
                                    personToReturn.PersonId + "," +
                                    personToReturn.FirstName + "," +
                                    personToReturn.MIddleName + "," +
                                    personToReturn.LastName + "," +
                                    personToReturn.Age + "," +
                                    personToReturn.City + "," +
                                    personToReturn.DateOfBirth);
        }

        public void WriteError(string message, Exception ex)
        {
            _logger.LogInformation("Message:" + message);
            _logger.LogInformation("Exception:" + ex.ToString());
        }
    }
}
