using Database;
using Microsoft.EntityFrameworkCore;
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
    }
}
