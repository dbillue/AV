using FamilyAPITestHarness.Controllers;
using FamilyAPITestHarness.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FamilyAPITestHarness
{
    public class App
    {
        private readonly IPersonController _personController;

        //Ctor.
        public App(IPersonController personController)
        {
            _personController = personController;
        }

        // Determine which controller to call based on test entity.
        public async Task TestEntity()
        {
            switch (Program.entity)
            {
                case "Person":
                    await _personController.TestAction();
                    break;
                case "Pets":
                    //TODO: Create controller to call PetService
                    break;
                default:
                    break;
            }
        }
    }
}
