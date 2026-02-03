using Models;
using System.Xml;

namespace Services.Utils
{
    /// <summary>
    /// Builder class for creating virtual network XML definitions.
    /// </summary>
    public class VirtualNetworkCreateDefinitionBuilder : BaseDefinitionBuilder<VirtualNetworkCreateDefinition>
    {
        private string? _networkName;
        private string? _macAddress;
        private bool? _forwardNat;
        private string? _ipAddress;
        private string? _netMask;
        private string? _dhcpStartRange;
        private string? _dhcpEndRange;
        private string? _bridgeName;

        /// <summary>
        /// Sets the builder state from a virtual network create definition.
        /// </summary>
        /// <param name="definition">The network creation definition.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public override VirtualNetworkCreateDefinitionBuilder SetFromCreateDefinition(VirtualNetworkCreateDefinition definition)
        {
            _networkName = definition.NetworkName;
            _macAddress = definition.MacAddress;
            _forwardNat = definition.ForwardNat;
            _ipAddress = definition.IpAddress;
            _netMask = definition.NetMask;
            _dhcpStartRange = definition.DhcpStartRange;
            _dhcpEndRange = definition.DhcpEndRange;
            _bridgeName = definition.BridgeName;

            return this;
        }
        
        /// <summary>
        /// Builds the complete virtual network XML definition.
        /// </summary>
        /// <returns>The XML definition as a string.</returns>
        public override string Build()
        {
            CheckIsEverythingIsProvided(_networkName);

            var (writer, stringWriter) = CreateXmlWriter();

            using (writer)
            {
                writer.WriteStartDocument();

                CreateNetworkBlock(writer, w =>
                {
                    if (_forwardNat ?? false)
                    {
                        CreateForwardBlock(writer, _forwardNat ?? false);
                    }
                    CreateBridgeBlock(writer, _bridgeName!);
                    CreateNetworkNameBlock(w, _networkName!);
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

        static private void CreateForwardBlock(XmlWriter writer, bool forwardNat)
        {
            writer.WriteStartElement("forward");

            if (forwardNat)
            {
                writer.WriteStartAttribute("mode", "nat");
            }

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
