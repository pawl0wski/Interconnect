using Hubs;
using Models;
using Models.Requests;
using Moq;
using Services;

namespace HubsTests
{
    public class VirtualMachineConsoleHubTests
    {
        private Mock<IVirtualMachineConsoleService> _service;
        private VirtualMachineConsoleHub _hub;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IVirtualMachineConsoleService>();
            _hub = new VirtualMachineConsoleHub(_service.Object);
        }

        [Test]
        public void GetInitialDataForConsole_WhenInvoked_ShouldGetInitialDataForConsole()
        {
            var testUuid = "9f27319c-86f2-4657-ba57-a29b8e14c443";
            var testBytes = new byte[] { 123, 233 };
            var testBase64 = Convert.ToBase64String(testBytes);
            _service.Setup(s => s.GetInitialConsoleData(It.IsAny<Guid>())).Returns([123, 233]);

            var resp = _hub.GetInitialDataForConsole(testUuid);

            _service.Verify(s => s.GetInitialConsoleData(It.Is<Guid>(v => v == Guid.Parse(testUuid))), Times.Once);
            Assert.That(resp.Data?.Data, Is.EqualTo(testBase64));
            Assert.That(resp.Data?.Uuid, Is.EqualTo(testUuid));
        }

        [Test]
        public void SendDataToConsole_WhenInvoked_ShouldSendDataToConsole()
        {
            var testUuid = "9f27319c-86f2-4657-ba57-a29b8e14c443";
            var testData = "123";
            _service.Setup(s => s.SendBytesToVirtualMachineConsoleByUuid(It.IsAny<Guid>(), It.IsAny<string>()));

            _hub.SendDataToConsole(new SendDataToConsoleRequest
            {
                Uuid = testUuid,
                Data = testData
            });

            _service.Verify(s => s.SendBytesToVirtualMachineConsoleByUuid(It.Is<Guid>(v => v == Guid.Parse(testUuid)), It.Is<string>(v => v == testData)), Times.Once);
        }
    }
}
