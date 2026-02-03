#include "LibpcapWrapper.h"

/**
 * @brief Opens a live packet capture handler for a network interface.
 * 
 * @param interfaceName The name of the network interface to capture from.
 * @param errBuff Buffer to store error messages if the operation fails.
 * @return pcap_t* Pointer to the opened handler, or nullptr if an error occurs.
 */
pcap_t* LibpcapWrapper::openHandlerLive(const std::string& interfaceName, char* errBuff)
{
    return pcap_open_live(interfaceName.c_str(), BUFSIZ, 1, 1000, errBuff);
}

/**
 * @brief Gets the data link layer type of the opened interface.
 * 
 * @param handler The packet capture handler.
 * @return int The data link type (e.g., DLT_EN10MB for Ethernet).
 */
int LibpcapWrapper::getLinkLayerType(pcap_t* handler)
{
    return pcap_datalink(handler);
}

/**
 * @brief Closes a packet capture handler.
 * 
 * @param handler The packet capture handler to close.
 */
void LibpcapWrapper::closeHandler(pcap_t* handler)
{
    return pcap_close(handler);
}

/**
 * @brief Dispatches packets to a callback handler.
 * 
 * Processes packets from the capture interface and invokes the callback for each packet.
 * 
 * @param handler The packet capture handler.
 * @param callback Function to be called for each captured packet.
 * @param args User-defined data to be passed to the callback.
 * @return int Number of packets processed, or -1 on error.
 */
int LibpcapWrapper::listenForPackets(pcap_t* handler, const pcap_handler callback, u_char* args)
{
    return pcap_dispatch(handler, 0, callback, args);
}

/**
 * @brief Closes and releases a packet capture handler.
 * 
 * @param handler The packet capture handler to close.
 */
void LibpcapWrapper::close(pcap_t* handler)
{
    return pcap_close(handler);
}
