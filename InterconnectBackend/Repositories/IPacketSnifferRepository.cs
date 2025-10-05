using Models;

namespace Repositories
{
    public interface IPacketSnifferRepository
    {
        public void AddSniffer(PacketSniffer sniffer);
        void RemoveSniffer(string bridgeName);
        public List<PacketSniffer> GetOpenedSniffers();
    }
}
