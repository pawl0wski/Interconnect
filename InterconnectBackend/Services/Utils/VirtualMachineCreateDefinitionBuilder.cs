using Models;
using System.Xml;

namespace Services.Utils
{
    public class VirtualMachineCreateDefinitionBuilder
    {
        private string? _name;
        private uint? _memory;
        private uint? _virtualCpus;
        private string? _bootableDiskPath;

        public void SetFromCreateDefinition(VirtualMachineCreateDefinition definition, string prefix)
        {
            _name = definition.GetVirtualMachineNameWithPrefix(prefix);
            _memory = definition.Memory;
            _virtualCpus = definition.VirtualCpus;
            _bootableDiskPath = definition.BootableDiskPath;
        }

        public string Build()
        {
            if (_name is null || _memory is null || _virtualCpus is null || _bootableDiskPath is null)
            {
                throw new Exception("There are not all the things needed to build the XML definition for the virtual machine");
            }

            using var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            }))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("domain");
                writer.WriteAttributeString("type", "qemu");

                writer.WriteElementString("name", _name);

                writer.WriteStartElement("memory");
                writer.WriteAttributeString("unit", "KiB");
                writer.WriteString(_memory.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("vcpu");
                writer.WriteAttributeString("placement", "static");
                writer.WriteString(_virtualCpus.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("os");

                writer.WriteStartElement("type");
                writer.WriteAttributeString("arch", "x86_64");
                writer.WriteAttributeString("machine", "q35");
                writer.WriteString("hvm");
                writer.WriteEndElement();

                writer.WriteStartElement("boot");
                writer.WriteAttributeString("dev", "cdrom");
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteStartElement("controller");
                writer.WriteAttributeString("type", "pci");
                writer.WriteAttributeString("model", "pcie-root");
                writer.WriteEndElement();

                writer.WriteStartElement("controller");
                writer.WriteAttributeString("type", "pci");
                writer.WriteAttributeString("model", "pcie-root-port");
                writer.WriteEndElement();

                writer.WriteStartElement("controller");
                writer.WriteAttributeString("type", "sata");
                writer.WriteAttributeString("index", "0");
                writer.WriteEndElement();

                writer.WriteStartElement("devices");

                writer.WriteStartElement("disk");
                writer.WriteAttributeString("type", "file");
                writer.WriteAttributeString("device", "cdrom");

                writer.WriteStartElement("driver");
                writer.WriteAttributeString("name", "qemu");
                writer.WriteAttributeString("type", "raw");
                writer.WriteEndElement();

                writer.WriteStartElement("source");
                writer.WriteAttributeString("file", _bootableDiskPath);
                writer.WriteEndElement();

                writer.WriteStartElement("target");
                writer.WriteAttributeString("dev", "sda");
                writer.WriteEndElement();

                writer.WriteStartElement("readonly");
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteStartElement("serial");
                writer.WriteAttributeString("type", "pty");

                writer.WriteStartElement("target");
                writer.WriteAttributeString("port", "0");
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }

            return stringWriter.ToString();
        }
    }
}
