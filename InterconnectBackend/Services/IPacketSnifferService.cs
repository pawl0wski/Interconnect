using Models;

namespace Services
{
    public interface IPacketSnifferService
    {
        public void StartListeningForBridge(string interfaceName);
        public List<PacketSniffer> GetOpenedSniffers();
        public bool ListenForPacket(PacketSniffer sniffer);
        public Packet? GetCapturedPacket();
    }
}
