using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualSwitchEntityRepositoryTests
    {
        private InterconnectDbContext _context;
        private VirtualSwitchEntityRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new VirtualSwitchEntityRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateVisibleVirtualSwitch()
        {
            var virtualSwitchUuid = Guid.Parse("f056d767-e170-498f-a8eb-b333782f8ea3");
            await _repository.Create("test", "testBridge", virtualSwitchUuid);

            var model = await _context.VirtualSwitchEntityModels.FirstAsync();

            Assert.Multiple(() =>
            {
                Assert.That(model.Uuid, Is.EqualTo(virtualSwitchUuid));
                Assert.That(model.Name, Is.EqualTo("test"));
                Assert.That(model.BridgeName, Is.EqualTo("testBridge"));
                Assert.That(model.Visible, Is.True);
            });
        }

        [Test]
        public async Task Create_WhenInvoked_ShouldCreateInvisibleVirtualSwitch()
        {
            var virtualSwitchUuid = Guid.Parse("f056d767-e170-498f-a8eb-b333782f8ea3");
            await _repository.CreateInvisible("testBridge", virtualSwitchUuid);

            var model = await _context.VirtualSwitchEntityModels.FirstAsync();

            Assert.Multiple(() =>
            {
                Assert.That(model.Uuid, Is.EqualTo(virtualSwitchUuid));
                Assert.That(model.Name, Is.Null);
                Assert.That(model.BridgeName, Is.EqualTo("testBridge"));
                Assert.That(model.Visible, Is.False);
            });
        }

        [Test]
        public async Task GetAll_WhenInvoked_ShouldGetAllVirtualSwitches()
        {
            var virtualSwitchUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                Name = "test",
                BridgeName = "testBridge",
                Uuid = virtualSwitchUuid,
                Visible = true,
            };

            await _context.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            var model = await _repository.GetAll();
            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model[0].Name, Is.EqualTo("test"));
            Assert.That(model[0].BridgeName, Is.EqualTo("testBridge"));
            Assert.That(model[0].Uuid, Is.EqualTo(virtualSwitchUuid));
            Assert.That(model[0].Visible, Is.EqualTo(true));
        }

        [Test]
        public async Task GetById_WhenInvokedWithCorrectId_ShouldGetVirtualSwitchById()
        {
            var virtualSwitchUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                Id = 1,
                Name = "test",
                BridgeName = "testBridge",
                Uuid = virtualSwitchUuid,
                Visible = true,
            };

            await _context.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            var model = await _repository.GetById(1);
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("test"));
            Assert.That(model.BridgeName, Is.EqualTo("testBridge"));
            Assert.That(model.Uuid, Is.EqualTo(virtualSwitchUuid));
            Assert.That(model.Visible, Is.EqualTo(true));
        }

        [Test]
        public async Task UpdateEntityPosition_WhenInvoked_ShouldUpdateEntityPosition()
        {
            var virtualSwitchUuid = Guid.Parse("60E7A667-78E4-49C4-8383-3CD796C621D5");
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                Id = 1,
                Name = "test",
                BridgeName = "testBridge",
                Uuid = virtualSwitchUuid,
                Visible = true,
            };
            await _context.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            await _repository.UpdateEntityPosition(1, 25, 54);

            var model = await _context.VirtualSwitchEntityModels.FirstAsync(m => m.Id == 1);
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.X, Is.EqualTo(25));
            Assert.That(model.Y, Is.EqualTo(54));
        }
    }
}
