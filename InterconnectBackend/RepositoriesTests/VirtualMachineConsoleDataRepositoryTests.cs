using Repositories;
using Repositories.Impl;
using TestUtils;

namespace RepositoriesTests
{
    public class VirtualMachineConsoleDataRepositoryTests
    {
        private IVirtualMachineConsoleDataRepository _repository;

        [SetUp]
        public void Setup()
        {
            var config = TestMocks.GetMockConfig();
            config.Value.MaxConsoleDataHistory = 4;
            _repository = new VirtualMachineConsoleDataRepository(config);
        }

        [Test]
        public void AddDataToConsole_WhenInvoked_ShouldAddDataToConsole()
        {
            var testUuid = Guid.Parse("90fa25be-ae9d-4d24-806a-b807b13312b8");

            _repository.AddDataToConsole(testUuid, [123, 244]);

            var result = _repository.GetData(testUuid);
            Assert.That(result, Is.EquivalentTo(new byte[] { 123, 244 }));
        }

        [Test]
        public void AddDataToConsole_ExceedDataLength_ShouldTrimData()
        {
            var testUuid = Guid.Parse("90fa25be-ae9d-4d24-806a-b807b13312b8");

            _repository.AddDataToConsole(testUuid, [1, 2, 3, 4]);
            _repository.AddDataToConsole(testUuid, [5]);

            var result = _repository.GetData(testUuid);
            Assert.That(result, Is.EquivalentTo(new byte[] { 2, 3, 4, 5 }));
        }
    }
}
