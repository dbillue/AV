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

        public void WriteInformation(string message, AddPersonDTO personToReturn = null, PetDTO petToReturn = null)
        {
            _logger.LogInformation("Message:" + message);

            switch(message)
            {
                case "New pet added":
                    _logger.LogInformation("Pet:" +
                                            petToReturn.PetId + "," +
                                            petToReturn.Name + "," +
                                            petToReturn.NickName + "," +
                                            petToReturn.PetTypeId + "," +
                                            petToReturn.PersonId + "," +
                                            petToReturn.CreateDate);
                    break;
                case "New person added":
                    _logger.LogInformation("Person:" +
                                            personToReturn.PersonId + "," +
                                            personToReturn.FirstName + "," +
                                            personToReturn.MIddleName + "," +
                                            personToReturn.LastName + "," +
                                            personToReturn.Age + "," +
                                            personToReturn.City + "," +
                                            personToReturn.DateOfBirth);
                    break;
                default:
                    break;
            }
        }

        public void WriteError(string message, Exception ex)
        {
            _logger.LogInformation("Message:" + message);
            _logger.LogInformation("Exception:" + ex.ToString());
        }
    }
}
