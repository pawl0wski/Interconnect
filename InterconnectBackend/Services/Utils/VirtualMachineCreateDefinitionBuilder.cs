using Models;
using System.Xml;

namespace Services.Utils
{

    public class VirtualMachineCreateDefinitionBuilder
    {
        public delegate void BuildingBlock(XmlWriter writer);
        private string? Name;
        private uint? Memory;
        private uint? VirtualCpus;
        private string? BootableDiskPath;

        public void SetFromCreateDefinition(VirtualMachineCreateDefinition definition, string prefix)
        {
            Name = definition.GetVirtualMachineNameWithPrefix(prefix);
            Memory = definition.Memory;
            VirtualCpus = definition.VirtualCpus;
            BootableDiskPath = definition.BootableDiskPath;
        }

        public string Build()
        {
            if (Name is null || Memory is null || VirtualCpus is null || BootableDiskPath is null)
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

                CreateDomainBlock(writer, w =>
                {
                    CreateNameBlock(w, Name);
                    CreateMemoryBlock(w, (uint)Memory);
                    CreateVCPUBlock(w, (uint)VirtualCpus);
                    CreateOsBlock(w, w =>
                    {
                        CreateTypeBlock(w);
                        CreateBootBlock(w);
                    });

                    CreateFeaturesBlock(w);
                    CreateDevicesBlock(w, w =>
                    {
                        CreateIsoDiskBlock(w, BootableDiskPath);
                        //CreateInterfaceBlock(w);
                        CreateConsoleBlock(w);
                        CreateSerialBlock(w);
                    });
                });

                writer.WriteEndDocument();
            }

            return stringWriter.ToString();
        }

        static private void CreateDomainBlock(XmlWriter writer, BuildingBlock blockFunc)
        {
            writer.WriteStartElement("domain");
            writer.WriteAttributeString("type", "qemu");

            blockFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateNameBlock(XmlWriter writer, string name)
        {
            writer.WriteElementString("name", name);
        }

        static private void CreateMemoryBlock(XmlWriter writer, uint memory)
        {
            writer.WriteStartElement("memory");

            writer.WriteAttributeString("unit", "KiB");
            writer.WriteString(memory.ToString());

            writer.WriteEndElement();
        }

        static private void CreateVCPUBlock(XmlWriter writer, uint vcpus)
        {
            writer.WriteStartElement("vcpu");

            writer.WriteAttributeString("placement", "static");
            writer.WriteString(vcpus.ToString());

            writer.WriteEndElement();
        }

        static private void CreateOsBlock(XmlWriter writer, BuildingBlock buildingFunc)
        {
            writer.WriteStartElement("os");

            buildingFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateBootBlock(XmlWriter writer)
        {
            writer.WriteStartElement("boot");
            writer.WriteAttributeString("dev", "cdrom");
            writer.WriteEndElement();
        }

        static private void CreateTypeBlock(XmlWriter writer)
        {
            writer.WriteStartElement("type");
            writer.WriteAttributeString("arch", "x86_64");
            writer.WriteAttributeString("machine", "pc-q35-7.1");
            writer.WriteString("hvm");
            writer.WriteEndElement();
        }

        static private void CreateFeaturesBlock(XmlWriter writer)
        {
            writer.WriteStartElement("features");

            writer.WriteElementString("acpi", "");
            writer.WriteElementString("apic", "");
            writer.WriteElementString("pae", "");

            writer.WriteEndElement();
        }

        static private void CreateDevicesBlock(XmlWriter writer, BuildingBlock buildingFunc)
        {
            writer.WriteStartElement("devices");

            buildingFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateIsoDiskBlock(XmlWriter writer, string sourceFile)
        {
            writer.WriteStartElement("disk");
            writer.WriteAttributeString("type", "file");

            writer.WriteStartElement("driver");
            writer.WriteAttributeString("name", "qemu");
            writer.WriteAttributeString("type", "raw");
            writer.WriteEndElement();

            writer.WriteStartElement("source");
            writer.WriteAttributeString("file", sourceFile);
            writer.WriteEndElement();

            writer.WriteStartElement("target");
            writer.WriteAttributeString("dev", "sda");
            writer.WriteEndElement();

            writer.WriteElementString("readonly", "");

            writer.WriteEndElement();
        }

        static private void CreateInterfaceBlock(XmlWriter writer)
        {
            writer.WriteStartElement("interface");
            writer.WriteAttributeString("type", "network");

            writer.WriteStartElement("source");
            writer.WriteAttributeString("network", "default");
            writer.WriteEndElement();

            writer.WriteStartElement("model");
            writer.WriteAttributeString("type", "virtio");
            writer.WriteEndElement();

            writer.WriteStartElement("address");
            writer.WriteAttributeString("type", "pci");
            writer.WriteAttributeString("domain", "0x0000");
            writer.WriteAttributeString("bus", "0x00");
            writer.WriteAttributeString("slot", "0x08");
            writer.WriteAttributeString("function", "0x0");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        static private void CreateConsoleBlock(XmlWriter writer)
        {
            writer.WriteStartElement("console");
            writer.WriteAttributeString("type", "pty");

            writer.WriteStartElement("target");
            writer.WriteAttributeString("type", "serial");
            writer.WriteAttributeString("port", "0");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        static private void CreateSerialBlock(XmlWriter writer)
        {
            writer.WriteStartElement("serial");
            writer.WriteAttributeString("type", "pty");

            writer.WriteStartElement("target");
            writer.WriteAttributeString("port", "0");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
