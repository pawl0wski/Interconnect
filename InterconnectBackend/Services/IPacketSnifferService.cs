using Models;

namespace Services
{
    /// <summary>
    /// Service for capturing network packets.
    /// </summary>
    public interface IPacketSnifferService
    {
        /// <summary>
        /// Starts listening for packets on a specified network bridge.
        /// </summary>
        /// <param name="interfaceName">Network interface name.</param>
        public void StartListeningForBridge(string interfaceName);
        
        /// <summary>
        /// Retrieves a list of active packet analyzers.
        /// </summary>
        /// <returns>List of opened packet sniffers.</returns>
        public List<PacketSniffer> GetOpenedSniffers();
        
        /// <summary>
        /// Listens for packets using the specified analyzer.
        /// </summary>
        /// <param name="sniffer">Packet analyzer.</param>
        /// <returns>True if packet was captured successfully, false otherwise.</returns>
        public bool ListenForPacket(PacketSniffer sniffer);
        
        /// <summary>
        /// Retrieves the last captured packet.
        /// </summary>
        /// <returns>Captured packet or null if no packet available.</returns>
        public Packet? GetCapturedPacket();
    }
}
