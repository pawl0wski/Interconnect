using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using Moq;
using Services;

namespace ControllersTests
{
    class VirtualMachineManagerControllerTests
    {
        private Mock<IVirtualMachineManagerService> _mockManagerService; 
        private VirtualMachineManagerController _controller;

        [SetUp]
        public void Setup()
        {
            _mockManagerService = new Mock<IVirtualMachineManagerService>();

            _controller = new VirtualMachineManagerController(_mockManagerService.Object);
        }

        [Test]
        public void InitializeConnection_Invoke_ShouldCallForConnectionInitialize()
        {
            var result = _controller.InitializeConnection();

            Assert.That(result, Is.InstanceOf<OkResult>());
            _mockManagerService.Verify(s => s.InitializeConnection(), Times.Once());
        }
    }
}
