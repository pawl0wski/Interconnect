using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
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
    }
}
