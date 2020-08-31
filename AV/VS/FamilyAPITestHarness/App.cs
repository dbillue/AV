using FamilyAPITestHarness.Interfaces;
using System.Threading.Tasks;

namespace FamilyAPITestHarness
{
    public class App
    {
        private readonly IPersonController _personController;
        private readonly IPetController _petController;
        private readonly IBirthStateController _birthstateController;

        //Ctor.
        public App(IPersonController personController, IPetController petController, IBirthStateController birthstateController)
        {
            _personController = personController;
            _petController = petController;
            _birthstateController = birthstateController;
        }

        // Determine which controller to call based on test entity.
        public async Task TestEntity()
        {
            switch (Program.entity)
            {
                case "Person":
                    await _personController.TestAction();
                    break;
                case "Pet":
                    await _petController.TestAction();
                    break;
                case "BirthState":
                    await _birthstateController.TestAction();
                    break;
                default:
                    break;
            }
        }
    }
}
