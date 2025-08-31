using Models;
using System.Xml;

namespace Services.Utils
{
    public class VirtualNetworkInterfaceCreateDefinitionBuilder : BaseDefinitionBuilder<VirtualNetworkInterfaceCreateDefinition>
    {
        private string? _macAddress;
        private string? _networkName;

        public override VirtualNetworkInterfaceCreateDefinitionBuilder SetFromCreateDefinition(VirtualNetworkInterfaceCreateDefinition definition)
        {
            _macAddress = definition.MacAddress;
            _networkName = definition.NetworkName;

            return this;
        }

        public override string Build()
        {
            CheckIsEverythingIsProvided(_macAddress, _networkName);

            var (writer, stringWriter) = CreateXmlWriter();

            using (writer)
            {
                CreateNetworkBlock(writer, w =>
                {
                    CreateMacAddressBlock(w, _macAddress);
                    CreateSourceNetworkBlock(w, _networkName);
                    CreateModelTypeBlock(w);
                });
            }

            return stringWriter.ToString();
        }

        static private void CreateNetworkBlock(XmlWriter writer, BuildingBlock blockFunc)
        {
            writer.WriteStartElement("interface");
            writer.WriteAttributeString("type", "network");

            blockFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateMacAddressBlock(XmlWriter writer, string macAddress)
        {
            writer.WriteStartElement("mac");

            writer.WriteAttributeString("address", macAddress);

            writer.WriteEndElement();
        }

        static private void CreateSourceNetworkBlock(XmlWriter writer, string networkName)
        {
            writer.WriteStartElement("source");

            writer.WriteAttributeString("network", networkName);

            writer.WriteEndElement();
        }

        static private void CreateModelTypeBlock(XmlWriter writer)
        {
            writer.WriteStartElement("model");

            writer.WriteAttributeString("type", "virtio");

            writer.WriteEndElement();
        }
    }
}
