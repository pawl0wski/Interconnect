using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Models.Enums;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualNetworkConnectionRepositoryTests
    {
        private InterconnectDbContext _context;
        private VirtualNetworkConnectionRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new VirtualNetworkConnectionRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateVirtualNetworkConnectionEntity()
        {
            await _repository.Create(1, EntityType.VirtualMachine, 2, EntityType.VirtualMachine);

            var savedModel = await _context.VirtualNetworkEntityConnectionModels.FirstAsync();

            Assert.That(savedModel.Id, Is.EqualTo(1));
            Assert.That(savedModel.SourceEntityId, Is.EqualTo(1));
            Assert.That(savedModel.DestinationEntityId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllVirtualNetworkConnectionEntities()
        {
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                SourceEntityId = 1,
                SourceEntityType = EntityType.VirtualMachine,
                DestinationEntityId = 2,
                DestinationEntityType = EntityType.VirtualMachine
            });
            await _context.SaveChangesAsync();

            var savedModels = await _repository.GetAll();

            Assert.That(savedModels.Count(), Is.EqualTo(1));
            Assert.That(savedModels[0].SourceEntityId, Is.EqualTo(1));
            Assert.That(savedModels[0].DestinationEntityId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetUsingEntityId_WhenInvokedWithSourceEntity_ShouldGetVirtualNetworkConnectionEntity()
        {
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                SourceEntityId = 1,
                SourceEntityType = EntityType.VirtualMachine,
                DestinationEntityId = 2,
                DestinationEntityType = EntityType.VirtualMachine
            });
            await _context.SaveChangesAsync();

            var savedModel = await _repository.GetUsingEntityId(1, EntityType.VirtualMachine);

            Assert.That(savedModel[0].SourceEntityId, Is.EqualTo(1));
            Assert.That(savedModel[0].DestinationEntityId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetUsingEntityId_WhenInvokedWithDestinationEntity_ShouldGetVirtualNetworkConnectionEntity()
        {
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                SourceEntityId = 1,
                SourceEntityType = EntityType.VirtualMachine,
                DestinationEntityId = 2,
                DestinationEntityType = EntityType.VirtualMachine
            });
            await _context.SaveChangesAsync();

            var savedModel = await _repository.GetUsingEntityId(2, EntityType.VirtualMachine);

            Assert.That(savedModel[0].SourceEntityId, Is.EqualTo(1));
            Assert.That(savedModel[0].DestinationEntityId, Is.EqualTo(2));
        }
    }
}
