using FamilyAPITestHarness.Interfaces;
using FamilyAPITestHarness.Services;
using System;
using System.Threading.Tasks;
using Serilog;
using System.Text;

namespace FamilyAPITestHarness.Controllers
{
    public class PetController : IPetController
    {
        private readonly IPetService _petService;
        private readonly IHarnessDBService _harnessDbService;
        private DateTime startTime, finishTime;

        Int32 intTestRunCount = 2000;

        // Ctor.
        public PetController(IPetService petService, IHarnessDBService harnessDbService)
        {
            _petService = petService;
            _harnessDbService = harnessDbService;
        }

        // Determines which area to test
        public async Task TestAction()
        {
            switch (Program.action)
            {
                case "AddPet":
                    await AddPet();
                    break;
                case "UpdatePet":
                    await UpdatePet();
                    break;
                case "GetPet":
                    await GetPet();
                    break;
                case "GetPets":
                    await GetPets();
                    break;
                case "GetPetTypes":
                    await GetPetTypes();
                    break;
                case "DeletePet":
                    await DeletePet();
                    break;
                default:
                    break;
            }
        }

        // Test Case: Add Pet
        private async Task AddPet()
        {
            Log.Information("PetController.AddPet()");
            string data = @"{
                    ""name"": ""Mia"",
                    ""nickName"": ""MiaMia"",
                    ""petTypeId"": 3,
                    ""personId"": """ + Program.personId + @""",
                    ""createdate"": """ + DateTime.Now.ToString() + @"""}";

            /*{
                "name": "Mia",
                "nickName": "MiaMia",
                "petTypeId": 3,
                "personId": "FFE36941-51F5-4FC9-9FEC-F5429AD61E11",
                "createdate": "2020-08-22T15:47:25"
            }*/

            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                await _petService.AddPet("AddPet", data);
            }
        }

        //Test Case: Update pet
        private async Task UpdatePet()
        {
            Log.Information("PetController.UpdatePet()");

            #region // Setup patch document
            StringBuilder sb = new StringBuilder();
            sb.Append(@"[{");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/name"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""MountainBelly""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/nickName"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""BellyBaby""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/petTypeId"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" ""1""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/personId"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" """ + Program.personId + @"""");
            sb.Append(@" },{ ");
            sb.Append(@" ""op"": ");
            sb.Append(@" ""replace"", ");
            sb.Append(@" ""path"": ");
            sb.Append(@" ""/createdate"", ");
            sb.Append(@" ""value"": ");
            sb.Append(@" """ + DateTime.Now.ToString() + @"""");
            sb.Append(@"}]");
            #endregion

            //TODO: Add time capture
            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                await _petService.UpdatePet("UpdatePet", Program.petId, sb.ToString());
            }
        }

        // Test Case: Delete Pet
        private async Task DeletePet()
        {
            Log.Information("PetController.DeletePet()");

            var lstPetIds = await _harnessDbService.GetPetIds();

            startTime = DateTime.Now;
            Log.Information("Test time start: " + startTime.ToString());
            foreach (var pet in lstPetIds)
            {
                await _petService.DeletePet("DeletePet", pet.petId.ToString());
            }
            finishTime = DateTime.Now;
            Log.Information("Test time end: " + finishTime.ToString());
        }

        // Test Case: Get Pet
        private async Task GetPet()
        {
            Log.Information("PetController.GetPet()");

            var lstPetIds = await _harnessDbService.GetPetIds();
            startTime = DateTime.Now;
            foreach (var pet in lstPetIds)
            {
                var returnPet = await _petService.GetPet("GetPet", pet.petId.ToString());
                Console.WriteLine(returnPet);
            }
            finishTime = DateTime.Now;
            Log.Information("Test time end: " + finishTime.ToString());
        }

        // Test Case: Get pets
        private async Task GetPets()
        {
            Log.Information("PetsController.GetPets()");

            startTime = DateTime.Now;
            Log.Information("Test time start: " + startTime.ToString());
            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                var _pets = await _petService.GetPets("GetPets");
                Console.WriteLine(_pets);
            }
            finishTime = DateTime.Now;
            Log.Information("Test time end: " + finishTime.ToString());
        }

        // Test Case: Get PetTypes
        private async Task GetPetTypes()
        {
            Log.Information("PetController.GetPetTypes()");

            startTime = DateTime.Now;
            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                var returnPetTypes = await _petService.GetPetTypes("GetPetTypes");
                Console.WriteLine(returnPetTypes);
            }
            finishTime = DateTime.Now;
            Log.Information("Test time end: " + finishTime.ToString());
        }
    }
}
