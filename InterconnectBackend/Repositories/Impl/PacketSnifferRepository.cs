using Models;

namespace Repositories.Impl
{
    public class PacketSnifferRepository : IPacketSnifferRepository
    {
        private List<PacketSniffer> _packetSniffers = [];

        public void AddSniffer(PacketSniffer sniffer)
        {
            _packetSniffers.Add(sniffer);
        }

        public void RemoveSniffer(string bridgeName)
        {
            _packetSniffers.Remove(_packetSniffers.First(s => s.BridgeName == bridgeName));
        }

        public List<PacketSniffer> GetOpenedSniffers()
        {
            return _packetSniffers;
        }
    }
}
