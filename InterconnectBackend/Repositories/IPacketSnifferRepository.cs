using Models;

namespace Repositories
{
    /// <summary>
    /// Repository managing packet analyzers in memory.
    /// </summary>
    public interface IPacketSnifferRepository
    {
        /// <summary>
        /// Adds a packet analyzer to the repository.
        /// </summary>
        /// <param name="sniffer">Packet analyzer.</param>
        public void AddSniffer(PacketSniffer sniffer);
        
        /// <summary>
        /// Removes a packet analyzer for a specified bridge.
        /// </summary>
        /// <param name="bridgeName">Network bridge name.</param>
        void RemoveSniffer(string bridgeName);
        
        /// <summary>
        /// Retrieves a list of opened packet analyzers.
        /// </summary>
        /// <returns>List of opened analyzers.</returns>
        public List<PacketSniffer> GetOpenedSniffers();
    }
}
