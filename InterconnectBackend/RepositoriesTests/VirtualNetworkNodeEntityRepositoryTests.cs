using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualNetworkNodeEntityRepositoryTests
    {
        private InterconnectDbContext _context;
        private VirtualNetworkNodeEntityRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new VirtualNetworkNodeEntityRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateVisibleVirtualNetworkNode()
        {
            var virtualNetworkNodeUuid = Guid.Parse("f056d767-e170-498f-a8eb-b333782f8ea3");
            await _repository.Create("test", new VirtualNetworkModel
            {
                BridgeName = "test",
                Uuid = virtualNetworkNodeUuid
            });

            var model = await _context.VirtualNetworkNodeEntityModels.FirstAsync();

            Assert.Multiple(() =>
            {
                Assert.That(model.VirtualNetwork.Uuid, Is.EqualTo(virtualNetworkNodeUuid));
                Assert.That(model.Name, Is.EqualTo("test"));
                Assert.That(model.VirtualNetwork.BridgeName, Is.EqualTo("test"));
                Assert.That(model.Visible, Is.True);
            });
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateInvisibleVirtualNetworkNode()
        {
            var virtualNetworkNodeUuid = Guid.Parse("f056d767-e170-498f-a8eb-b333782f8ea3");
            await _repository.CreateInvisible(new VirtualNetworkModel
            {
                BridgeName = "test",
                Uuid = virtualNetworkNodeUuid
            });

            var model = await _context.VirtualNetworkNodeEntityModels.FirstAsync();

            Assert.Multiple(() =>
            {
                Assert.That(model.VirtualNetwork.Uuid, Is.EqualTo(virtualNetworkNodeUuid));
                Assert.That(model.Name, Is.Null);
                Assert.That(model.VirtualNetwork.BridgeName, Is.EqualTo("test"));
                Assert.That(model.Visible, Is.False);
            });
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllVirtualNetworkNodes()
        {
            var virtualNetworkNodeUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualNetworkNode = new VirtualNetworkNodeEntityModel
            {
                Name = "test",
                VirtualNetwork = new VirtualNetworkModel
                {
                    BridgeName = "testBridge",
                    Uuid = virtualNetworkNodeUuid,

                },
                Visible = true,
            };

            await _context.AddAsync(virtualNetworkNode);
            await _context.SaveChangesAsync();

            var model = await _repository.GetAll();
            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model[0].Name, Is.EqualTo("test"));
            Assert.That(model[0].VirtualNetwork.BridgeName, Is.EqualTo("testBridge"));
            Assert.That(model[0].VirtualNetwork.Uuid, Is.EqualTo(virtualNetworkNodeUuid));
            Assert.That(model[0].Visible, Is.EqualTo(true));
        }

        [Test]
        public async Task GetById_WhenInvokedWithCorrectId_ShouldGetVirtualNetworkNodeById()
        {
            var virtualNetworkNodeUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualNetworkNode = new VirtualNetworkNodeEntityModel
            {
                Id = 1,
                Name = "test",
                VirtualNetwork = new VirtualNetworkModel
                {
                    BridgeName = "testBridge",
                    Uuid = virtualNetworkNodeUuid,
                },
                Visible = true,
            };

            await _context.AddAsync(virtualNetworkNode);
            await _context.SaveChangesAsync();

            var model = await _repository.GetById(1);
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("test"));
            Assert.That(model.VirtualNetwork.BridgeName, Is.EqualTo("testBridge"));
            Assert.That(model.VirtualNetwork.Uuid, Is.EqualTo(virtualNetworkNodeUuid));
            Assert.That(model.Visible, Is.EqualTo(true));
        }

        [Test]
        public async Task UpdateEntityPosition_WhenInvoked_ShouldUpdateEntityPosition()
        {
            var virtualNetworkNodeUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualNetworkNode = new VirtualNetworkNodeEntityModel
            {
                Id = 1,
                Name = "test",
                VirtualNetwork = new VirtualNetworkModel
                {
                    BridgeName = "testBridge",
                    Uuid = virtualNetworkNodeUuid,
                },
                Visible = true,
            };
            await _context.AddAsync(virtualNetworkNode);
            await _context.SaveChangesAsync();

            await _repository.UpdateEntityPosition(1, 25, 54);

            var model = await _context.VirtualNetworkNodeEntityModels.FirstAsync(m => m.Id == 1);
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.X, Is.EqualTo(25));
            Assert.That(model.Y, Is.EqualTo(54));
        }
    }
}
