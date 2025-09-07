using Controllers;
using Models.DTO;
using Models.Requests;
using Models.Responses;
using Moq;
using Services;
using TestUtils;

namespace ControllersTests
{
    public class VirtualMachineEntityControlerTests
    {
        private Mock<IVirtualMachineEntityService> _mockService;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IVirtualMachineEntityService>();
        }

        [Test]
        public async Task UpdateVirtualMachineEntityPosition_WhenInvoked_ShouldUpdatePosition()
        {
            _mockService.Setup(s => s.UpdateEntityPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            var controller = new VirtualMachineEntityController(_mockService.Object);

            await controller.UpdateVirtualMachineEntityPosition(new UpdateEntityPositionRequest
            {
                Id = 1,
                X = 43,
                Y = 21,
            });

            _mockService.Verify(s => s.UpdateEntityPosition(1, 43, 21), Times.Once());
        }

        [Test]
        public async Task UpdateVirtualMachineEntityPosition_WhenInvoked_ShouldReturnUpdatedEntity()
        {
            _mockService.Setup(s => s.UpdateEntityPosition(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new VirtualMachineEntityDTO
            {
                Id = 1,
                Name = "Test",
                X = 10,
                Y = 50
            });
            var controller = new VirtualMachineEntityController(_mockService.Object);

            var response = await controller.UpdateVirtualMachineEntityPosition(new UpdateEntityPositionRequest
            {
                Id = 1,
                X = 10,
                Y = 50,
            });
            var extractedData = ResponseExtractor.ExtractControllerResponse<VirtualMachineEntityResponse, VirtualMachineEntityDTO>(response)?.Data;

            Assert.IsNotNull(extractedData);
            Assert.That(extractedData?.Id, Is.EqualTo(1));
            Assert.That(extractedData?.Name, Is.EqualTo("Test"));
            Assert.That(extractedData?.X, Is.EqualTo(10));
            Assert.That(extractedData?.Y, Is.EqualTo(50));
        }

        [Test]
        public async Task GetVirtualMachineEntities_WhenInvoked_ShouldReturnListOfEntities()
        {
            _mockService.Setup(s => s.GetEntities())
                .ReturnsAsync([
                    new VirtualMachineEntityDTO
                    {
                        Id = 1,
                        Name = "Test",
                        X = 10,
                        Y = 50,
                    },
                    new VirtualMachineEntityDTO
                    {
                        Id = 2,
                        Name = "Test2",
                        X = 20,
                        Y = 423,
                    }
                ]);
            var controller = new VirtualMachineEntityController(_mockService.Object);

            var response = await controller.GetVirtualMachineEntities();
            var extractedData = ResponseExtractor.ExtractControllerResponse<VirtualMachinesEntitiesResponse, List<VirtualMachineEntityDTO>>(response)?.Data;

            Assert.IsNotNull(extractedData);
            Assert.That(extractedData.Count(), Is.EqualTo(2));
            Assert.That(extractedData[0]?.Id, Is.EqualTo(1));
            Assert.That(extractedData[0]?.Name, Is.EqualTo("Test"));
            Assert.That(extractedData[0]?.X, Is.EqualTo(10));
            Assert.That(extractedData[0]?.Y, Is.EqualTo(50));
            Assert.That(extractedData[1]?.Id, Is.EqualTo(2));
            Assert.That(extractedData[1]?.Name, Is.EqualTo("Test2"));
            Assert.That(extractedData[1]?.X, Is.EqualTo(20));
            Assert.That(extractedData[1]?.Y, Is.EqualTo(423));
        }
    }
}
