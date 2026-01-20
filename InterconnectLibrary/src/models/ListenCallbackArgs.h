#ifndef LISTENCALLBACKARGS_H
#define LISTENCALLBACKARGS_H
#include "../packetsniffer/PacketSniffer.h"

/**
 * @brief Structure containing arguments for packet listening callbacks.
 *
 * This structure is passed to packet capture callback functions to provide
 * context about the packet sniffer and interface being monitored.
 */
struct ListenCallbackArgs
{
    /**
     * @brief Pointer to the PacketSniffer instance handling the capture.
     */
    PacketSniffer* sniffer;
    
    /**
     * @brief Name of the network interface being monitored.
     */
    std::string interfaceName;
};

#endif //LISTENCALLBACKARGS_H
