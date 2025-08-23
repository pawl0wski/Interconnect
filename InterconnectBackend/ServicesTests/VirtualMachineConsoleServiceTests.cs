using Models;
using Moq;
using NativeLibrary.Structs;
using NativeLibrary.Wrappers;
using Repositories;
using Services.Impl;

namespace ServicesTests
{
    public class VirtualMachineConsoleServiceTests
    {
        private Mock<IVirtualizationWrapper> _virtualizationWrapper;
        private Mock<IVirtualMachineConsoleDataRepository> _consoleDataRespository;
        private Mock<IVirtualMachineConsoleStreamRepository> _consoleStreamRespository;
        private VirtualMachineConsoleService _vmConsoleService;

        [SetUp]
        public void Setup()
        {
            _virtualizationWrapper = new Mock<IVirtualizationWrapper>();
            _consoleDataRespository = new Mock<IVirtualMachineConsoleDataRepository>();
            _consoleStreamRespository = new Mock<IVirtualMachineConsoleStreamRepository>();
            _vmConsoleService = new VirtualMachineConsoleService(
                _virtualizationWrapper.Object,
                _consoleDataRespository.Object,
                _consoleStreamRespository.Object);
        }

        [Test]
        public void GetInitialConsoleData_WhenInvoked_ShouldReturnInitialConsoleData()
        {
            var testGuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _consoleDataRespository.Setup(r => r.GetData(It.Is<Guid>(v => v == testGuid))).Returns([255, 123]);

            var result = _vmConsoleService.GetInitialConsoleData(testGuid);

            _consoleDataRespository.Verify(r => r.GetData(It.Is<Guid>(v => v == testGuid)), Times.Once);
            Assert.That(result, Is.EquivalentTo(new List<byte>([255, 123])));
        }

        [Test]
        public void GetBytesFromConsole_WhenInvoked_ShouldGetBytesFromConsole()
        {
            var testUuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _virtualizationWrapper.Setup(w => w.GetDataFromStream(It.IsAny<IntPtr>())).Returns(new NativeStreamData
            {
                Buffer = [123, 232],
                IsStreamBroken = false,
            });

            var result = _vmConsoleService.GetBytesFromConsole(new StreamInfo
            {
                Uuid = testUuid,
                Stream = 123,
            });

            _virtualizationWrapper.Verify(w => w.GetDataFromStream(It.IsAny<IntPtr>()), Times.Once);
            Assert.That(result.Data, Is.EqualTo(new byte[] { 123, 232 }));
            Assert.That(result.IsStreamBroken, Is.False);
        }

        [Test]
        public void GetBytesFromConsole_WhenInvoked_ShouldSaveBytesToRepository()
        {
            var testUuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _virtualizationWrapper.Setup(w => w.GetDataFromStream(It.IsAny<IntPtr>())).Returns(new NativeStreamData
            {
                Buffer = [123, 232],
                IsStreamBroken = false,
            });
            _consoleDataRespository.Setup(r => r.AddDataToConsole(It.Is<Guid>(v => v == testUuid), It.IsAny<byte[]>()));

            var result = _vmConsoleService.GetBytesFromConsole(new StreamInfo
            {
                Uuid = testUuid,
                Stream = 123,
            });

            _consoleDataRespository.Verify(r => r.AddDataToConsole(
                It.Is<Guid>(v => v == testUuid),
                It.Is<byte[]>(v => v.SequenceEqual(new byte[] { 123, 232 }))
            ), Times.Once);
        }

        [Test]
        public void OpenVirtualMachineConsole_WhenInvoked_ShouldOpenVirtualMachineConsole()
        {
            var testUuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _virtualizationWrapper.Setup(w => w.OpenVirtualMachineConsole(It.Is<Guid>(v => v == testUuid))).Returns(123);

            _vmConsoleService.OpenVirtualMachineConsole(testUuid);

            _virtualizationWrapper.Verify(w => w.OpenVirtualMachineConsole(It.Is<Guid>(v => v == testUuid)), Times.Once);
        }

