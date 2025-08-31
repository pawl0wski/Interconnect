using Models;
using Services.Utils;
using System.Xml.Linq;

namespace ServicesTests.Utils
{
    public class VirtualNetworkInterfaceCreateDefinitionBuilderTests
    {
        private VirtualNetworkInterfaceCreateDefinitionBuilder _definitionBuilder;

        [SetUp]
        public void OnSetup()
        {
            _definitionBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            _definitionBuilder.SetFromCreateDefinition(new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = "Test",
                MacAddress = "D0-8C-DC-3F-5D-DE",
            });
        }

        [Test]
        public void Build_WhenNothingIsProvided_ShouldThrowException()
        {
            _definitionBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();

            Assert.That(() => _definitionBuilder.Build(), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Build_WhenInvoked_ShouldContainNetworkName()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("interface")?.Element("source")?.Attribute("network")?.Value;
            Assert.That(value, Is.EqualTo("Test"));
        }

        [Test]
        public void Build_WhenInvoked_ShouldContainMacAddress()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Element("interface")?.Element("mac")?.Attribute("address")?.Value;
            Assert.That(value, Is.EqualTo("D0-8C-DC-3F-5D-DE"));
        }
    }
}
