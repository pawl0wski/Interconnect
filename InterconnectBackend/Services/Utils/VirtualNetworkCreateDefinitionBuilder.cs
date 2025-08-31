using Models;
using System.Xml;

namespace Services.Utils
{
    public class VirtualNetworkCreateDefinitionBuilder : BaseDefinitionBuilder<VirtualNetworkCreateDefinition>
    {
        private string? _networkName;
        private string? _macAddress;
        private string? _ipAddress;
        private string? _netMask;
        private bool _dhcpEnabled = false;
        private string? _dhcpStartRange;
        private string? _dhcpEndRange;

        public override VirtualNetworkCreateDefinitionBuilder SetFromCreateDefinition(VirtualNetworkCreateDefinition definition)
        {
            _networkName = definition.NetworkName;
            _macAddress = definition.MacAddress;
            _ipAddress = definition.IpAddress;
            _netMask = definition.NetMask;
            _dhcpEnabled = definition.DhcpEnabled;
            _dhcpStartRange = definition.DhcpStartRange;
            _dhcpEndRange = definition.DhcpEndRange;

            return this;
        }
        public override string Build()
        {
            CheckIsEverythingIsProvided(_networkName, _macAddress, _ipAddress, _netMask, _dhcpEnabled);

            var (writer, stringWriter) = CreateXmlWriter();

            using (writer)
            {
                writer.WriteStartDocument();

                CreateNetworkBlock(writer, w =>
                {
                    CreateNetworkNameBlock(w, _networkName);
                    CreateMacAddressBlock(w, _macAddress);
                    CreateIpAddressBlock(w, _ipAddress, _netMask, w =>
                    {
                        if (_dhcpEnabled)
                        {
                            CreateDhcpAddressBlock(w, _dhcpStartRange, _dhcpEndRange);
                        }
                    });
                });

                writer.WriteEndDocument();
            }

            return stringWriter.ToString();
        }

        static private void CreateNetworkBlock(XmlWriter writer, BuildingBlock blockFunc)
        {
            writer.WriteStartElement("network");

            blockFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateNetworkNameBlock(XmlWriter writer, string networkName)
        {
            writer.WriteStartElement("name");

            writer.WriteString(networkName);

            writer.WriteEndElement();
        }

        static private void CreateMacAddressBlock(XmlWriter writer, string address)
        {
            writer.WriteStartElement("mac");

            writer.WriteAttributeString("address", address);

            writer.WriteEndElement();
        }

        static private void CreateIpAddressBlock(XmlWriter writer, string ipAddress, string netMask, BuildingBlock blockFunc)
        {
            writer.WriteStartElement("ip");

            writer.WriteAttributeString("address", ipAddress);
            writer.WriteAttributeString("netmask", netMask);

            blockFunc(writer);

            writer.WriteEndElement();
        }

        static private void CreateDhcpAddressBlock(XmlWriter writer, string dhcpStartRange, string dhcpEndRange)
        {
            writer.WriteStartElement("dhcp");

            writer.WriteStartElement("range");

            writer.WriteAttributeString("start", dhcpStartRange);
            writer.WriteAttributeString("end", dhcpEndRange);

            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
