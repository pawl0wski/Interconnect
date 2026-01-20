#ifndef STREAMRESPONSE_H
#define STREAMRESPONSE_H

/**
 * @brief Structure containing data received from a virtual machine console stream.
 *
 * This structure holds data read from a VM console stream along with the stream status.
 */
struct StreamData
{
    /**
     * @brief Buffer containing data read from the stream.
     */
    char buffer[255];
    
    /**
     * @brief Flag indicating whether the stream connection is broken.
     */
    bool isStreamBroken;
};

#endif // STREAMRESPONSE_H
