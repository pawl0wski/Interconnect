#ifndef ILIBPCAPWRAPPER_H
#define ILIBPCAPWRAPPER_H
#include <string>
#include <pcap/pcap.h>

/**
 * @brief Interface for libpcap wrapper functionality.
 *
 * This abstract class defines an interface for interacting with
 * the libpcap library for packet capture operations.
 */
class ILibpcapWrapper
{
public:
    virtual ~ILibpcapWrapper() = default;

    /**
     * @brief Opens a live packet capture handler for a network interface.
     *
     * @param interfaceName Name of the network interface to capture packets from.
     * @param errBuff Buffer to store error messages.
     * @return pcap_t* Pointer to the packet capture handler, or nullptr on failure.
     */
    virtual pcap_t* openHandlerLive(const std::string& interfaceName, char* errBuff) = 0;

    /**
     * @brief Gets the link-layer header type for a capture handler.
     *
     * @param handler Active packet capture handler.
     */
    virtual int getLinkLayerType(pcap_t* handler) = 0;

    /**
     * @brief Closes a packet capture handler.
     *
     * @param handler Packet capture handler to close.
     */
    virtual void closeHandler(pcap_t* handler) = 0;

    /**
     * @brief Starts listening for packets on the capture handler.
     *
     * @param handler Active packet capture handler.
     * @param callback Function to call when a packet is captured.
     * @param args User-defined arguments to pass to the callback function.
     */
    virtual int listenForPackets(pcap_t* handler, pcap_handler callback, u_char* args) = 0;

    /**
     * @brief Closes the packet capture handler and releases resources.
     *
     * @param handler Packet capture handler to close.
     */
    virtual void close(pcap_t* handler) = 0;
};

#endif //ILIBPCAPWRAPPER_H
