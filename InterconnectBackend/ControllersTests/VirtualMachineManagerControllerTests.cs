using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Requests;
using Models.Responses;
using Moq;
using Services;

namespace ControllersTests
{
    class VirtualMachineManagerControllerTests
    {
        private Mock<IVirtualMachineManagerService> _mockManagerService;

        [SetUp]
        public void Setup()
        {
            _mockManagerService = new Mock<IVirtualMachineManagerService>();

        }

        [Test]
        public void InitializeConnection_Invoke_ShouldCallForConnectionInitialize()
        {
            var controller = new VirtualMachineManagerController(_mockManagerService.Object);
            var response = controller.InitializeConnection(new InitializeConnectionRequest { ConnectionUrl = null });
            var baseResponse = ((OkObjectResult)response.Result).Value as BaseResponse<object>;

            Assert.That(baseResponse?.Success, Is.True);
            Assert.That(baseResponse?.ErrorMessage, Is.Null);
            _mockManagerService.Verify(s => s.InitializeConnection(null), Times.Once());
        }

        [Test]
        public void ConnectionInfo_Invoke_ShouldGetConnectionInfo()
        {
            var controller = new VirtualMachineManagerController(_mockManagerService.Object);
            _mockManagerService.Setup(m => m.GetConnectionInfo()).Returns(new ConnectionInfo
            {
                ConnectionUrl = "testUrl",
                CpuCount = 3,
                CpuFreq = 54,
                DriverType = "testDriver",
                DriverVersion = "1.1.1",
                LibVersion = "5.5.5",
                TotalMemory = 123
            });

            var response = controller.ConnectionInfo();

            _mockManagerService.Verify(m => m.GetConnectionInfo(), Times.Once);
        }

        [Test]
        public void CreateVirtualMachine_Invoke_ShouldCreateVirtualMachine()
        {
            var controller = new VirtualMachineManagerController(_mockManagerService.Object);
            _mockManagerService.Setup(m => m.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>()));

            var response = controller.CreateVirtualMachine(new VirtualMachineCreateDefinition
            {
                BootableDiskPath = "/disk",
                Memory = 12,
                Name = "test",
                VirtualCpus = 1
            });

            _mockManagerService.Verify(m => m.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>()), Times.Once);
        }
    }
}
