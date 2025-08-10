using Controllers;
using Controllers.Requests;
using Models;
using Models.DTO;
using Models.Responses;
using Moq;
using Services;
using TestUtils;

namespace ControllersTests
{
    public class VirtualMachineControllerTests
    {
        private Mock<IBootableDiskProviderService> _bootableDiskProviderService;
        private Mock<IVirtualMachineEntityService> _virtualMachineEntityService;
        private Mock<IVirtualMachineManagerService> _virtualMachineManagerService;
        private VirtualMachineController _controller;

        private readonly CreateVirtualMachineRequest _testCreateVirtualMachineRequest = new()
        {
            Name = "Test",
            Memory = 1024,
            VirtualCPUs = 2,
            BootableDiskId = 13
        };
        private readonly VirtualMachineInfo _testVirtualMachineInfo = new()
        {
            Name = "Test",
            UsedMemory = 1024,
            State = 1,
            Uuid = "1451dddf-e57f-439f-8230-b6c5b2973edc"
        };
        private readonly VirtualMachineEntityDTO _testVirtualMachineEntityDTO = new()
        {
            Id = 1,
            Name = "Test",
            X = 25,
            Y = 25
        };
        private readonly VirtualMachineEntityDTO _testVirtualMachineEntityDTOWithUuid = new()
        {
            Id = 1,
            Name = "Test",
            X = 25,
            Y = 25,
            VmUuid = Guid.Parse("1451dddf-e57f-439f-8230-b6c5b2973edc")
        };

        [SetUp]
        public void SetUp()
        {
            _bootableDiskProviderService = new Mock<IBootableDiskProviderService>();
            _virtualMachineEntityService = new Mock<IVirtualMachineEntityService>();
            _virtualMachineManagerService = new Mock<IVirtualMachineManagerService>();
            _controller = new VirtualMachineController(_virtualMachineManagerService.Object, _virtualMachineEntityService.Object, _bootableDiskProviderService.Object);

            _bootableDiskProviderService.Setup(s => s.GetBootableDiskPathById(13)).ReturnsAsync("/tmp/a");
            _virtualMachineManagerService
                .Setup(s => s.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>()))
                .Returns(_testVirtualMachineInfo);
            _virtualMachineEntityService.Setup(s => s.CreateEntity("Test", 25, 25)).ReturnsAsync(_testVirtualMachineEntityDTO);
            _virtualMachineEntityService.Setup(s => s.UpdateEntityVmUUID(1, "1451dddf-e57f-439f-8230-b6c5b2973edc")).ReturnsAsync(_testVirtualMachineEntityDTOWithUuid);
        }

        [Test]
        public void CreateVirtualMachine_WhenProvidedWrongBootableDiskId_ShouldThrowException()
        {
            _bootableDiskProviderService.Setup(s => s.GetBootableDiskPathById(13)).ReturnsAsync((string?)null);

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _controller.CreateVirtualMachine(_testCreateVirtualMachineRequest);
            });
            _bootableDiskProviderService.Verify(s => s.GetBootableDiskPathById(13), Times.Once());
        }

        [Test]
        public async Task CreateVirtualMachine_WhenInvokedWithCorrectData_ShouldCreateVirtualMachine()
        {
            await _controller.CreateVirtualMachine(_testCreateVirtualMachineRequest);

            _virtualMachineManagerService.Verify(s =>
                s.CreateVirtualMachine(It.Is<VirtualMachineCreateDefinition>(m =>
                    m.VirtualCpus == 2 &&
                    m.Memory == 1024 &&
                    m.Name == "Test" &&
                    m.BootableDiskPath == "/tmp/a")
                ), Times.Once());
        }

        [Test]
        public async Task CreateVirtualMachine_WhenInvokedWithCorrectData_ShouldCreateVirtualMachineEntity()
        {
            await _controller.CreateVirtualMachine(_testCreateVirtualMachineRequest);

            _virtualMachineEntityService.Verify(s => s.CreateEntity("Test", 25, 25), Times.Once());
        }

        [Test]
        public async Task CreateVirtualMachine_WhenInvokedWithCorrectData_ShouldUpdateVirtualMachineEntityUuid()
        {
            await _controller.CreateVirtualMachine(_testCreateVirtualMachineRequest);

            _virtualMachineEntityService.Verify(s => s.UpdateEntityVmUUID(1, "1451dddf-e57f-439f-8230-b6c5b2973edc"), Times.Once());
        }

        [Test]
        public async Task CreateVirtualMachine_WhenCorrectCreateVm_ShouldReturnVirtualMachineEntityAsResponse()
        {
            var resp = await _controller.CreateVirtualMachine(_testCreateVirtualMachineRequest);

            var extractedResponse = ResponseExtractor.ExtractControllerResponse<VirtualMachineEntityResponse, VirtualMachineEntityDTO>(resp);

            Assert.That(extractedResponse?.Data?.Id, Is.EqualTo(1));
            Assert.That(extractedResponse?.Data?.Name, Is.EqualTo("Test"));
            Assert.That(extractedResponse?.Data?.VmUuid, Is.EqualTo(Guid.Parse("1451dddf-e57f-439f-8230-b6c5b2973edc")));
            Assert.That(extractedResponse?.Data?.X, Is.EqualTo(25));
            Assert.That(extractedResponse?.Data?.Y, Is.EqualTo(25));
        }
    }
}
