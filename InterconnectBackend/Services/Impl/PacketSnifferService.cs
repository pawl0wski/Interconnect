using BackgroundServices.Impl;
using Models;
using NativeLibrary.Wrappers;
using Repositories;

namespace Services.Impl
{
    public class PacketSnifferService : IPacketSnifferService
    {
        private readonly IPacketSnifferWrapper _snifferWrapper;
        private readonly IPacketSnifferRepository _packetSnifferRepository;

        public PacketSnifferService(
            IPacketSnifferWrapper snifferWrapper,
            IPacketSnifferRepository packetSnifferRepository)
        {
            _snifferWrapper = snifferWrapper;
            _packetSnifferRepository = packetSnifferRepository;
        }

        public void StartListeningForBridge(string bridgeName)
        {
            var handler = _snifferWrapper.OpenSnifferHandler(bridgeName);
            _packetSnifferRepository.AddSniffer(new PacketSniffer
            {
                BridgeName = bridgeName,
                Handler = handler
            });
        }

        public List<PacketSniffer> GetOpenedSniffers()
        {
            return _packetSnifferRepository.GetOpenedSniffers();
        }

        public bool ListenForPacket(PacketSniffer sniffer)
        {
            return _snifferWrapper.ListenForPacket(sniffer.Handler, sniffer.BridgeName);
        }

        public Packet? GetCapturedPacket()
        {
            if (_snifferWrapper.GetNumberOfPackets() <= 0)
            {
                return null;
            }
            var nativePacket = _snifferWrapper.GetPacket();

            return PacketAnalyzer.AnalyzePacket(nativePacket);
        }
    }
}