        [Test]
        public void OpenVirtualMachineConsole_WhenInvokedAndVmCreated_ShouldAddStreamToRepository()
        {
            var testUuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _virtualizationWrapper.Setup(w => w.OpenVirtualMachineConsole(It.Is<Guid>(v => v == testUuid))).Returns(123);
            _consoleStreamRespository.Setup(r => r.Add(It.IsAny<StreamInfo>()));

            _vmConsoleService.OpenVirtualMachineConsole(testUuid);

            _consoleStreamRespository.Verify(r => r.Add(It.Is<StreamInfo>(v => v.Uuid == testUuid && v.Stream == 123)), Times.Once);
        }

        [Test]
        public void SendBytesToVirtualMachineConsoleByUuid_WhenInvoked_ShouldSendDataToStream()
        {
            var testUuid = Guid.Parse("fe18228a-d7b0-4f1f-b17a-85d839e4d023");
            _consoleStreamRespository.Setup(r => r.GetByUuid(It.IsAny<Guid>())).Returns(new StreamInfo
            {
                Uuid = testUuid,
                Stream = 123
            });
            _virtualizationWrapper.Setup(w => w.SendDataToStream(It.IsAny<IntPtr>(), It.IsAny<string>()));

            _vmConsoleService.SendBytesToVirtualMachineConsoleByUuid(testUuid, "123");

            _virtualizationWrapper.Verify(w => w.SendDataToStream(It.Is<IntPtr>(v => v == 123), It.Is<string>(v => v == "123")), Times.Once);
        }

        [Test]
        public void GetStreams_WhenInvoked_ShouldReturnStreams()
        {
            _consoleStreamRespository.Setup(r => r.GetAllStreams()).Returns(new List<StreamInfo>
            {
                new StreamInfo
                {
                    Uuid = Guid.Parse("bf50384f-8fb7-4ecb-ba73-a36ef1697444"),
                    Stream = 123,
                },
                new StreamInfo
                {
                    Uuid = Guid.Parse("2e8f08c8-b201-46c7-a1c6-75270cc60ac3"),
                    Stream = 321,
                }
            });

            var streams = _vmConsoleService.GetStreams();

            Assert.That(streams.Count, Is.EqualTo(2));
            Assert.That(streams[0].Uuid, Is.EqualTo(Guid.Parse("bf50384f-8fb7-4ecb-ba73-a36ef1697444")));
            Assert.That(streams[0].Stream, Is.EqualTo((IntPtr)123));
            Assert.That(streams[1].Uuid, Is.EqualTo(Guid.Parse("2e8f08c8-b201-46c7-a1c6-75270cc60ac3")));
            Assert.That(streams[1].Stream, Is.EqualTo((IntPtr)321));
        }

        [Test]
        public void CloseStream_WhenInvoked_ShouldRemoveStreamFromRepository()
        {
            var testStreamInfo = new StreamInfo
            {
                Uuid = Guid.Parse("2e8f08c8-b201-46c7-a1c6-75270cc60ac3"),
                Stream = 123
            };
            _consoleStreamRespository.Setup(r => r.Remove(It.IsAny<StreamInfo>()));

            _vmConsoleService.CloseStream(testStreamInfo);

            _consoleStreamRespository.Verify(r => r.Remove(testStreamInfo));
        }

        [Test]
        public void CloseStream_WhenInvoked_ShouldCloseStream()
        {
            var testStreamInfo = new StreamInfo
            {
                Uuid = Guid.Parse("2e8f08c8-b201-46c7-a1c6-75270cc60ac3"),
                Stream = 123
            };
            _virtualizationWrapper.Setup(w => w.CloseStream(It.IsAny<IntPtr>()));

            _vmConsoleService.CloseStream(testStreamInfo);

            _virtualizationWrapper.Verify(w => w.CloseStream(It.Is<IntPtr>(v => v == testStreamInfo.Stream)));
        }
    }
}
