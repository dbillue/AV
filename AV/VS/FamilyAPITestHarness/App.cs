using FamilyAPITestHarness.Interfaces;
using System.Threading.Tasks;

namespace FamilyAPITestHarness
{
    public class App
    {
        private readonly IPersonController _personController;
        private readonly IPetController _petController;

        //Ctor.
        public App(IPersonController personController, IPetController petController)
        {
            _personController = personController;
            _petController = petController;
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
                default:
                    break;
            }
        }
    }
}
