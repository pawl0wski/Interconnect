using Models.Database;
using Models.DTO;
using Models.Enums;
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
        private Mock<IVirtualMachineEntityRepository> _vmEntityRepository;
        private Mock<IVirtualNetworkConnectionRepository> _connectionRepository;
        private Mock<IVirtualSwitchEntityRepository> _switchRepository;
        private Mock<IInternetEntityRepository> _internetRepository;
        private Mock<IVirtualNetworkRepository> _networkRepository;
        private VirtualNetworkService _vnService;

        [SetUp]
        public void SetUp()
        {
            _virtualizationWrapper = new Mock<IVirtualizationWrapper>();
            _vmEntityRepository = new Mock<IVirtualMachineEntityRepository>();
            _connectionRepository = new Mock<IVirtualNetworkConnectionRepository>();
            _switchRepository = new Mock<IVirtualSwitchEntityRepository>();
            _internetRepository = new Mock<IInternetEntityRepository>();
            _networkRepository = new Mock<IVirtualNetworkRepository>();
            _vnService = new VirtualNetworkService(
                _virtualizationWrapper.Object,
                _vmEntityRepository.Object,
                _connectionRepository.Object,
                _switchRepository.Object,
                _internetRepository.Object,
                _networkRepository.Object
                );
        }

        [Test]
        public async Task GetVirtualNetworkConnections_WhenInvoked_ShouldGetVirtualNetworkConnections()
        {
            _connectionRepository.Setup(r => r.GetAll()).ReturnsAsync([new VirtualNetworkEntityConnectionModel{
                Id = 1,
                SourceEntityId = 1,
                SourceEntityType = EntityType.VirtualMachine,
                DestinationEntityId = 2,
                DestinationEntityType = EntityType.VirtualMachine
            }]);

            var connections = await _vnService.GetVirtualNetworkConnections();

            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(connections[0].SourceEntityId, Is.EqualTo(1));
            Assert.That(connections[0].DestinationEntityId, Is.EqualTo(2));
        }

        [Test]
        public void ConnectTwoVirtualMachines_WhenInvokedWithEntitiesWithoutVm_ShouldThrowException()
        {
            _vmEntityRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityModel
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
            });

            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _vnService.ConnectTwoVirtualMachines(1, 1);
            }, "Source or destination entity vmuuid is null");
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldCreateVirtualNetwork()
        {
            var vmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            _vmEntityRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityModel
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = vmUuid
            });
            MockConnectionCrepositoryCreateMethod();
            MockSwitchRepositoryCreateInvisibleMethod();

            await _vnService.ConnectTwoVirtualMachines(1, 1);

            _virtualizationWrapper.Verify(vw => vw.CreateVirtualNetwork(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldAttachNetworkInterfacesToVMs()
        {
            var sourceVmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            var destinationVmUuid = Guid.Parse("8343aeaa-6da4-46da-8118-d4edb5b39c49");
            _vmEntityRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityModel
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = entityId == 1 ? sourceVmUuid : destinationVmUuid
            });
            MockConnectionCrepositoryCreateMethod();
            MockSwitchRepositoryCreateInvisibleMethod();

            await _vnService.ConnectTwoVirtualMachines(1, 2);

            _virtualizationWrapper.Verify(vw => vw.AttachDeviceToVirtualMachine(It.Is<Guid>(uuid => uuid == sourceVmUuid), It.IsAny<string>()), Times.Once());
            _virtualizationWrapper.Verify(vw => vw.AttachDeviceToVirtualMachine(It.Is<Guid>(uuid => uuid == destinationVmUuid), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task ConnectTwoVirtualMachines_WhenInvoked_ShouldAddVirtualNetworkToDb()
        {
            var sourceVmUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            var destinationVmUuid = Guid.Parse("8343aeaa-6da4-46da-8118-d4edb5b39c49");
            _vmEntityRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync((int entityId) => new VirtualMachineEntityModel
            {
                Id = entityId,
                Name = $"Entity{entityId}",
                X = 12,
                Y = 13,
                VmUuid = entityId == 1 ? sourceVmUuid : destinationVmUuid
            });
            MockConnectionCrepositoryCreateMethod();
            MockSwitchRepositoryCreateInvisibleMethod();

            await _vnService.ConnectTwoVirtualMachines(1, 2);

            _connectionRepository.Verify(r => r.Create(
                It.Is<int>(id => id == 1),
                It.Is<EntityType>(et => et == EntityType.VirtualMachine),
                It.Is<int>(id => id == 2),
                It.Is<EntityType>(et => et == EntityType.VirtualMachine)
                ), Times.Once());
        }

        [TestCase(null)]
        [TestCase("test")]
        public async Task CreateVirtualSwitch_WhenInvoked_ShouldCreateVirtualNetwork(string? name)
        {
            MockSwitchRepositoryCreateMethod();
            MockSwitchRepositoryCreateInvisibleMethod();

            await _vnService.CreateVirtualSwitch(name);

            _virtualizationWrapper.Verify(vw => vw.CreateVirtualNetwork(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task CreateVirtualSwitch_WhenInvokedWithoutName_ShouldCreateInvisibleVirtualSwitch()
        {
            MockSwitchRepositoryCreateInvisibleMethod();

            await _vnService.CreateVirtualSwitch(null);

            _switchRepository.Verify(r => r.CreateInvisible(It.IsAny<VirtualNetworkModel>()), Times.Once);
        }

        [Test]
        public async Task CreateVirtualSwitch_WhenInvokedWithName_ShouldCreateVisibleVirtualSwitch()
        {
            MockSwitchRepositoryCreateMethod();

            await _vnService.CreateVirtualSwitch("testName");

            _switchRepository.Verify(r => r.Create(It.Is<string>(s => s == "testName"), It.IsAny<VirtualNetworkModel>()), Times.Once);
        }

        [Test]
        public async Task ConnectVirtualMachineToVirtualSwitch_WhenInvoked_ShouldConnectVirtualMachineToSwitch()
        {
            var vmUuid = Guid.Parse("2FB1BEC3-7E13-4510-A4E4-A1869A52E02F");
            var networkUuid = Guid.Parse("9482e9c9-2a4c-4e79-922d-b6ec273e26a5");
            _vmEntityRepository.Setup(s => s.GetById(It.Is<int>(id => id == 1))).ReturnsAsync((int id) =>
            {
                return new VirtualMachineEntityModel
                {
                    Id = id,
                    VmUuid = vmUuid,
                    Name = "Test",
                    X = 0,
                    Y = 0,
                };
            });
            _switchRepository.Setup(s => s.GetById(It.Is<int>(id => id == 2))).ReturnsAsync((int id) =>
            {
                return new VirtualSwitchEntityModel
                {
                    Id = id,
                    Name = "Test",
                    VirtualNetwork = new VirtualNetworkModel
                    {
                        BridgeName = "Bridge",
                        Uuid = networkUuid,
                    },
                    Visible = true,
                };
            });
            MockConnectionCrepositoryCreateMethod();
            MockSwitchRepositoryCreateMethod();

            await _vnService.ConnectVirtualMachineToVirtualSwitch(1, 2);

            _virtualizationWrapper.Verify(vw => vw.AttachDeviceToVirtualMachine(It.Is<Guid>(uuid => uuid == vmUuid), It.IsAny<string>()), Times.Once);
        }

        private void MockSwitchRepositoryCreateInvisibleMethod()
        {
            _switchRepository.Setup(r => r.CreateInvisible(It.IsAny<VirtualNetworkModel>())).ReturnsAsync((VirtualNetworkModel networkModel) =>
            {
                return new VirtualSwitchEntityModel
                {
                    Id = 1,
                    VirtualNetwork = networkModel,
                    Visible = false,
                    X = 0,
                    Y = 0,
                };
            });
        }

        private void MockSwitchRepositoryCreateMethod()
        {
            _switchRepository.Setup(r => r.Create(It.IsAny<string>(), It.IsAny<VirtualNetworkModel>())).ReturnsAsync((string name, VirtualNetworkModel networkModel) =>
            {
                return new VirtualSwitchEntityModel
                {
                    Id = 1,
                    Name = name,
                    VirtualNetwork = networkModel,
                    Visible = true,
                    X = 0,
                    Y = 0,
                };
            });
        }

        private void MockConnectionCrepositoryCreateMethod()
        {
            _connectionRepository.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<EntityType>(), It.IsAny<int>(), It.IsAny<EntityType>())).ReturnsAsync(
            (int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType) =>
            {
                return new VirtualNetworkEntityConnectionModel
                {
                    SourceEntityId = sourceEntityId,
                    SourceEntityType = sourceEntityType,
                    DestinationEntityId = destinationEntityId,
                    DestinationEntityType = destinationEntityType
                };
            });
        }
    }
}
