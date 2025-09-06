using Models;
using Repositories;
using Repositories.Impl;

namespace RepositoriesTests
{
    public class VirtualMachineConsoleStreamRepositoryTests
    {
        private IVirtualMachineConsoleStreamRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new VirtualMachineConsoleStreamRepository();
        }

        [Test]
        public void Add_WhenInvoked_ShouldAddNewStream()
        {
            var testUuid = Guid.Parse("90fa25be-ae9d-4d24-806a-b807b13312b8");
            var testStreamInfo = new StreamInfo
            {
                Uuid = testUuid,
                Stream = 123,
            };

            _repository.Add(testStreamInfo);
            var data = _repository.GetByUuid(testUuid);

            Assert.That(data.Uuid, Is.EqualTo(testUuid));
            Assert.That(data.Stream, Is.EqualTo((IntPtr)123));
        }

        [Test]
        public void GetAllStreams_WhenInvoked_ShouldGetAllStreams()
        {
            var firstTestUuid = Guid.Parse("90fa25be-ae9d-4d24-806a-b807b13312b8");
            var secondTestUuid = Guid.Parse("ef328d5b-0de0-4e9d-9344-752e4f0085f9");
            var firstTestStreamInfo = new StreamInfo
            {
                Uuid = firstTestUuid,
                Stream = 123,
            };
            var secondTestStreamInfo = new StreamInfo
            {
                Uuid = secondTestUuid,
                Stream = 321,
            };

            _repository.Add(firstTestStreamInfo);
            _repository.Add(secondTestStreamInfo);
            var data = _repository.GetAllStreams();

            Assert.That(data.Count, Is.EqualTo(2));
            Assert.That(data[0].Uuid, Is.EqualTo(firstTestUuid));
            Assert.That(data[0].Stream, Is.EqualTo((IntPtr)123));
            Assert.That(data[1].Uuid, Is.EqualTo(secondTestUuid));
            Assert.That(data[1].Stream, Is.EqualTo((IntPtr)321));
        }

        [Test]
        public void Remove_WhenInvoked_ShouldRemoveStream()
        {
            var testUuid = Guid.Parse("90fa25be-ae9d-4d24-806a-b807b13312b8");
            var testStreamInfo = new StreamInfo
            {
                Uuid = testUuid,
                Stream = 123,
            };

            _repository.Add(testStreamInfo);
            var data = _repository.GetByUuid(testUuid);

            Assert.That(data.Uuid, Is.EqualTo(testUuid));
            Assert.That(data.Stream, Is.EqualTo((IntPtr)123));

            _repository.Remove(testStreamInfo);
            Assert.That(_repository.GetByUuid(testUuid), Is.EqualTo(null));
        }
    }
}
