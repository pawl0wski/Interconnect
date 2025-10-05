#include "PacketSniffer.h"

#include <cstring>
#include <memory>

#include "../exceptions/PacketSnifferException.h"
#include "../models/ListenCallbackArgs.h"
#include "../utils/StringUtils.h"

pcap_t* PacketSniffer::openSnifferHandler(const std::string& interfaceName)
{
    const auto snifferHandler = pcap->openHandlerLive(interfaceName, this->errBuffer);

    if (snifferHandler == nullptr)
    {
        throw PacketSnifferException(std::string(this->errBuffer));
    }

    if (pcap->getLinkLayerType(snifferHandler) != DLT_EN10MB)
    {
        throw PacketSnifferException("Opened device doesn't have ethernet headers");
    }

    return snifferHandler;
}

bool PacketSniffer::listenForPacket(pcap_t* snifferHandler, const std::string& interfaceName)
{
    const auto callbackArgs = std::make_unique<ListenCallbackArgs>(ListenCallbackArgs(this, interfaceName));
    const auto listenOutput = pcap->listenForPackets(snifferHandler, handlePackets,
                                                     reinterpret_cast<u_char*>(callbackArgs.get()));

    if (listenOutput == -1)
    {
        throw PacketSnifferException(pcap_geterr(snifferHandler));
    }

    return listenOutput != 0;
}

void PacketSniffer::closeAndStopListening(pcap_t* handler) const
{
    pcap->close(handler);
}

Packet PacketSniffer::getPacketFromQueue()
{
    const auto packet = packetsQueue.front();
    packetsQueue.pop();

    return packet;
}

int PacketSniffer::getNumberOfReceivedPackets() const
{
    return static_cast<int>(packetsQueue.size());
}

void PacketSniffer::handlePackets(u_char* args, const pcap_pkthdr* header, const u_char* packet)
{
    const auto callbackArgs = reinterpret_cast<ListenCallbackArgs*>(args);
    Packet pkt = {};

    StringUtils::copyStringToCharArray(callbackArgs->interfaceName, pkt.interfaceName, 64);

    pkt.contentLength = static_cast<int>(header->caplen);
    pkt.content = new unsigned char[pkt.contentLength];
    std::memcpy(pkt.content, packet, pkt.contentLength);

    callbackArgs->sniffer->packetsQueue.push(pkt);
}
