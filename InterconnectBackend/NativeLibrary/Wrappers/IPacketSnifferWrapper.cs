using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers
{
    /// <summary>
    /// Interface wrapper for native packet analyzer library.
    /// </summary>
    public interface IPacketSnifferWrapper
    {
        /// <summary>
        /// Opens a packet analyzer handler for the specified bridge.
        /// </summary>
        /// <param name="bridgeName">Name of the network bridge.</param>
        /// <returns>Pointer to the analyzer handler.</returns>
        public IntPtr OpenSnifferHandler(string bridgeName);
        
        /// <summary>
        /// Listens for a network packet.
        /// </summary>
        /// <param name="handler">Pointer to the analyzer handler.</param>
        /// <param name="bridgeName">Name of the network bridge.</param>
        /// <returns>True if a packet was captured.</returns>
        public bool ListenForPacket(IntPtr handler, string bridgeName);
        
        /// <summary>
        /// Gets the last captured packet.
        /// </summary>
        /// <returns>Captured packet.</returns>
        public NativePacket GetPacket();
        
        /// <summary>
        /// Gets the number of captured packets.
        /// </summary>
        /// <returns>Number of packets.</returns>
        public int GetNumberOfPackets();
    }
}
