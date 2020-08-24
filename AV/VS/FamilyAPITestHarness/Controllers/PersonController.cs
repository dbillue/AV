using FamilyAPITestHarness.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

namespace FamilyAPITestHarness.Controllers
{
    public class PersonController
    {
        IConfigurationRoot _configurationRoot;
        PersonService personService;
        string testAction = string.Empty;
        Int32 APITestCallCnt = 500;

        // CTOR.
        public PersonController(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            personService = new PersonService(_configurationRoot);
            testAction = _configurationRoot.GetSection("PersonTestArea").GetSection("TestAction").Value;
        }

        // Determines which area to test
        public async Task TestEntry()
        {
            switch (testAction)
            {
                case "AddPerson":
                    await AddPerson();
                    break;
                default:
                    break;
            }
        }

        public async Task AddPerson()
        {
            for (Int32 iCnt = 1; iCnt <= APITestCallCnt; iCnt++)
            {
                await personService.AddPerson("Persons");
            }
        }
    }
}
