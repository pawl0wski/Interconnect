using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using Moq;
using NativeLibrary.Structs;
using NativeLibrary.Utils;
using NativeLibrary.Wrappers;
using Services;
using Services.Impl;
using TestUtils;

namespace ServicesTests
{
    public class VirtualMachineManagerServiceTests
    {
        private Mock<IVirtualizationWrapper> _managerWrapper;
        private Mock<IVirtualMachineConsoleService> _consoleService;
        private IOptions<InterconnectConfig> _interconnectConfig;
        private VirtualMachineManagerService _managerService;

        [SetUp]
        public void SetUp()
        {
            _managerWrapper = new Mock<IVirtualizationWrapper>();
            _consoleService = new Mock<IVirtualMachineConsoleService>();
            _interconnectConfig = TestMocks.GetMockConfig();
            _managerService = new VirtualMachineManagerService(_managerWrapper.Object, _interconnectConfig, _consoleService.Object);
        }

        [Test]
        public void CreateVirtualMachine_WhenInvoked_ShouldCreateVirtualMachineUsingNativeLibrary()
        {
            _managerWrapper.Setup(m => m.CreateVirtualMachine(It.IsAny<string>()));
            _managerWrapper.Setup(m => m.GetVirtualMachineInfo(It.IsAny<string>())).Returns(
                new NativeVirtualMachineInfo { Uuid = "fe18228a-d7b0-4f1f-b17a-85d839e4d023" });

            var info = _managerService.CreateVirtualMachine(new VirtualMachineCreateDefinition
            {
                VirtualCpus = 1,
                Memory = 123,
                Name = "test",
                BootableDiskPath = "/alpine.iso"
            });

            _managerWrapper.Verify(w => w.CreateVirtualMachine(It.IsAny<string>()), Times.Once());
            _managerWrapper.Verify(w => w.GetVirtualMachineInfo("interconnect_test"));
            Assert.That(info.Uuid, Is.EqualTo("fe18228a-d7b0-4f1f-b17a-85d839e4d023"));
        }

        [Test]
        public void CreateVirtualMachine_AfterVirtualMachineCreated_ShouldOpenVirtualMachineConsole()
        {
            _consoleService.Setup(s => s.OpenVirtualMachineConsole(It.IsAny<Guid>()));
            _managerWrapper.Setup(m => m.GetVirtualMachineInfo(It.IsAny<string>())).Returns(
                new NativeVirtualMachineInfo { Uuid = "fe18228a-d7b0-4f1f-b17a-85d839e4d023" });

            _managerService.CreateVirtualMachine(new VirtualMachineCreateDefinition
            {
                VirtualCpus = 1,
                Memory = 123,
                Name = "test",
                BootableDiskPath = "/alpine.iso"
            });

            _consoleService.Verify(s => s.OpenVirtualMachineConsole(It.Is<Guid>(v => v.Equals(Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023")))), Times.Once);
        }

        [Test]
        public void GetListOfVirtualMachines_WhenInvoked_ShouldGetListOfVirtualMachinesUsingNativeLibrary()
        {
            var listWithMockVirtualMachines = new List<NativeVirtualMachineInfo>{
                new NativeVirtualMachineInfo {
                        Name = "test",
                        Uuid = "123",
                        State = 1,
                        UsedMemory = 123,
                },
                new NativeVirtualMachineInfo {
                        Name = "tes2",
                        Uuid = "321",
                        State = 1,
                        UsedMemory = 123,
                },
            };
            var mockedNativeList = new Mock<INativeList<NativeVirtualMachineInfo>>();
            mockedNativeList.Setup(m => m.GetEnumerator()).Returns(listWithMockVirtualMachines.GetEnumerator());
            _managerWrapper.Setup(m => m.GetListOfVirtualMachines()).Returns(mockedNativeList.Object);

            var vms = _managerService.GetListOfVirtualMachines();

            Assert.That(vms.Count, Is.EqualTo(2));
        }
    }
}
