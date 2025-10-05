#ifndef ILIBPCAPWRAPPER_H
#define ILIBPCAPWRAPPER_H
#include <string>
#include <pcap/pcap.h>

class ILibpcapWrapper
{
public:
    virtual ~ILibpcapWrapper() = default;

    virtual pcap_t* openHandlerLive(const std::string& interfaceName, char* errBuff) = 0;

    virtual int getLinkLayerType(pcap_t* handler) = 0;

    virtual void closeHandler(pcap_t* handler) = 0;

    virtual int listenForPackets(pcap_t* handler, pcap_handler callback, u_char* args) = 0;

    virtual void close(pcap_t* handler) = 0;
};

#endif //ILIBPCAPWRAPPER_H
