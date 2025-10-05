#ifndef LIBPCAPWRAPPER_H
#define LIBPCAPWRAPPER_H
#include "../interfaces/ILibpcapWrapper.h"

class LibpcapWrapper final : public ILibpcapWrapper
{
public:
    pcap_t* openHandlerLive(const std::string& interfaceName, char* errBuff) override;

    int getLinkLayerType(pcap_t* handler) override;

    void closeHandler(pcap_t* handler) override;

    int listenForPackets(pcap_t* handler, pcap_handler callback, u_char* args) override;

    void close(pcap_t* handler) override;
};

#endif //LIBPCAPWRAPPER_H
