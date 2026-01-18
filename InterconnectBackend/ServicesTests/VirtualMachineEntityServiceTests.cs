using Models;
using Models.Database;
using Models.Enums;
using Moq;
using Repositories;
using Services;
using Services.Impl;

namespace ServicesTests
{
    public class VirtualMachineEntityServiceTests
    {
        private Mock<IVirtualMachineEntityRepository> _repository;
        private Mock<IVirtualMachineManagerService> _managerService;
        private Mock<IBootableDiskProviderService> _bootableDiskProviderService;
        private VirtualMachineEntityService _service;


        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IVirtualMachineEntityRepository>();
            _managerService = new Mock<IVirtualMachineManagerService>();
            _bootableDiskProviderService = new Mock<IBootableDiskProviderService>();
            _service = new VirtualMachineEntityService(_repository.Object, _bootableDiskProviderService.Object, _managerService.Object);
        }

        [Test]
        public async Task CreateEntity_WhenInvoked_ShouldReturnEntity()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((int id) => new VirtualMachineEntityModel
            {
                Id = id,
                Name = "Test",
                X = 123,
                Y = 321,
            });
            MockVirtualMachineManagerCreateVirtualMachine();
            _bootableDiskProviderService.Setup(s => s.GetBootableDiskPathById(It.IsAny<int>())).ReturnsAsync("/tmp/test.iso");
            var response = await _service.CreateEntity("Test", 1, 1024, 2, VirtualMachineEntityType.Host, 12, 54);

            Assert.IsNotNull(response);
            Assert.That(response.Name, Is.EqualTo("Test"));
            Assert.That(response.X, Is.EqualTo(12));
            Assert.That(response.Y, Is.EqualTo(54));
            Assert.That(response.Id, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateEntity_WhenInvoked_ShouldCreateEntityInRepository()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((int id) => new VirtualMachineEntityModel
            {
                Id = id,
                Name = "Test1",
                X = 123,
                Y = 321,
            });
            MockVirtualMachineManagerCreateVirtualMachine();
            _bootableDiskProviderService.Setup(s => s.GetBootableDiskPathById(It.IsAny<int>())).ReturnsAsync("/tmp/test.iso");

            await _service.CreateEntity("Test1", 1, 1024, 2, VirtualMachineEntityType.Host, 32, 12);

            _repository.Verify(r => r.Add(It.Is<VirtualMachineEntityModel>(vm =>
                vm.Id == 0 &&
                vm.Name == "Test1" &&
                vm.X == 32 &&
                vm.Y == 12
            )), Times.Once);
        }

        [Test]
        public async Task GetEntities_WhenEntitiesExist_ShouldReturnListOfEntities()
        {
            _repository.Setup(r => r.GetAll()).ReturnsAsync([
                new VirtualMachineEntityModel
                    {
                        Id = 0,
                        Name = "TestEntity1",
                        X = 10,
                        Y = 43,
                        VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
                    },
                new VirtualMachineEntityModel
                    {
                        Id = 0,
                        Name = "TestEntity2",
                        X = 120,
                        Y = 43,
                        VmUuid = Guid.Parse("68013152-09EE-4837-998D-E7C3FEE3BB41"),
                    }
            ]);
            _managerService.Setup(s => s.GetListOfVirtualMachines()).Returns([
                new VirtualMachineInfo {
                    Name = "Test",
                    Uuid = "68013152-09EE-4837-998D-E7C3FEE3BB41",
                    State = 1,
                    UsedMemory = 1024,
                }]);

            var entities = await _service.GetEntities();

            Assert.That(entities.Count(), Is.EqualTo(2));
            Assert.That(entities[0].Id, Is.EqualTo(0));
            Assert.That(entities[0].Name, Is.EqualTo("TestEntity1"));
            Assert.That(entities[0].X, Is.EqualTo(10));
            Assert.That(entities[0].Y, Is.EqualTo(43));
            Assert.That(entities[0].VmUuid, Is.EqualTo(Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41")));
            Assert.That(entities[1].Id, Is.EqualTo(0));
            Assert.That(entities[1].Name, Is.EqualTo("TestEntity2"));
            Assert.That(entities[1].X, Is.EqualTo(120));
            Assert.That(entities[1].Y, Is.EqualTo(43));
            Assert.That(entities[1].VmUuid, Is.EqualTo(Guid.Parse("68013152-09EE-4837-998D-E7C3FEE3BB41")));
        }

        [Test]
        public async Task GetEntityById_WhenEntityExist_ShouldReturnEntity()
        {
            _repository.Setup(r => r.GetById(It.Is<int>(v => v == 1))).ReturnsAsync(
                new VirtualMachineEntityModel
                {
                    Id = 0,
                    Name = "TestEntity1",
                    X = 10,
                    Y = 43,
                    VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
                });

            var entity = await _service.GetById(1);

            Assert.That(entity.Id, Is.EqualTo(0));
            Assert.That(entity.Name, Is.EqualTo("TestEntity1"));
            Assert.That(entity.X, Is.EqualTo(10));
            Assert.That(entity.Y, Is.EqualTo(43));
            Assert.That(entity.VmUuid, Is.EqualTo(Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41")));
        }

        [Test]
        public async Task UpdateEntityPosition_WhenEntityExist_UpdatesPosition()
        {
            _repository.Setup(r => r.GetById(It.Is<int>(v => v == 1))).ReturnsAsync(
                new VirtualMachineEntityModel
                {
                    Id = 0,
                    Name = "TestEntity1",
                    X = 10,
                    Y = 43,
                    VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
                });
            _repository.Setup(r => r.Update(It.IsAny<VirtualMachineEntityModel>()));

            await _service.UpdateEntityPosition(1, 100, 500);

            _repository.Verify(r => r.Update(It.Is<VirtualMachineEntityModel>(v => v.X == 100 && v.Y == 500)), Times.Once);
        }

        [Test]
        public async Task UpdateEntityVmUUID_WhenInvoked_ShouldUpdateEntityVmUUID()
        {
            var testGuid = Guid.NewGuid();
            _repository.Setup(r => r.GetById(It.Is<int>(v => v == 1))).ReturnsAsync(
               new VirtualMachineEntityModel
               {
                   Id = 0,
                   Name = "TestEntity1",
                   X = 10,
                   Y = 43,
                   VmUuid = null,
               });
            _repository.Setup(r => r.Update(It.IsAny<VirtualMachineEntityModel>()));

            await _service.UpdateEntityVmUUID(1, testGuid.ToString());

            _repository.Verify(r => r.Update(It.Is<VirtualMachineEntityModel>(v => v.VmUuid == testGuid)), Times.Once);
        }

        private void MockVirtualMachineManagerCreateVirtualMachine()
        {
            _managerService.Setup(m => m.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>())).Returns((VirtualMachineCreateDefinition definition) =>
            {
                return new VirtualMachineInfo
                {
                    Name = definition.Name,
                    State = 1,
                    UsedMemory = definition.Memory,
                    Uuid = Guid.NewGuid().ToString(),
                };
            });
        }
    }
}
