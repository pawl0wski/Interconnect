using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using Models.Requests;
using Models.Responses;
using Moq;
using Services;

namespace ControllersTests
{
    class HypervisorConnectionControllerTests
    {
        private Mock<IHypervisorConnectionService> _hypervisorConnectionService;

        [SetUp]
        public void Setup()
        {
            _hypervisorConnectionService = new Mock<IHypervisorConnectionService>();

        }

        [Test]
        public void InitializeConnection_WhenInvoked_ShouldCallForConnectionInitialize()
        {
            var controller = new HypervisorConnectionController(_hypervisorConnectionService.Object);
            var response = controller.InitializeConnection(new InitializeConnectionRequest { ConnectionUrl = null });
            var baseResponse = ((OkObjectResult)response.Result).Value as BaseResponse<object>;

            Assert.That(baseResponse?.Success, Is.True);
            Assert.That(baseResponse?.ErrorMessage, Is.Null);
            _hypervisorConnectionService.Verify(s => s.InitializeConnection(null), Times.Once());
        }

        [Test]
        public void ConnectionInfo_WhenInvoked_ShouldGetConnectionInfo()
        {
            var controller = new HypervisorConnectionController(_hypervisorConnectionService.Object);
            _hypervisorConnectionService.Setup(m => m.GetConnectionInfo()).Returns(new ConnectionInfo
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

            _hypervisorConnectionService.Verify(m => m.GetConnectionInfo(), Times.Once);
        }

        [Test]
        public void ConnectionStatus_WhenInvoked_ShouldGetConnectionStatus()
        {
            var controller = new HypervisorConnectionController(_hypervisorConnectionService.Object);
            _hypervisorConnectionService.Setup(m => m.GetConnectionStatus()).Returns(ConnectionStatus.ALIVE);

            var response = controller.ConnectionStatus();

            _hypervisorConnectionService.Verify(m => m.GetConnectionStatus());
        }
    }
}
