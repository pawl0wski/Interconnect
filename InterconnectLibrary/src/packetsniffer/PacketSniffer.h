#ifndef PACKETSNIFFER_H
#define PACKETSNIFFER_H
#include <queue>
#include <string>

#include "../interfaces/ILibpcapWrapper.h"
#include "../models/Packet.h"
#include "../wrappers/LibpcapWrapper.h"

class PacketSniffer
{
    ILibpcapWrapper* pcap;
    char errBuffer[PCAP_ERRBUF_SIZE]{};
    std::queue<Packet> packetsQueue;

public:
    PacketSniffer()
    {
        pcap = new LibpcapWrapper();
    }

    [[nodiscard]] pcap_t* openSnifferHandler(const std::string& interfaceName);

    [[nodiscard]] bool listenForPacket(pcap_t* snifferHandler, const std::string& interfaceName);

    void closeAndStopListening(pcap_t* handler) const;

    [[nodiscard]] Packet getPacketFromQueue();

    [[nodiscard]] int getNumberOfReceivedPackets() const;

private:
    static void handlePackets(u_char* args, const pcap_pkthdr* header, const u_char* packet);
};

#endif //PACKETSNIFFER_H
