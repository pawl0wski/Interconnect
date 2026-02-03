using BackgroundServices.Impl;
using Models;
using NativeLibrary.Wrappers;
using Repositories;

namespace Services.Impl
{
    /// <summary>
    /// Service for capturing network packets.
    /// </summary>
    public class PacketSnifferService : IPacketSnifferService
    {
        private readonly IPacketSnifferWrapper _snifferWrapper;
        private readonly IPacketSnifferRepository _packetSnifferRepository;

        /// <summary>
        /// Initializes a new instance of the PacketSnifferService.
        /// </summary>
        /// <param name="snifferWrapper">Wrapper for packet sniffing functionality.</param>
        /// <param name="packetSnifferRepository">Repository for managing packet sniffers.</param>
        public PacketSnifferService(
            IPacketSnifferWrapper snifferWrapper,
            IPacketSnifferRepository packetSnifferRepository)
        {
            _snifferWrapper = snifferWrapper;
            _packetSnifferRepository = packetSnifferRepository;
        }

        /// <summary>
        /// Starts listening for packets on a specified network bridge.
        /// </summary>
        /// <param name="bridgeName">Network interface name.</param>
        public void StartListeningForBridge(string bridgeName)
        {
            var handler = _snifferWrapper.OpenSnifferHandler(bridgeName);
            _packetSnifferRepository.AddSniffer(new PacketSniffer
            {
                BridgeName = bridgeName,
                Handler = handler
            });
        }

        /// <summary>
        /// Retrieves a list of active packet analyzers.
        /// </summary>
        /// <returns>List of opened packet sniffers.</returns>
        public List<PacketSniffer> GetOpenedSniffers()
        {
            return _packetSnifferRepository.GetOpenedSniffers();
        }

        /// <summary>
        /// Listens for packets using the specified analyzer.
        /// </summary>
        /// <param name="sniffer">Packet analyzer.</param>
        /// <returns>True if packet was captured successfully, false otherwise.</returns>
        public bool ListenForPacket(PacketSniffer sniffer)
        {
            return _snifferWrapper.ListenForPacket(sniffer.Handler, sniffer.BridgeName);
        }

        /// <summary>
        /// Retrieves the last captured packet.
        /// </summary>
        /// <returns>Captured packet or null if no packet available.</returns>
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
