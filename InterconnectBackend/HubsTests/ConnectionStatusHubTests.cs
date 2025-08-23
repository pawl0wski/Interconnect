using Hubs;

namespace HubsTests
{
    public class ConnectionStatusHubTests
    {
        private ConnectionStatusHub _hub;

        [SetUp]
        public void Setup()
        {
            _hub = new ConnectionStatusHub();
        }

        [TearDown]
        public void Teardown()
        {
            _hub.Dispose();
        }

        [Test]
        public void Ping_WhenInvoked_ShouldReturnPong()
        {
            var response = _hub.Ping();

            Assert.That(response.Data, Is.EqualTo("Pong"));
        }
    }
}