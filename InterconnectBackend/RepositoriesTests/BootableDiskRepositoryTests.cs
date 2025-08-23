using Database;
using Models.Database;
using Models.Enums;
using Repositories;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class BootableDiskRepositoryTests
    {
        private InterconnectDbContext _context;
        private IBootableDiskRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _repository = new BootableDiskRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task GetOnlyWithNotNullablePath_WhenInvoked_ShouldReturnBootableDisksWithNotNullablePaths()
        {
            _context.BootableDiskModels.Add(new BootableDiskModel
            {
                Id = 1,
                Name = "TestDisk",
                Version = "1.0.0",
                Path = "123",
                OperatingSystemType = OperatingSystemType.Windows
            });
            _context.BootableDiskModels.Add(new BootableDiskModel
            {
                Id = 2,
                Name = "TestDiskWithoutPath",
                Version = "1.0.0",
                Path = null,
                OperatingSystemType = OperatingSystemType.Linux
            });
            await _context.SaveChangesAsync();

            var result = await _repository.GetOnlyWithNotNullablePath();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(x => x == null), Is.False);
        }

        [Test]
        public async Task GetById_WhenInvoked_ShouldGetBootableDiskUsingProvidedId()
        {
            _context.BootableDiskModels.Add(new BootableDiskModel
            {
                Id = 54,
                Name = "TestDisk",
                Version = "1.0.0",
                Path = "123",
                OperatingSystemType = OperatingSystemType.Windows
            });
            await _context.SaveChangesAsync();

            var result = await _repository.GetById(54);

            Assert.That(result.Id, Is.EqualTo(54));
            Assert.That(result.Name, Is.EqualTo("TestDisk"));
        }
    }
}