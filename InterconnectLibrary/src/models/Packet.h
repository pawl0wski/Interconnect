#ifndef PACKET_H
#define PACKET_H

struct Packet
{
    char interfaceName[64];
    unsigned char* content;
    int contentLength;
    unsigned int timestampMicroseconds;
};

#endif //PACKET_H
