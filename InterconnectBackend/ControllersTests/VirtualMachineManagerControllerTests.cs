using Controllers;
using Models;
using Moq;
using Services;

namespace ControllersTests
{
    class VirtualMachineManagerControllerTests
    {
        private Mock<IVirtualMachineManagerService> _mockManagerService;

        [SetUp]
        public void Setup()
        {
            _mockManagerService = new Mock<IVirtualMachineManagerService>();
        }

        [Test]
        public void CreateVirtualMachine_WhenInvoked_ShouldCreateVirtualMachine()
        {
            var controller = new VirtualMachineManagerController(_mockManagerService.Object);
            _mockManagerService.Setup(m => m.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>()));

            var response = controller.CreateVirtualMachine(new VirtualMachineCreateDefinition
            {
                BootableDiskPath = "/disk",
                Memory = 12,
                Name = "test",
                VirtualCpus = 1
            });

            _mockManagerService.Verify(m => m.CreateVirtualMachine(It.IsAny<VirtualMachineCreateDefinition>()), Times.Once);
        }

        [Test]
        public void GetListOfVirtualMachines_WhenInvoked_ShouldListAllVirtualMachines()
        {
            var controller = new VirtualMachineManagerController(_mockManagerService.Object);
            _mockManagerService.Setup(m => m.GetListOfVirtualMachines());

            var response = controller.GetListOfVirtualMachines();

            _mockManagerService.Verify(m => m.GetListOfVirtualMachines(), Times.Once);
        }
    }
}
