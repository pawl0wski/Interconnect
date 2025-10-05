#ifndef LISTENCALLBACKARGS_H
#define LISTENCALLBACKARGS_H
#include "../packetsniffer/PacketSniffer.h"

struct ListenCallbackArgs
{
    PacketSniffer* sniffer;
    std::string interfaceName;
};

#endif //LISTENCALLBACKARGS_H
