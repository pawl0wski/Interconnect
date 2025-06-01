using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Responses;
using Moq;
using Services;

namespace ControllersTests
{
    public class InfoControllerTests
    {
        private Mock<IInfoService> _mockInfoService;
        private InfoController _controller;

        [SetUp]
        public void Setup()
        {
            _mockInfoService = new Mock<IInfoService>();
            _mockInfoService.Setup(s => s.GetSystemInfo()).Returns(new InformationModel
            {
                OsDescription = "test",
                OsArch = "xtest",
            });

            _controller = new InfoController(_mockInfoService.Object);
        }

        [Test]
        public void InfoController_GetSystemInfo_ShouldReturnSystemInfo()
        {
            var result = _controller.GetSystemInfo().Result;


            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<SystemInfoResponse>());
            var response = okResult?.Value as SystemInfoResponse;

            Assert.Multiple(() =>
            {
                Assert.That(response?.Os, Is.EqualTo("test"));
                Assert.That(response?.Arch, Is.EqualTo("xtest"));
            });
        }
    }
}