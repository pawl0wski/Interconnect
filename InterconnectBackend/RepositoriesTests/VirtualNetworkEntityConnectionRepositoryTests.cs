using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualNetworkEntityConnectionRepositoryTests
    {
        private InterconnectDbContext _context;
        private VirtualNetworkEntityConnectionRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new VirtualNetworkEntityConnectionRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateVirtualNetworkConnectionEntity()
        {
            var sourceEntityUuid = Guid.Parse("b8ea0706-679c-405f-83c7-3e2da0cfe283");
            var destinationEntityUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            await _repository.Create( sourceEntityUuid, destinationEntityUuid);

            var savedModel = await _context.VirtualNetworkEntityConnectionModels.FirstAsync();

            Assert.That(savedModel.Id, Is.EqualTo(1));
            Assert.That(savedModel.FirstEntityUuid, Is.EqualTo(sourceEntityUuid));
            Assert.That(savedModel.SecondEntityUuid, Is.EqualTo(destinationEntityUuid));
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllVirtualNetworkConnectionEntities()
        {
            var sourceEntityUuid = Guid.Parse("b8ea0706-679c-405f-83c7-3e2da0cfe283");
            var destinationEntityUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                FirstEntityUuid = sourceEntityUuid,
                SecondEntityUuid = destinationEntityUuid
            });
            await _context.SaveChangesAsync();

            var savedModels = await _repository.GetAll();

            Assert.That(savedModels.Count(), Is.EqualTo(1));
            Assert.That(savedModels[0].FirstEntityUuid, Is.EqualTo(sourceEntityUuid));
            Assert.That(savedModels[0].SecondEntityUuid, Is.EqualTo(destinationEntityUuid));
        }

        [Test]
        public async Task GetForEntityUuid_WhenInvokedWithSourceEntity_ShouldGetVirtualNetworkConnectionEntity()
        {
            var sourceEntityUuid = Guid.Parse("b8ea0706-679c-405f-83c7-3e2da0cfe283");
            var destinationEntityUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                FirstEntityUuid = sourceEntityUuid,
                SecondEntityUuid = destinationEntityUuid
            });
            await _context.SaveChangesAsync();

            var savedModel = await _repository.GetForEntityUuid(sourceEntityUuid);

            Assert.That(savedModel.FirstEntityUuid, Is.EqualTo(sourceEntityUuid));
            Assert.That(savedModel.SecondEntityUuid, Is.EqualTo(destinationEntityUuid));
        }

        [Test]
        public async Task GetForEntityUuid_WhenInvokedWithDestinationEntity_ShouldGetVirtualNetworkConnectionEntity()
        {
            var sourceEntityUuid = Guid.Parse("b8ea0706-679c-405f-83c7-3e2da0cfe283");
            var destinationEntityUuid = Guid.Parse("cb066f62-2094-46cf-87da-530fb1ad304b");
            await _context.VirtualNetworkEntityConnectionModels.AddAsync(new VirtualNetworkEntityConnectionModel
            {
                FirstEntityUuid = sourceEntityUuid,
                SecondEntityUuid = destinationEntityUuid
            });
            await _context.SaveChangesAsync();

            var savedModel = await _repository.GetForEntityUuid(destinationEntityUuid);

            Assert.That(savedModel.FirstEntityUuid, Is.EqualTo(sourceEntityUuid));
            Assert.That(savedModel.SecondEntityUuid, Is.EqualTo(destinationEntityUuid));
        }
    }
}
