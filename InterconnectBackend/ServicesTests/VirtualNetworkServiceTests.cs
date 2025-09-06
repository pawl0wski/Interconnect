using Models.Database;
using Models.DTO;
using Moq;
using NativeLibrary.Wrappers;
using Repositories;
using Services;
using Services.Impl;

namespace ServicesTests
{
    public class VirtualNetworkServiceTests
    {
        private Mock<IVirtualizationWrapper> _virtualizationWrapper;
        private Mock<IVirtualMachineEntityService> _vmEntityService;
        private Mock<IVirtualNetworkEntityConnectionRepository> _connectionRepository;
        private VirtualNetworkService _vnService;

        [SetUp]
        public void SetUp()
        {
            _virtualizationWrapper = new Mock<IVirtualizationWrapper>();
            _vmEntityService = new Mock<IVirtualMachineEntityService>();
            _connectionRepository = new Mock<IVirtualNetworkEntityConnectionRepository>();
            _vnService = new VirtualNetworkService(_virtualizationWrapper.Object, _vmEntityService.Object, _connectionRepository.Object);
        }

        [Test]
        public async Task GetVirtualNetworkConnections_WhenInvoked_ShouldGetVirtualNetworkConnections()
        {
            var sourceEntityUuid = Guid.Parse("b8ea0706-679c-405f-83c7-3e2da0cfe283");
            var destinationEntityUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            _connectionRepository.Setup(r => r.GetAll()).ReturnsAsync([new VirtualNetworkEntityConnectionModel{
                Id = 1,
                BridgeName = "virbr1",
                FirstEntityUuid = sourceEntityUuid,
                SecondEntityUuid = destinationEntityUuid
            }]);

            var connections = await _vnService.GetVirtualNetworkConnections();

            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(connections[0].FirstEntityUuid, Is.EqualTo(sourceEntityUuid));
            Assert.That(connections[0].SecondEntityUuid, Is.EqualTo(destinationEntityUuid));
        }

        [Test]
        public void ConnectTwoVirtualMachines_WhenInvokedWithEntitiesWithoutVm_ShouldThrowException()
        {
            _vmEntityService.Setup(s => s.GetEntityById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityDTO
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
            });

            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _vnService.ConnectTwoVirtualMachines(1, 1, 1, 1);
            }, "Source or destination entity vmuuid is null");
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldCreateVirtualNetwork()
        {
            var vmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            _vmEntityService.Setup(s => s.GetEntityById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityDTO
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = vmUuid
            });

            await _vnService.ConnectTwoVirtualMachines(1, 1, 1, 1);

            _virtualizationWrapper.Verify(vw => vw.CreateVirtualNetwork(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldAttachNetworkInterfacesToVMs()
        {
            var sourceVmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            var destinationVmUuid = Guid.Parse("8343aeaa-6da4-46da-8118-d4edb5b39c49");
            _vmEntityService.Setup(s => s.GetEntityById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityDTO
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = entityId == 1 ? sourceVmUuid : destinationVmUuid
            });

            await _vnService.ConnectTwoVirtualMachines(1, 1, 2, 1);

            _virtualizationWrapper.Verify(vw => vw.AttachDeviceToVirtualMachine(It.Is<Guid>(uuid => uuid == sourceVmUuid), It.IsAny<string>()), Times.Once());
            _virtualizationWrapper.Verify(vw => vw.AttachDeviceToVirtualMachine(It.Is<Guid>(uuid => uuid == destinationVmUuid), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldAddVirtualNetworkToDb()
        {
            var sourceVmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            var destinationVmUuid = Guid.Parse("8343aeaa-6da4-46da-8118-d4edb5b39c49");
            _vmEntityService.Setup(s => s.GetEntityById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityDTO
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = entityId == 1 ? sourceVmUuid : destinationVmUuid
            });

            await _vnService.ConnectTwoVirtualMachines(1, 1, 2, 1);

            _connectionRepository.Verify(r => r.CreateNew(
                It.IsAny<string>(),
                It.Is<Guid>(uuid => uuid == sourceVmUuid),
                It.Is<Guid>(uuid => uuid == destinationVmUuid)
                ), Times.Once());
        }
    }
}
