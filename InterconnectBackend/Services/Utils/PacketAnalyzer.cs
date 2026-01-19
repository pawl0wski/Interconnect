using Models;
using Models.Enums;
using NativeLibrary.Structs;
using System.Net;
using System.Runtime.InteropServices;

namespace BackgroundServices.Impl
{
    static internal class PacketAnalyzer
    {
        private static int LastPacketNumber = 0;
        public static Packet? AnalyzePacket(NativePacket packet, string bridgeName)
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

        private static string ConvertPacketDataToBase64(byte[] packet, int packetLength)
        {
            byte[] actualData = new byte[packetLength];
            Array.Copy(packet, actualData, packetLength);

            return Convert.ToBase64String(actualData);
        }

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

        private static string GetSourceMacAddress(byte[] packet)
        {
            return BitConverter.ToString(packet, 6, 6).Replace("-", ":");
        }

        private static string GetDestinationMacAddress(byte[] packet)
        {
            return BitConverter.ToString(packet, 0, 6).Replace("-", ":");
        }

        private static int GetIpVersion(byte[] ipPacket)
        {
            byte firstByte = ipPacket[0];
            byte ipVersion = (byte)(firstByte >> 4);

            return ipVersion;
        }

        private static IPAddress GetDestinationIp(byte[] ipPacket)
        {
            byte[] dstIpBytes = new byte[4];
            Array.Copy(ipPacket, 16, dstIpBytes, 0, 4);

            return new IPAddress(dstIpBytes);
        }

        private static IPAddress GetSourceIpAddress(byte[] ipPacket)
        {
            byte[] srcIpAddress = new byte[4];
            Array.Copy(ipPacket, 12, srcIpAddress, 0, 4);

            return new IPAddress(srcIpAddress);
        }
    }
}
