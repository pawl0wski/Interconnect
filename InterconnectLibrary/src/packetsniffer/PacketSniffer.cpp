#include "PacketSniffer.h"

#include <cstring>
#include <memory>

#include "../exceptions/PacketSnifferException.h"
#include "../models/ListenCallbackArgs.h"
#include "../utils/StringUtils.h"

/**
 * @brief Opens a packet capture handler for the specified network interface.
 * 
 * Initializes a packet sniffer handler for live packet capture on the given interface.
 * Verifies that the interface supports Ethernet headers (DLT_EN10MB).
 * 
 * @param interfaceName The name of the network interface to capture from.
 * @return pcap_t* Pointer to the opened packet capture handler.
 * @throws PacketSnifferException if handler cannot be opened or interface is not Ethernet.
 */
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

/**
 * @brief Listens for a single packet on the specified interface.
 * 
 * Captures one packet from the network and adds it to the internal queue.
 * Registers a callback function to process the captured packet.
 * 
 * @param snifferHandler The active packet capture handler.
 * @param interfaceName The name of the network interface.
 * @return bool True if a packet was successfully captured, false otherwise.
 * @throws PacketSnifferException if an error occurs during packet capture.
 */
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

/**
 * @brief Closes the packet capture handler and stops listening.
 * 
 * Properly closes and releases resources associated with the packet capture handler.
 * 
 * @param handler The packet capture handler to close.
 */
void PacketSniffer::closeAndStopListening(pcap_t* handler) const
{
    pcap->close(handler);
}

/**
 * @brief Retrieves and removes a packet from the internal queue.
 * 
 * Removes and returns the oldest packet stored in the capture queue.
 * 
 * @return Packet The packet at the front of the queue.
 */
Packet PacketSniffer::getPacketFromQueue()
{
    const auto packet = packetsQueue.front();
    packetsQueue.pop();

    return packet;
}

/**
 * @brief Gets the number of packets currently in the queue.
 * 
 * Returns the count of captured packets waiting in the internal queue.
 * 
 * @return int The number of packets in the queue.
 */
int PacketSniffer::getNumberOfReceivedPackets() const
{
    return static_cast<int>(packetsQueue.size());
}

/**
 * @brief Callback function for processing captured packets.
 * 
 * Static callback invoked by libpcap for each captured packet. Extracts packet metadata
 * and data, then stores the packet in the sniffer's queue.
 * 
 * @param args User-defined arguments (pointer to ListenCallbackArgs containing sniffer and interface name).
 * @param header Packet header with metadata including timestamp and captured length.
 * @param packet Raw packet data bytes.
 */
void PacketSniffer::handlePackets(u_char* args, const pcap_pkthdr* header, const u_char* packet)
{
    const auto callbackArgs = reinterpret_cast<ListenCallbackArgs*>(args);
    Packet pkt = {};

    StringUtils::copyStringToCharArray(callbackArgs->interfaceName, pkt.interfaceName, 64);

    pkt.contentLength = static_cast<int>(header->caplen);
    pkt.content = new unsigned char[pkt.contentLength];
    pkt.timestampMicroseconds = header->ts.tv_usec;
    std::memcpy(pkt.content, packet, pkt.contentLength);

    callbackArgs->sniffer->packetsQueue.push(pkt);
}
