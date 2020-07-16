using FamilyApp.Service;
using Moq;
using Xunit;

namespace XUnitTestFamilyApp
{
    public class FamilyAPIServiceTest
    {
        [Fact]
        public async void TestGetPeopleRaw()
        {
            // Arrange.
            var familyAPIService = new Mock<IFamilyAPIService>();
            familyAPIService.Setup(fsAPI => fsAPI.GetPeopleRaw()).ReturnsAsync("");
            var famAPIService = familyAPIService.Object;

            // Act.
            var result = await famAPIService.GetPeopleRaw();

            // Assert.
            Assert.True
                (result == string.Empty);
        }
    }
}
