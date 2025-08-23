using Models.Database;
using Models.Enums;
using Moq;
using Repositories;
using Services.Impl;

namespace ServicesTests
{
    public class BootableDiskProviderServiceTests
    {
        private Mock<IBootableDiskRepository> _repository;
        private string _testBootableDiskPath;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IBootableDiskRepository>();
            _testBootableDiskPath = Path.GetTempFileName();
            File.WriteAllText(_testBootableDiskPath, "TEST");
        }

        [TearDown]
        public void TearDown()
        {
            File.ReadAllText(_testBootableDiskPath);
        }

        [Test]
        public async Task GetAvailableBootableDiskModels_WhenInvoked_ShouldGetAllBootableDisksWithPath()
        {
            var bootableDiskProviderService = new BootableDiskProviderService(_repository.Object);
            _repository.Setup(r => r.GetOnlyWithNotNullablePath()).ReturnsAsync([
                new BootableDiskModel
                {
                    Id = 1,
                    Name = "TestDisk",
                    Version = "1.0.0",
                    Path = _testBootableDiskPath,
                    OperatingSystemType = OperatingSystemType.Windows
                },
                new BootableDiskModel
                {
                    Id = 2,
                    Name = "TestDiskWithoutPath",
                    Version = "1.0.0",
                    Path = null,
                    OperatingSystemType = OperatingSystemType.Linux
                }
                ]);

            var bootableDisks = await bootableDiskProviderService.GetAvailableBootableDiskModels();

            Assert.That(bootableDisks, Has.Count.EqualTo(1));
            Assert.That(bootableDisks[0].Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetBootableDiskPathById_WhenInvokedWithCorrectId_ShouldReturnBootableDiskPath()
        {
            var bootableDiskProviderService = new BootableDiskProviderService(_repository.Object);
            _repository.Setup(r => r.GetById(It.Is<int>(v => v == 1))).ReturnsAsync(new BootableDiskModel
            {
                Id = 1,
                Name = "TestDisk",
                Version = "1.0.0",
                Path = _testBootableDiskPath,
                OperatingSystemType = OperatingSystemType.Windows
            });

            var bootableDisk = await bootableDiskProviderService.GetBootableDiskPathById(1);

            Assert.That(bootableDisk, Is.EqualTo(_testBootableDiskPath));
        }
    }
}
