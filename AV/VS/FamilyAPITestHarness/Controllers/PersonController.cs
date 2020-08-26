using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Serilog;
using System.Text;
using FamilyAPITestHarness.Services;

namespace FamilyAPITestHarness.Controllers
{
    public class PersonController : IPersonController
    {
        private readonly IPersonService _personService;
        private readonly IHarnessDBService _harnessDbService;

        Int32 intTestRunCount = 100;

        // CTOR.
        public PersonController(IPersonService personService, IHarnessDBService harnessDbService)
        {
            _personService = personService;
            _harnessDbService = harnessDbService;
        }

        // Determines which area to test
        public async Task TestAction()
        {
            switch (Program.action)
            {
                case "AddPerson":
                    await AddPerson();
                    break;
                case "UpdatePerson":
                    await UpdatePerson();
                    break;
                case "DeletePerson":
                    DeletePerson();
                    break;
                default:
                    break;
            }
        }

        // Test Case: Add Person
        public async Task AddPerson()
        {
            Log.Information("PersonContoller.AddPerson()");
            string data = @"{
                    ""age"": 25,
                    ""city"": ""Atlanta"",
                    ""country"": ""USA"",
                    ""dateOfBirth"": ""1975 -01-11"",
                    ""firstName"": ""April"",
                    ""gender"": ""Female"",
                    ""lastName"": ""Showers"",
                    ""mIddleName"": ""May"",
                    ""stateId"": 51,
                    ""createdate"": """ + DateTime.Now.ToString() + @"""}";

            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                await _personService.AddPerson("AddPerson", data);
            }
        }

        // Test Case: Update Person
        public async Task UpdatePerson()
        {
            Log.Information("PersonContoller.UpdatePerson()");

            #region // Setup patch document
            StringBuilder sb = new StringBuilder();
            sb.Append(@"[{");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/FirstName"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""First_Updated""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/MIddleName"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""Middle_Updated""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/LastName"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""LastName_Updated""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/Gender"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""Female""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/Age"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""120""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/Country"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""USA""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/City"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""Colorado Springs""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/StateId"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""51""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/DateOfBirth"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""1983-01-11 00:00:00.000""");
            sb.Append(@"}]");
            #endregion

            //TODO: Add time capture
            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                await _personService.UpdatePerson("UpdatePerson", Program.personId, sb.ToString());
            }
        }

        // Test Case: Delete Persons
        public void DeletePerson()
        {
            Log.Information("PersonContoller.DeletePerson()");

            var lstPersons = _harnessDbService.GetPersons();

            foreach (var person in lstPersons)
            {
                _personService.DeletePerson("DeletePerson", person.PersonId.ToString());
            }
        }
    }
}
