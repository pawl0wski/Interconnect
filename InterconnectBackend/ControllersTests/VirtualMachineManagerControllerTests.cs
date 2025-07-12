using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
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
            var response = _controller.InitializeConnection(new InitializeConnectionRequest { ConnectionUrl = null });
            var baseResponse = ((OkObjectResult)response.Result).Value as BaseResponse<object>;

            Assert.That(baseResponse?.Success, Is.True);
            Assert.That(baseResponse?.ErrorMessage, Is.Null);
            _mockManagerService.Verify(s => s.InitializeConnection(null), Times.Once());
        }
    }
}
