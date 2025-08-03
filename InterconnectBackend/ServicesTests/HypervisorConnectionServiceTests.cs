using Models.Enums;
using Moq;
using NativeLibrary.Structs;
using NativeLibrary.Wrappers;
using Services.Impl;

namespace ServicesTests
{
    public class HypervisorConnectionServiceTests
    {
        private Mock<IVirtualMachineManagerWrapper> _managerWrapper;

        [SetUp]
        public void SetUp()
        {
            _managerWrapper = new Mock<IVirtualMachineManagerWrapper>();
        }

        [Test]
        public void InitializeConnection_WhenInvokedWithoutConnectionUrl_ShouldConnectUsingNativeLibraryWithDefaultConnectionUrl()
        {
            _managerWrapper.Setup(m => m.InitializeConnection("qemu:///session"));
            var hypervisorConnectionService = new HypervisorConnectionService(_managerWrapper.Object);

            hypervisorConnectionService.InitializeConnection(null);

            _managerWrapper.Verify(m => m.InitializeConnection("qemu:///session"), Times.Once());
        }

        [Test]
        public void InitializeConnection_WhenInvoked_ShouldConnectUsingNativeLibrary()
        {
            _managerWrapper.Setup(m => m.InitializeConnection("testUrl"));
            var hypervisorConnectionService = new HypervisorConnectionService(_managerWrapper.Object);

            hypervisorConnectionService.InitializeConnection("testUrl");

            _managerWrapper.Verify(m => m.InitializeConnection("testUrl"), Times.Once());
        }

        [Test]
        public void GetConnectionInfo_WhenInvoked_ShouldGetConnectionInfo()
        {
            _managerWrapper.Setup(m => m.GetConnectionInfo()).Returns(
                new NativeConnectionInfo
                {
                    ConnectionUrl = "mockUrl",
                    CpuCount = 12,
                    CpuFreq = 55,
                    DriverType = "testDriver",
                    DriverVersion = new NativeVersion { Minor = 2, Major = 1, Patch = 3 },
                    LibVersion = new NativeVersion { Minor = 23, Major = 5, Patch = 2 },
                    TotalMemory = 123
                });
            var hypervisorConnectionService = new HypervisorConnectionService(_managerWrapper.Object);

            var info = hypervisorConnectionService.GetConnectionInfo();

            _managerWrapper.Verify(m => m.GetConnectionInfo(), Times.Once());
            Assert.That(info.ConnectionUrl, Is.EqualTo("mockUrl"));
            Assert.That(info.CpuCount, Is.EqualTo(12));
            Assert.That(info.CpuFreq, Is.EqualTo(55));
            Assert.That(info.DriverType, Is.EqualTo("testDriver"));
            Assert.That(info.DriverVersion, Is.EqualTo("2.1.3"));
            Assert.That(info.LibVersion, Is.EqualTo("23.5.2"));
            Assert.That(info.TotalMemory, Is.EqualTo(123));
        }

        [Test]
        public void GetConnectionStatus_WhenConnectionStatusIsAlive_ShouldReturnConnectionIsAlive()
        {
            _managerWrapper.Setup(m => m.IsConnectionAlive()).Returns(true);
            var hypervisorConnectionService = new HypervisorConnectionService(_managerWrapper.Object);

            var status = hypervisorConnectionService.GetConnectionStatus();

            Assert.That(status, Is.EqualTo(ConnectionStatus.ALIVE));
        }

        [Test]
        public void GetConnectionStatus_WhenConnectionStatusIsAlive_ShouldReturnConnectionIsDead()
        {
            _managerWrapper.Setup(m => m.IsConnectionAlive()).Returns(false);
            var hypervisorConnectionService = new HypervisorConnectionService(_managerWrapper.Object);

            var status = hypervisorConnectionService.GetConnectionStatus();

            Assert.That(status, Is.EqualTo(ConnectionStatus.DEAD));
        }
    }
}
