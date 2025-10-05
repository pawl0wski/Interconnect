#ifndef PACKET_H
#define PACKET_H

struct Packet
{
    char interfaceName[64];
    unsigned char* content;
    int contentLength;
};

#endif //PACKET_H
