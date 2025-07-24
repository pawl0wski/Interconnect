using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using Moq;
using NativeLibrary.Structs;
using NativeLibrary.Utils;
using NativeLibrary.Wrappers;
using Services.Impl;

namespace ServicesTests
{
    public class VirtualMachineManagerServiceTests
    {
        private Mock<IVirtualMachineManagerWrapper> _managerWrapper;
        private IOptions<InterconnectConfig> _interconnectConfig;

        [SetUp]
        public void SetUp()
        {
            _managerWrapper = new Mock<IVirtualMachineManagerWrapper>();
            _interconnectConfig = Options.Create(new InterconnectConfig { VmPrefix = "test", HypervisorUrl = "test:///test" });
        }

        [Test]
        public void CreateVirtualMachine_WhenInvoked_ShouldCreateVirtualMachineUsingNativeLibrary()
        {
            _managerWrapper.Setup(m => m.CreateVirtualMachine(It.IsAny<string>()));
            _managerWrapper.Setup(m => m.GetVirtualMachineInfo(It.IsAny<string>())).Returns(new NativeVirtualMachineInfo { Uuid = "testUuid" });
            var manager = new VirtualMachineManagerService(_managerWrapper.Object, _interconnectConfig);

            var info = manager.CreateVirtualMachine(new VirtualMachineCreateDefinition
            {
                VirtualCpus = 1,
                Memory = 123,
                Name = "test",
                BootableDiskPath = "/alpine.iso"
            });

            _managerWrapper.Verify(w => w.CreateVirtualMachine(It.IsAny<string>()), Times.Once());
            _managerWrapper.Verify(w => w.GetVirtualMachineInfo("test_test"));
            Assert.That(info.Uuid, Is.EqualTo("testUuid"));
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
            var manager = new VirtualMachineManagerService(_managerWrapper.Object, _interconnectConfig);

            var vms = manager.GetListOfVirtualMachines();

            Assert.That(vms.Count, Is.EqualTo(2));
        }
    }
}
