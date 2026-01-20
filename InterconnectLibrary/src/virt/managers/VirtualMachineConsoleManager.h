#ifndef VIRTUALMACHINECONSOLEMANAGER_H
#define VIRTUALMACHINECONSOLEMANAGER_H
#include "BaseManagerWithConnection.h"
#include "../../wrappers/LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"
#include "../../models/StreamData.h"

/**
 * @brief Manages virtual machine console connections.
 *
 * This class provides functionality for opening and managing console streams
 * to virtual machines, allowing for interactive terminal access.
 */
class VirtualMachineConsoleManager : public BaseManagerWithConnection
{
public:
    using BaseManagerWithConnection::BaseManagerWithConnection;

    /**
     * @brief Opens a console stream to a virtual machine.
     *
     * @param vmUuid UUID of the virtual machine to connect to.
     * @return virStreamPtr Pointer to the opened console stream.
     */
    virStreamPtr openVirtualMachineConsole(const std::string& vmUuid) const;

    /**
     * @brief Receives data from a virtual machine console stream.
     *
     * @param stream Active console stream to read from.
     * @param streamData Pointer to StreamData structure to be filled with received data.
     */
    void getDataFromStream(virStreamPtr stream, StreamData* streamData) const;

    /**
     * @brief Sends data to a virtual machine console stream.
     *
     * @param stream Active console stream to write to.
     * @param data Data to send to the console.
     * @param dataSize Size of data in bytes.
     */
    void sendDataToStream(virStreamPtr stream, const char* data, int dataSize) const;

    /**
     * @brief Closes a console stream and releases resources.
     *
     * @param stream Console stream to close.
     */
    void closeStream(virStreamPtr stream) const;
};


#endif //VIRTUALMACHINECONSOLEMANAGER_H
