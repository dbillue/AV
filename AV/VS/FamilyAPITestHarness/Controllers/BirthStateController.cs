using FamilyAPITestHarness.Interfaces;
using FamilyAPITestHarness.Services;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAPITestHarness.Controllers
{
    public class BirthStateController : IBirthStateController
    {
        IBirthStateService _birthstateService;
        private DateTime startTime, finishTime;
        Int32 intTestRunCount = 2000;

        // CTOR.
        public BirthStateController(IBirthStateService birthstateService)
        {
            _birthstateService = birthstateService;
        }

        public async Task TestAction()
        {
            switch (Program.action)
            {
                case "GetStates":
                    await GetStates();
                    break;
                default:
                    break;
            }
        }

        private async Task GetStates()
        {
            Log.Information("PersonContoller.QueryPersons()");

            startTime = DateTime.Now;
            Log.Information("Test time start: " + startTime.ToString());
            for (Int32 iCnt = 1; iCnt <= intTestRunCount; iCnt++)
            {
                var _states = await _birthstateService.GetStates("GetStates");
                Console.WriteLine(_states);
            }
            finishTime = DateTime.Now;
            Log.Information("Test time end: " + finishTime.ToString());
        }
    }
}
