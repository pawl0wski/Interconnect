#ifndef PACKETSNIFFERLIBRARYEXTERN_H
#define PACKETSNIFFERLIBRARYEXTERN_H
#include "models/ExecutionInfo.h"
#include "packetsniffer/PacketSniffer.h"
#include "utils/ExecutionInfoObtainer.h"

extern "C" {
PacketSniffer* CreatePacketSniffer()
{
    return new PacketSniffer();
}

void DestroyPacketSniffer(const PacketSniffer* sniffer)
{
    delete sniffer;
}

int* PacketSniffer_OpenSnifferHandler(ExecutionInfo* executionInfo, PacketSniffer* sniffer, const char* bridgeName)
{
    int* handler = nullptr;

    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, bridgeName, &handler]
    {
        handler = reinterpret_cast<int*>(sniffer->openSnifferHandler(bridgeName));
    });

    return handler;
}

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

void PacketSniffer_GetNumberOfPackets(ExecutionInfo* executionInfo, PacketSniffer* sniffer, size_t* numberOfPackets)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, numberOfPackets]
    {
        *numberOfPackets = sniffer->getNumberOfReceivedPackets();
    });
}

void PacketSniffer_GetPacket(ExecutionInfo* executionInfo, PacketSniffer* sniffer, Packet* packet)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [sniffer, packet]
    {
        *packet = sniffer->getPacketFromQueue();
    });
}
}

#endif // PACKETSNIFFERLIBRARYEXTERN_H
