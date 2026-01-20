#ifndef PACKET_H
#define PACKET_H

/**
 * @brief Structure representing a captured network packet.
 *
 * This structure holds all the information about a single captured packet,
 * including its content, length, timestamp, and the interface it was captured on.
 */
struct Packet
{
    /**
     * @brief Name of the network interface where the packet was captured.
     */
    char interfaceName[64];
    
    /**
     * @brief Pointer to the raw packet data.
     */
    unsigned char* content;
    
    /**
     * @brief Length of the packet content in bytes.
     */
    int contentLength;
    
    /**
     * @brief Timestamp of packet capture in microseconds.
     */
    unsigned int timestampMicroseconds;
};

#endif //PACKET_H
