using Models;
using Models.Enums;
using NativeLibrary.Structs;
using System.Net;
using System.Runtime.InteropServices;

namespace BackgroundServices.Impl
{
    /// <summary>
    /// Provides functionality for analyzing network packets from native packet data.
    /// </summary>
    static internal class PacketAnalyzer
    {
        private static int LastPacketNumber = 0;

        /// <summary>
        /// Analyzes a native packet and converts it into a managed Packet object.
        /// </summary>
        /// <param name="packet">The native packet structure containing raw packet data.</param>
        /// <returns>A Packet object if the packet type is supported, otherwise null.</returns>
        public static Packet? AnalyzePacket(NativePacket packet)
        {
            byte[] packetData = new byte[packet.ContentLength];
            Marshal.Copy(packet.Content, packetData, 0, packet.ContentLength);

            var dataLinkLayerType = GetDataLinkLayerPacketType(packetData);

            switch (dataLinkLayerType)
            {
                case DataLinkLayerPacketType.Arp:
                    return AnalyzeArpPacket(packetData, packet.ContentLength, packet.TimestampMicroseconds);
                case DataLinkLayerPacketType.Ipv4:
                    return AnalyzeIpv4Packet(packetData, packet.ContentLength, packet.TimestampMicroseconds);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Analyzes an ARP packet and extracts relevant information.
        /// </summary>
        /// <param name="packet">The raw packet data as a byte array.</param>
        /// <param name="packetLength">The length of the packet in bytes.</param>
        /// <param name="timestampMicroseconds">The packet capture timestamp in microseconds.</param>
        /// <returns>A Packet object containing the analyzed ARP packet data.</returns>
        private static Packet AnalyzeArpPacket(byte[] packet, int packetLength, ulong timestampMicroseconds)
        {
            return new Packet
            {
                Id = LastPacketNumber++,
                DataLinkLayerPacketType = DataLinkLayerPacketType.Arp,
                SourceMacAddress = GetSourceMacAddress(packet),
                DestinationMacAddress = GetDestinationMacAddress(packet),
                Content = ConvertPacketDataToBase64(packet, packetLength),
                TimestampMicroseconds = timestampMicroseconds
            };
        }

        /// <summary>
        /// Analyzes an IPv4 packet and extracts relevant information including IP addresses.
        /// </summary>
        /// <param name="packet">The raw packet data as a byte array.</param>
        /// <param name="packetLength">The length of the packet in bytes.</param>
        /// <param name="timestampMicroseconds">The packet capture timestamp in microseconds.</param>
        /// <returns>A Packet object containing the analyzed IPv4 packet data.</returns>
        private static Packet AnalyzeIpv4Packet(byte[] packet, int packetLength, ulong timestampMicroseconds)
        {
            byte[] ipPacket = new byte[packetLength - 14];
            Array.Copy(packet, 14, ipPacket, 0, packetLength - 14);

            return new Packet
            {
                Id = LastPacketNumber++,
                DataLinkLayerPacketType = DataLinkLayerPacketType.Ipv4,
                SourceMacAddress = GetSourceMacAddress(packet),
                DestinationMacAddress = GetDestinationMacAddress(packet),
                Content = ConvertPacketDataToBase64(packet, packetLength),
                IpVersion = GetIpVersion(ipPacket),
                SourceIpAddress = GetSourceIpAddress(ipPacket).ToString(),
                DestinationIpAddress = GetDestinationIp(ipPacket).ToString(),
                TimestampMicroseconds = timestampMicroseconds
            };
        }

        /// <summary>
        /// Converts packet data to a Base64-encoded string.
        /// </summary>
        /// <param name="packet">The packet data as a byte array.</param>
        /// <param name="packetLength">The length of the actual packet data.</param>
        /// <returns>A Base64-encoded string representation of the packet data.</returns>
        private static string ConvertPacketDataToBase64(byte[] packet, int packetLength)
        {
            byte[] actualData = new byte[packetLength];
            Array.Copy(packet, actualData, packetLength);

            return Convert.ToBase64String(actualData);
        }

        /// <summary>
        /// Determines the data link layer packet type by examining the EtherType field.
        /// </summary>
        /// <param name="packet">The raw packet data as a byte array.</param>
        /// <returns>The data link layer packet type (IPv4, ARP, or Unknown).</returns>
        private static DataLinkLayerPacketType GetDataLinkLayerPacketType(byte[] packet)
        {
            ushort etherType = (ushort)((packet[12] << 8) | packet[13]);

            return etherType switch
            {
                0x0800 => DataLinkLayerPacketType.Ipv4,
                0x0806 => DataLinkLayerPacketType.Arp,
                _ => DataLinkLayerPacketType.Unknown
            };
        }

        /// <summary>
        /// Extracts the source MAC address from the packet.
        /// </summary>
        /// <param name="packet">The raw packet data as a byte array.</param>
        /// <returns>The source MAC address in colon-separated format (e.g., "00:11:22:33:44:55").</returns>
        private static string GetSourceMacAddress(byte[] packet)
        {
            return BitConverter.ToString(packet, 6, 6).Replace("-", ":");
        }

        /// <summary>
        /// Extracts the destination MAC address from the packet.
        /// </summary>
        /// <param name="packet">The raw packet data as a byte array.</param>
        /// <returns>The destination MAC address in colon-separated format (e.g., "00:11:22:33:44:55").</returns>
        private static string GetDestinationMacAddress(byte[] packet)
        {
            return BitConverter.ToString(packet, 0, 6).Replace("-", ":");
        }

        /// <summary>
        /// Extracts the IP version from the IP packet header.
        /// </summary>
        /// <param name="ipPacket">The IP packet data (without Ethernet header) as a byte array.</param>
        /// <returns>The IP version number (typically 4 for IPv4).</returns>
        private static int GetIpVersion(byte[] ipPacket)
        {
            byte firstByte = ipPacket[0];
            byte ipVersion = (byte)(firstByte >> 4);

            return ipVersion;
        }

        /// <summary>
        /// Extracts the destination IP address from the IP packet header.
        /// </summary>
        /// <param name="ipPacket">The IP packet data (without Ethernet header) as a byte array.</param>
        /// <returns>An IPAddress object representing the destination IP address.</returns>
        private static IPAddress GetDestinationIp(byte[] ipPacket)
        {
            byte[] dstIpBytes = new byte[4];
            Array.Copy(ipPacket, 16, dstIpBytes, 0, 4);

            return new IPAddress(dstIpBytes);
        }

        /// <summary>
        /// Extracts the source IP address from the IP packet header.
        /// </summary>
        /// <param name="ipPacket">The IP packet data (without Ethernet header) as a byte array.</param>
        /// <returns>An IPAddress object representing the source IP address.</returns>
        private static IPAddress GetSourceIpAddress(byte[] ipPacket)
        {
            byte[] srcIpAddress = new byte[4];
            Array.Copy(ipPacket, 12, srcIpAddress, 0, 4);

            return new IPAddress(srcIpAddress);
        }
    }
}
