#ifndef PACKETSNIFFER_H
#define PACKETSNIFFER_H
#include <queue>
#include <string>

#include "../interfaces/ILibpcapWrapper.h"
#include "../models/Packet.h"
#include "../wrappers/LibpcapWrapper.h"

/**
 * @brief Class for capturing network packets.
 *
 * This class provides functionality for capturing packets from network interfaces
 * using the libpcap library. It maintains a queue of captured packets and handles
 * the packet capture lifecycle.
 */
class PacketSniffer
{
    ILibpcapWrapper* pcap;
    char errBuffer[PCAP_ERRBUF_SIZE]{};
    std::queue<Packet> packetsQueue;

public:
    /**
     * @brief Constructs a PacketSniffer with default libpcap wrapper.
     */
    PacketSniffer()
    {
        pcap = new LibpcapWrapper();
    }

    /**
     * @brief Opens a packet capture handler for the specified network interface.
     *
     * @param interfaceName Name of the network interface to capture packets from.
     * @return pcap_t* Pointer to the packet capture handler.
     */
    [[nodiscard]] pcap_t* openSnifferHandler(const std::string& interfaceName);

    /**
     * @brief Listens for a single packet on the specified interface.
     *
     * This method captures one packet and adds it to the internal queue.
     *
     * @param snifferHandler Active packet capture handler.
     * @param interfaceName Name of the network interface.
     * @return bool True if a packet was successfully captured, false otherwise.
     */
    [[nodiscard]] bool listenForPacket(pcap_t* snifferHandler, const std::string& interfaceName);

    /**
     * @brief Closes the packet capture handler and stops listening.
     *
     * @param handler Packet capture handler to close.
     */
    void closeAndStopListening(pcap_t* handler) const;

    /**
     * @brief Retrieves and removes a packet from the internal queue.
     *
     * @return Packet The oldest packet in the queue.
     */
    [[nodiscard]] Packet getPacketFromQueue();

    /**
     * @brief Gets the number of packets currently in the queue.
     *
     * @return int Number of captured packets waiting in the queue.
     */
    [[nodiscard]] int getNumberOfReceivedPackets() const;

private:
    /**
     * @brief Callback function for processing captured packets.
     *
     * This static method is called by libpcap for each captured packet.
     *
     * @param args User-defined arguments (pointer to ListenCallbackArgs).
     * @param header Packet header containing metadata.
     * @param packet Raw packet data.
     */
    static void handlePackets(u_char* args, const pcap_pkthdr* header, const u_char* packet);
};

#endif //PACKETSNIFFER_H
