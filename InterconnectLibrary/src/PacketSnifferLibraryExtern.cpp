#ifndef PACKETSNIFFERLIBRARYEXTERN_H
#define PACKETSNIFFERLIBRARYEXTERN_H
#include "models/ExecutionInfo.h"
#include "packetsniffer/PacketSniffer.h"
#include "utils/ExecutionInfoObtainer.h"

extern "C" {
/**
 * @brief Creates a new PacketSniffer instance.
 * 
 * Allocates and initializes a new PacketSniffer object with default libpcap wrapper.
 * 
 * @return PacketSniffer* Pointer to the newly created PacketSniffer instance.
 */
PacketSniffer* CreatePacketSniffer()
{
    return new PacketSniffer();
}

/**
 * @brief Destroys a PacketSniffer instance.
 * 
 * Deallocates and releases all resources associated with the PacketSniffer.
 * 
 * @param sniffer Pointer to the PacketSniffer instance to destroy.
 */
void DestroyPacketSniffer(const PacketSniffer* sniffer)
{
    delete sniffer;
}

/**
 * @brief Opens a packet capture handler on the specified interface.
 * 
 * Safely wraps PacketSniffer::openSnifferHandler with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param sniffer PacketSniffer instance.
 * @param bridgeName Name of the network interface to capture from.
 * @return int* Pointer to the packet capture handler cast as int pointer.
 */
int* PacketSniffer_OpenSnifferHandler(ExecutionInfo* executionInfo, PacketSniffer* sniffer, const char* bridgeName)
{
    int* handler = nullptr;

    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, bridgeName, &handler]
    {
        handler = reinterpret_cast<int*>(sniffer->openSnifferHandler(bridgeName));
    });

    return handler;
}

/**
 * @brief Listens for a single packet on the specified interface.
 * 
 * Safely wraps PacketSniffer::listenForPacket with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param sniffer PacketSniffer instance.
 * @param bridgeName Name of the network interface.
 * @param handler The packet capture handler.
 * @return bool True if a packet was captured, false otherwise.
 */
bool PacketSniffer_ListenForPacket(ExecutionInfo* executionInfo, PacketSniffer* sniffer, const char* bridgeName,
                             pcap_t* handler)
{
    bool output;

    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [bridgeName, sniffer, handler, &output]
    {
        output = sniffer->listenForPacket(handler, bridgeName);
    });

    return output;
}

/**
 * @brief Gets the number of captured packets in the queue.
 * 
 * Safely wraps PacketSniffer::getNumberOfReceivedPackets with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param sniffer PacketSniffer instance.
 * @param numberOfPackets Pointer to store the packet count.
 */
void PacketSniffer_GetNumberOfPackets(ExecutionInfo* executionInfo, PacketSniffer* sniffer, size_t* numberOfPackets)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, numberOfPackets]
    {
        *numberOfPackets = sniffer->getNumberOfReceivedPackets();
    });
}

/**
 * @brief Retrieves a packet from the queue.
 * 
 * Safely wraps PacketSniffer::getPacketFromQueue with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param sniffer PacketSniffer instance.
 * @param packet Pointer to Packet structure to receive the packet data.
 */
void PacketSniffer_GetPacket(ExecutionInfo* executionInfo, PacketSniffer* sniffer, Packet* packet)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, packet]
    {
        *packet = sniffer->getPacketFromQueue();
    });
}
}

#endif // PACKETSNIFFERLIBRARYEXTERN_H
