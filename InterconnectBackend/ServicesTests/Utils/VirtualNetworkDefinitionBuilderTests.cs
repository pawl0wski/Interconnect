using Models;
using Services.Utils;
using System.Xml.Linq;

namespace ServicesTests.Utils
{
    public class VirtualNetworkDefinitionBuilderTests
    {
        private VirtualNetworkCreateDefinitionBuilder _definitionBuilder;

        [SetUp]
        public void OnSetup()
        {
            _definitionBuilder = new VirtualNetworkCreateDefinitionBuilder();
            _definitionBuilder.SetFromCreateDefinition(new VirtualNetworkCreateDefinition
            {
                NetworkName = "Test",
                MacAddress = "D0-8C-DC-3F-5D-DE",
                IpAddress = "192.153.231.123",
                NetMask = "255.255.255.0",
                DhcpEnabled = true,
                DhcpStartRange = "192.0.0.1",
                DhcpEndRange = "192.0.0.2",
            });
        }

        [Test]
        public void Build_WhenNothingIsProvided_ShouldThrowException()
        {
            _definitionBuilder = new VirtualNetworkCreateDefinitionBuilder();

            Assert.That(() => _definitionBuilder.Build(), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Build_WhenInvoked_ShouldIncludeNetworkName()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("network")?.Element("name")?.Value;
            Assert.That(value, Is.EqualTo("Test"));
        }

        [Test]
        public void Build_WhenInvoked_ShouldIncludeMacAddress()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("network")?.Element("mac")?.Attribute("address")?.Value;
            Assert.That(value, Is.EqualTo("D0-8C-DC-3F-5D-DE"));
        }

        [Test]
        public void Build_WhenInvoked_ShouldIncludeIpAddress()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("network")?.Element("ip")?.Attribute("address")?.Value;
            Assert.That(value, Is.EqualTo("192.153.231.123"));
        }

        [Test]
        public void Build_WhenInvoked_ShouldIncludeNetmask()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("network")?.Element("ip")?.Attribute("netmask")?.Value;
            Assert.That(value, Is.EqualTo("255.255.255.0"));
        }

        [Test]
        public void Build_WhenInvoked_ShouldIncludeDhcpRange()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var range = doc.Element("network")?.Element("ip")?.Element("dhcp")?.Element("range");
            var startValue = range?.Attribute("start")?.Value;
            var endValue = range?.Attribute("end")?.Value;
            Assert.That(startValue, Is.EqualTo("192.0.0.1"));
            Assert.That(endValue, Is.EqualTo("192.0.0.2"));
        }

        [Test]
        public void Build_WhenInvokedWithDhcpDisabled_ShouldNotIncludeDhcpRange()
        {
            _definitionBuilder = new VirtualNetworkCreateDefinitionBuilder();
            _definitionBuilder.SetFromCreateDefinition(new VirtualNetworkCreateDefinition
            {
                NetworkName = "Test",
                MacAddress = "D0-8C-DC-3F-5D-DE",
                IpAddress = "192.153.231.123",
                NetMask = "255.255.255.0",
                DhcpEnabled = false,
                DhcpStartRange = "192.0.0.1",
                DhcpEndRange = "192.0.0.2",
            });

            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var dhcp = doc.Element("network")?.Element("ip")?.Element("dhcp");
            Assert.That(dhcp, Is.Null);
        }
    }
}
