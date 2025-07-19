using Models;
using Services.Utils;
using System.Xml.Linq;

namespace ServicesTests.Utils
{
    public class VirtualMachineCreateDefinitionBuilderTests
    {
        private VirtualMachineCreateDefinitionBuilder _definitionBuilder;

        [SetUp]
        public void OnSetup()
        {
            _definitionBuilder = new VirtualMachineCreateDefinitionBuilder();
            _definitionBuilder.SetFromCreateDefinition(new VirtualMachineCreateDefinition
            {
                BootableDiskPath = "//MockBootableDiskPath",
                Memory = 1000,
                Name = "MockName",
                VirtualCpus = 5,
            }, "test");
        }

        [Test]
        public void Build_WhenNothingIsProvided_ShouldThrowException()
        {
            _definitionBuilder = new VirtualMachineCreateDefinitionBuilder();

            Assert.That(() => _definitionBuilder.Build(), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Build_WhenBootableDiskPathProvided_ShouldIncludeBootableDiskPathInXml()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Descendants("source")
                            .FirstOrDefault()
                            ?.Attribute("file")
                            ?.Value;
            Assert.That(value, Is.EqualTo("//MockBootableDiskPath"));
        }

        [Test]
        public void Build_WhenMemoryProvided_ShouldIncludeMemoryInXml()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Root?.Element("memory")?.Value;
            Assert.That(value, Is.EqualTo("1000"));
        }

        [Test]
        public void Build_WhenNameProvided_ShouldIncludeNameInXml()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Root?.Element("name")?.Value;
            Assert.That(value, Is.EqualTo("test_MockName"));
        }

        [Test]
        public void Build_WhenVirtualCpusProvided_ShouldIncludeVirtualCpusInXml()
        {
            var definition = _definitionBuilder.Build();
            var doc = XDocument.Parse(definition);

            var value = doc.Root?.Element("vcpu")?.Value;
            Assert.That(value, Is.EqualTo("5"));
        }
    }
}
