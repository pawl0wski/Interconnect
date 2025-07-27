using Microsoft.Extensions.Options;
using Models.Config;
using Moq;
using Services;
using Services.Impl;
using TestUtils;

namespace ServicesTests
{
    public class InitializationServiceTests
    {
        private Mock<IHypervisorConnectionService> _connService;
        private IOptions<InterconnectConfig> _interconnectConfig;

        [SetUp]
        public void SetUp()
        {
            _connService = new Mock<IHypervisorConnectionService>();
            _interconnectConfig = TestMocks.GetMockConfig();
        }

        [Test]
        public void Initialize_WhenInvoked_ShouldConnectToHypervisor()
        {
            var initializationService = new InitializationService(_connService.Object, _interconnectConfig);

            initializationService.Initialize();

            _connService.Verify((c) => c.InitializeConnection(It.Is<string>((v) => v == "test:///testing")), Times.Once());
        }
    }
}
