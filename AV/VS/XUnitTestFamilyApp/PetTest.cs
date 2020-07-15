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

            // Arrange.
            var petService = new Mock<IPetService>();
            petService.Setup(ps => ps.AddNewPet(person, pet, petType)).ReturnsAsync(true);
            var addPet = petService.Object;

            // Act.
            var actual = await addPet.AddNewPet(person, pet, petType);

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

        [Fact]
        public void TestGetPetType()
        {
            var pet = dataObjs.GetPet();
            var person = dataObjs.GetPerson();
            var petType = dataObjs.GetPetTypes(PetType.Cat.ToString());

            // Arrange.
            var petService = new Mock<IPetService>();
            petService.Setup(ps => ps.GetPetType(pet, petType)).Returns(1);
            var ps = petService.Object;

            // Act.
            var actual = ps.GetPetType(pet, petType);

            // Assert.
            Assert.Equal
                (1, actual);
        }
    }

    enum PetType
    {
        Cat = 1,
        Dog = 2,
        Reptile = 3
    }
}
