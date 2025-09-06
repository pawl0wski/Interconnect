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
        private string? _dhcpStartRange;
        private string? _dhcpEndRange;
        private string? _bridgeName;

        public override VirtualNetworkCreateDefinitionBuilder SetFromCreateDefinition(VirtualNetworkCreateDefinition definition)
        {
            _networkName = definition.NetworkName;
            _macAddress = definition.MacAddress;
            _ipAddress = definition.IpAddress;
            _netMask = definition.NetMask;
            _dhcpStartRange = definition.DhcpStartRange;
            _dhcpEndRange = definition.DhcpEndRange;
            _bridgeName = definition.BridgeName;

            return this;
        }
        public override string Build()
        {
            CheckIsEverythingIsProvided(_networkName);

            var (writer, stringWriter) = CreateXmlWriter();

            using (writer)
            {
                writer.WriteStartDocument();

                CreateNetworkBlock(writer, w =>
                {
                    CreateBridgeBlock(writer, _bridgeName);
                    CreateNetworkNameBlock(w, _networkName);
                    if (_macAddress is not null)
                    {
                        CreateMacAddressBlock(w, _macAddress);
                    }
                    if (_ipAddress is not null && _netMask is not null)
                    {
                        CreateIpAddressBlock(w, _ipAddress, _netMask, w =>
                        {
                            if (_dhcpStartRange is not null && _dhcpEndRange is not null)
                            {
                                CreateDhcpAddressBlock(w, _dhcpStartRange, _dhcpEndRange);
                            }
                        });
                    }
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

        static private void CreateBridgeBlock(XmlWriter writer, string bridgeName)
        {
            writer.WriteStartElement("bridge");

            writer.WriteAttributeString("name", bridgeName);

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
