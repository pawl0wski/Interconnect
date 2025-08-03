using Database;
using Models.Database;
using Models.Enums;
using Services.Impl;
using TestUtils;

namespace ServicesTests
{
    public class BootableDiskProviderServiceTests
    {
        private InterconnectDbContext _context;
        private string _testBootableDiskPath;

        [SetUp]
        public void SetUp()
        {
            _context = TestMocks.CreateInMemoryDbContext();
            _testBootableDiskPath = Path.GetTempFileName();
            File.WriteAllText(_testBootableDiskPath, "TEST");
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task GetAvailableBootableDiskModels_WhenInvoked_ShouldGetAllBootableDisksWithPath()
        {
            var bootableDiskProviderService = new BootableDiskProviderService(_context);
            _context.BootableDiskModels.Add(new BootableDiskModel
            {
                Id = 1,
                Name = "TestDisk",
                Version = "1.0.0",
                Path = _testBootableDiskPath,
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

            var bootableDisks = await bootableDiskProviderService.GetAvailableBootableDiskModels();

            Assert.That(bootableDisks, Has.Count.EqualTo(1));
            Assert.That(bootableDisks[0].Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetBootableDiskPathById_WhenInvokedWithCorrectId_ShouldReturnBootableDiskPath()
        {
            var bootableDiskProviderService = new BootableDiskProviderService(_context);
            _context.BootableDiskModels.Add(new BootableDiskModel {
                Id = 1,
                Name = "TestDisk",
                Version = "1.0.0",
                Path = _testBootableDiskPath,
                OperatingSystemType = OperatingSystemType.Windows
            });
            await _context.SaveChangesAsync();

            var bootableDisk = await bootableDiskProviderService.GetBootableDiskPathById(1);

            Assert.That(bootableDisk, Is.EqualTo(_testBootableDiskPath));
        }
    }
}
