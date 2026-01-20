#ifndef LIBPCAPWRAPPER_H
#define LIBPCAPWRAPPER_H
#include "../interfaces/ILibpcapWrapper.h"

/**
 * @brief Concrete implementation of the ILibpcapWrapper interface.
 *
 * This class provides actual implementations for packet capture operations
 * using the libpcap library.
 */
class LibpcapWrapper final : public ILibpcapWrapper
{
public:
    /**
     * @brief Opens a live packet capture handler for a network interface.
     *
     * @param interfaceName Name of the network interface to capture packets from.
     * @param errBuff Buffer to store error messages if opening fails.
     * @return pcap_t* Pointer to the packet capture handler, or nullptr on failure.
     */
    pcap_t* openHandlerLive(const std::string& interfaceName, char* errBuff) override;

    /**
     * @brief Gets the link-layer header type for a capture handler.
     *
     * @param handler Active packet capture handler.
     * @return int Link-layer header type identifier.
     */
    int getLinkLayerType(pcap_t* handler) override;

    /**
     * @brief Closes a packet capture handler.
     *
     * @param handler Packet capture handler to close.
     */
    void closeHandler(pcap_t* handler) override;

    /**
     * @brief Starts listening for packets on the capture handler.
     *
     * @param handler Active packet capture handler.
     * @param callback Function to call when a packet is captured.
     * @param args User-defined arguments to pass to the callback function.
     * @return int Number of packets processed, or error code.
     */
    int listenForPackets(pcap_t* handler, pcap_handler callback, u_char* args) override;

    /**
     * @brief Closes the packet capture handler and releases resources.
     *
     * @param handler Packet capture handler to close.
     */
    void close(pcap_t* handler) override;
};

#endif //LIBPCAPWRAPPER_H
