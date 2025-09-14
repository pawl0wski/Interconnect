using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class InternetEntityRepositoryTests
    {
        private InterconnectDbContext _context;
        private InternetEntityRepository _internetRepository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _internetRepository = new InternetEntityRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateNewInternetEntity()
        {
            var uuid = Guid.Parse("989D81A2-FD93-476D-942D-61987F399890");
            await _internetRepository.Create(new VirtualNetworkModel
            {
                BridgeName = "TestBridge",
                Uuid = uuid
            });

            var model = await _context.InternetEntityModels.FirstAsync();

            Assert.Multiple(() =>
            {
                Assert.That(model.Id, Is.EqualTo(1));
                Assert.That(model.X, Is.EqualTo(0));
                Assert.That(model.Y, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllInternetEntities()
        {
            var uuid = Guid.Parse("989D81A2-FD93-476D-942D-61987F399890");
            await _context.InternetEntityModels.AddAsync(new InternetEntityModel
            {
                VirtualNetwork = new VirtualNetworkModel { BridgeName = "abc", Uuid = uuid },
                X = 1,
                Y = 15
            });
            await _context.SaveChangesAsync();

            var models = await _internetRepository.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(models.Count(), Is.EqualTo(1));
                Assert.That(models[0].X, Is.EqualTo(1));
                Assert.That(models[0].Y, Is.EqualTo(15));
            });
        }
    }
}
