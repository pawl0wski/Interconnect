#include "LibpcapWrapper.h"

pcap_t* LibpcapWrapper::openHandlerLive(const std::string& interfaceName, char* errBuff)
{
    return pcap_open_live(interfaceName.c_str(), BUFSIZ, 1, 1000, errBuff);
}

int LibpcapWrapper::getLinkLayerType(pcap_t* handler)
{
    return pcap_datalink(handler);
}

void LibpcapWrapper::closeHandler(pcap_t* handler)
{
    return pcap_close(handler);
}

int LibpcapWrapper::listenForPackets(pcap_t* handler, const pcap_handler callback, u_char* args)
{
    return pcap_dispatch(handler, 0, callback, args);
}

void LibpcapWrapper::close(pcap_t* handler)
{
    return pcap_close(handler);
}
