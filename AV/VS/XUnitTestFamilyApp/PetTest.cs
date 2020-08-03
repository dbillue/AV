using FamilyApp.Service;
using Moq;
using Xunit;
using XUnitTestFamilyApp.DataObjects;

namespace XUnitTestFamilyApp
{
    public class PetTest
    {
        private readonly DataObjs dataObjs = new DataObjs();

        [Fact]
        public async void TestAddPet()
        {
            var pet = dataObjs.GetPet();
            var person = dataObjs.GetPerson();
            var petType = dataObjs.GetPetTypes(PetType.Cat.ToString());
            var petTypeList = dataObjs.GetPetTypeList();

            // Arrange.
            var petService = new Mock<IPetService>();
            petService.Setup(ps => ps.AddNewPet(person, pet, petTypeList, petType.ToString())).ReturnsAsync(true);
            var addPet = petService.Object;

            // Act.
            var actual = await addPet.AddNewPet(person, pet, petTypeList, petType.ToString());

            // Assert.
            Assert.True
                (actual);
        }

        [Fact]
        public async void TestDeletePet()
        {
            var pet = dataObjs.GetPet();
            var person = dataObjs.GetPerson();
            var petType = dataObjs.GetPetTypes(PetType.Cat.ToString());

            // Arrange.
            var petService = new Mock<IPetService>();
            petService.Setup(ps => ps.DeletePet(pet)).ReturnsAsync(true);
            var deletePet = petService.Object;

            // Act.
            var actual = await deletePet.DeletePet(pet);

            // Assert.
            Assert.True
                (actual);
        }
    }

    enum PetType
    {
        Cat = 1,
        Dog = 2,
        Reptile = 3
    }
}
