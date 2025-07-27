using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Services.Impl;
using TestUtils;

namespace ServicesTests
{
    public class VirtualMachineEntityServiceTests
    {
        private InterconnectDbContext _context;
        private VirtualMachineEntityService _service;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _service = new VirtualMachineEntityService(_context);
        }

        [Test]
        public async Task CreateEntity_WhenInvoked_ShouldReturnEntity()
        {
            var response = await _service.CreateEntity("Test", 12, 54);

            Assert.IsNotNull(response);
            Assert.That(response.Name, Is.EqualTo("Test"));
            Assert.That(response.X, Is.EqualTo(12));
            Assert.That(response.Y, Is.EqualTo(54));
            Assert.That(response.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateEntity_WhenInvoked_ShouldCreateEntityInDb()
        {
            await _service.CreateEntity("Test1", 32, 12);

            var entity = await _context.VirtualMachineEntityModels.ToListAsync();

            Assert.That(entity.Count(), Is.EqualTo(1));
            Assert.That(entity[0].Id, Is.EqualTo(1));
            Assert.That(entity[0].Name, Is.EqualTo("Test1"));
            Assert.That(entity[0].X, Is.EqualTo(32));
            Assert.That(entity[0].Y, Is.EqualTo(12));
            Assert.That(entity[0].VmUuid, Is.Null);
        }

        [Test]
        public async Task GetEntities_WhenEntitiesExist_ShouldReturnListOfEntities()
        {
            _context.VirtualMachineEntityModels.Add(new VirtualMachineEntityModel
            {
                Id = 0,
                Name = "TestEntity1",
                X = 10,
                Y = 43,
                VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
            });
            _context.VirtualMachineEntityModels.Add(new VirtualMachineEntityModel
            {
                Id = 0,
                Name = "TestEntity2",
                X = 120,
                Y = 43,
                VmUuid = Guid.Parse("68013152-09EE-4837-998D-E7C3FEE3BB41"),
            });
            await _context.SaveChangesAsync();

            var entities = await _service.GetEntities();

            Assert.That(entities.Count(), Is.EqualTo(2));
            Assert.That(entities[0].Id, Is.EqualTo(1));
            Assert.That(entities[0].Name, Is.EqualTo("TestEntity1"));
            Assert.That(entities[0].X, Is.EqualTo(10));
            Assert.That(entities[0].Y, Is.EqualTo(43));
            Assert.That(entities[0].VmUuid, Is.EqualTo(Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41")));
            Assert.That(entities[1].Id, Is.EqualTo(2));
            Assert.That(entities[1].Name, Is.EqualTo("TestEntity2"));
            Assert.That(entities[1].X, Is.EqualTo(120));
            Assert.That(entities[1].Y, Is.EqualTo(43));
            Assert.That(entities[1].VmUuid, Is.EqualTo(Guid.Parse("68013152-09EE-4837-998D-E7C3FEE3BB41")));
        }

        [Test]
        public async Task GetEntityById_WhenEntityExist_ShouldReturnEntity()
        {
            _context.VirtualMachineEntityModels.Add(new VirtualMachineEntityModel
            {
                Id = 0,
                Name = "TestEntity1",
                X = 10,
                Y = 43,
                VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
            });
            await _context.SaveChangesAsync();

            var entity = await _service.GetEntityById(1);

            Assert.That(entity.Id, Is.EqualTo(1));
            Assert.That(entity.Name, Is.EqualTo("TestEntity1"));
            Assert.That(entity.X, Is.EqualTo(10));
            Assert.That(entity.Y, Is.EqualTo(43));
            Assert.That(entity.VmUuid, Is.EqualTo(Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41")));
        }

        [Test]
        public void GetEntityById_WhenEntityNotExist_ShouldThrowError()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.GetEntityById(1));
        }

        [Test]
        public async Task UpdateEntityPosition_WhenEntityExist_UpdatesPosition()
        {
            _context.VirtualMachineEntityModels.Add(new VirtualMachineEntityModel
            {
                Id = 0,
                Name = "TestEntity1",
                X = 10,
                Y = 43,
                VmUuid = Guid.Parse("68013153-09EE-4837-998D-E7C3FEE3BB41"),
            });
            await _context.SaveChangesAsync();

            await _service.UpdateEntityPosition(1, 100, 500);

            var updatedEntity = await _context.VirtualMachineEntityModels.SingleAsync();
            Assert.That(updatedEntity.X, Is.EqualTo(100));
            Assert.That(updatedEntity.Y, Is.EqualTo(500));
        }

        [Test]
        public void UpdateEntityPosition_WhenEntityNotExist_ShouldThrowException()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.UpdateEntityPosition(1, 100, 500));
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
    }
}
